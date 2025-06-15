using AuthSSO.Common.Constants;
using AuthSSO.Requirements;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AuthSSO.Handlers
{
    public class TokenHandler : IAuthorizationHandler
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenHandler(IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            List<IAuthorizationRequirement> requirements = context.PendingRequirements.ToList();
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            string? accessToken = httpContext.Request.Cookies[Constants.AccessToken];
            if (accessToken is null)
            {
                context.Fail();
                return;
            }

            foreach (var requirement in requirements)
            {
                switch (requirement)
                {
                    case TokenRequirement:
                        if (await ValidateToken(accessToken, false))
                        {
                            context.Succeed(requirement);
                        }
                        else
                        {
                            context.Fail();
                            return;
                        }
                        break;
                    case RefreshRequirement:
                        if (await ValidateToken(accessToken, true))
                        {
                            context.Succeed(requirement);
                        }
                        else
                        {
                            context.Fail();
                            return;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private async Task<bool> ValidateToken(string accessToken, bool isRefresh)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            var userUid = Guid.Parse(jwt.Claims.First(c => c.Type == Constants.UserUidClaim).Value);
            var refreshTokenValue = await GetRefreshTokenAsync(userUid);
            if (refreshTokenValue is null)
                return false;

            if (ValidateAccessToken(accessToken, refreshTokenValue, isRefresh))
            {
                return true;
            }

            return false;
        }

        private bool ValidateAccessToken(string accessToken, string refreshTokenValue, bool isRefresh)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(refreshTokenValue);
                tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                return true;
            }
            catch (SecurityTokenExpiredException ex) when (isRefresh)
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task<string?> GetRefreshTokenAsync(Guid userUid)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<UserContext>();
            var user = await dbContext.Users.FindAsync(userUid);
            if (user is null || user.RefreshTokenUid is null)
                return null;

            var refreshToken = await dbContext.RefreshTokens.FindAsync(user.RefreshTokenUid);
            if (refreshToken is null || refreshToken.DateExpire < DateTime.UtcNow)
                return null;

            return refreshToken.Value;
        }
    }
}
