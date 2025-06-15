using AuthSSO.Common.Constants;
using DAL.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BL.Services.Tokens
{
    public interface IAccessTokenService
    {
        public Task<string> GetAccessTokenAsync(Guid userUid);
    }

    public class AccessTokenService : IAccessTokenService
    {
        private readonly ILogger<AccessTokenService> _logger;
        private readonly IBaseRepository<User> _userRepository; 
        private readonly IBaseRepository<RefreshToken> _refreshTokenRepository; 
        public AccessTokenService(ILogger<AccessTokenService> logger,
                                  IBaseRepository<User> userRepository,
                                  IBaseRepository<RefreshToken> refreshTokenRepository) 
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<string> GetAccessTokenAsync(Guid userUid)
        {
            _logger.LogInformation("Вызван метод AccessTokenService.GetAccessTokenAsync()");

            var user = await _userRepository.GetAsync(userUid);

            if (user.RefreshTokenUid is not null)
            {
                var refreshToken = await _refreshTokenRepository.GetAsync((Guid)user.RefreshTokenUid);
                if (refreshToken.DateExpire <= DateTime.UtcNow)
                {
                    throw new UnauthorizedAccessException("Refresh токен истёк");
                }
                await UpdateRefreshToken(refreshToken);
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(refreshToken.Value));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "Encryptarium",
                    audience: "Encryptarium",
                    claims: new[]
                    { 
                        new Claim(Constants.UserUidClaim, userUid.ToString()), 
                        new Claim(Constants.RoleClaim, "Admin"),
                        new Claim(Constants.AppClaim, "Application")
                    },
                    expires: DateTime.UtcNow.AddMinutes(1),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return tokenString;
            }
            _logger.LogError("Метод AccessTokenService.GetAccessTokenAsync(). Некорректный refresh токен");
            throw new UnauthorizedAccessException("Некорректный refresh токен");
        }

        private async Task UpdateRefreshToken(RefreshToken token)
        {
            token.DateUpdate = DateTime.UtcNow;
            token.DateExpire = DateTime.UtcNow.AddMonths(1);
            await _refreshTokenRepository.UpdateAsync(token);
        }
    }
}
