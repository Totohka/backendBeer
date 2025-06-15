using AuthSSO.Common.Constants;
using AuthSSO.Common.Enums;
using AuthSSO.Common.Resourses.Emails;
using BL.Helpers;
using BL.RequestDTOs;
using BL.ResponseDTOs;
using BL.Utils;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Model.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BL.Services.Users
{
    public interface IAuthService
    {
        public Task<VerifyResponseDTO> VerifyTokenAsync(Guid userUid, string accessToken, VerifyDTO dto);
        public Task<string?> Login(string login, string password);
        public Task<bool> Registration(RegistrationUserDTO dto);
        public Task<bool> FirstLoginAsync(FirstLoginDefaultDTO dto);
    }

    public class AuthService : IAuthService
    {
        private readonly IStringLocalizer<Email> _localizer;
        private readonly IBaseRepository<RefreshToken> _refreshTokenRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Application> _applicationRepository;
        private readonly IBaseRepository<UserRole> _userRoleRepository;
        private readonly IBaseRepository<Role> _roleRepository;

        public AuthService(IBaseRepository<User> userRepository, 
                           IStringLocalizer<Email> localizer,
                           IBaseRepository<RefreshToken> refreshTokenRepository,
                           IBaseRepository<Application> applicationRepository,
                           IBaseRepository<UserRole> userRoleRepository,
                           IBaseRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _localizer = localizer;
            _refreshTokenRepository = refreshTokenRepository;
            _applicationRepository = applicationRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }
        public async Task<string?> Login(string login, string password)
        {
            var user = await _userRepository.GetAsync(u => u.Login == login);
            if (user is null || !user.IsActive)
            {
                return null;
            }
            var hash = HashHelper.GenerateSha256Hash(password, user.Salt);
            if (hash == user.HashPassword)
            {
                return await GetAccessTokenAsync(user.Uid);
            }
            else return null;
        }

        public async Task<bool> Registration(RegistrationUserDTO dto)
        {
            var user = _userRepository.GetAsync(u => u.Login == dto.Login || u.Email == dto.Email);
            if (user is null)
            {
                var salt = HashHelper.GenerateSalt();
                var hash = HashHelper.GenerateSha256Hash(dto.Password, salt);
                var newUser = new User()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Login = dto.Login,
                    MiddleName = dto.MiddleName,
                    IsActive = false,
                    Salt = salt,
                    HashPassword = hash
                };
                await _userRepository.CreateAsync(newUser);
                return true;
            }
            return false;
        }

        public async Task<bool> FirstLoginAsync(FirstLoginDefaultDTO dto)
        {
            var user = await _userRepository.GetAsync(u => u.Login == dto.Login);
            var userWithEmail = await _userRepository.GetAsync(u => u.Email == dto.Email);

            if (userWithEmail is not null)
                return false;
            if (user is null)
                return false;
            if (user.HashPassword is not null)
                return false;
            

            var password = PassHelper.Generate();
            var salt = HashHelper.GenerateSalt();
            var hash = HashHelper.GenerateSha256Hash(password, salt);

            user.Email = dto.Email;
            user.HashPassword = hash;
            user.Salt = salt;
            await _userRepository.UpdateAsync(user);
            await EmailUtil.SendEmailAsync(dto.Email, password, EnumEmailMessages.FirstLogin, _localizer);
            return true;
        }

        public async Task<VerifyResponseDTO> VerifyTokenAsync(Guid userUid, string accessToken, VerifyDTO dto)
        {
            var user = await _userRepository.GetAsync(userUid);
            if (user is null && user.RefreshTokenUid.HasValue)
                return new VerifyResponseDTO { Success = false, Failure = true };

            var refreshToken = await _refreshTokenRepository.GetAsync(user.RefreshTokenUid.Value);
            if (refreshToken is null) 
                return new VerifyResponseDTO { Success = false, Failure = true };

            var validate = await ValidateAccessTokenAsync(accessToken, refreshToken.Value);
            if (validate is null) 
                return new VerifyResponseDTO { Success = false, Failure = true };
            else if (validate.Value)
            {
                string nameApp = "";
                var userRoles = await _userRoleRepository.GetAllAsync(ur => ur.UserUid == userUid);
                var result = new VerifyResponseDTO { Success = true, Failure = false };
                if (!userRoles.Count().Equals(0))
                {
                    var roleUids = userRoles.Select(ur => ur.RoleUid);
                    var roles = await _roleRepository.GetAllAsync(r => roleUids.Contains(r.Uid));
                    var apps = await _applicationRepository.GetAllAsync(a => roles.Select(r => r.ApplicationUid).Contains(a.Uid));
                    switch (dto.Application)
                    {
                        case EnumApplication.VolgaTracker:
                            if (apps.Select(a => a.Name).Contains("Volga-Tracker"))
                            {
                                nameApp = "Volga-Tracker";
                                break;
                            }
                            else return new VerifyResponseDTO { Success = false, Failure = false };
                        case EnumApplication.Learning:
                            if (apps.Select(a => a.Name).Contains("Learning"))
                            {
                                nameApp = "Learning";
                                break;
                            }
                            else return new VerifyResponseDTO { Success = false, Failure = false };
                        case EnumApplication.Auth:
                            if (apps.Select(a => a.Name).Contains("Auth"))
                            {
                                nameApp = "Auth";
                                break;
                            }
                            else return new VerifyResponseDTO { Success = false, Failure = false };
                        default:
                            return new VerifyResponseDTO { Success = false, Failure = false };
                    }

                    result.Roles.AddRange(
                        roles.Where(r => r.ApplicationUid == apps.First(a => a.Name == nameApp).Uid)
                        .Select(r => new RoleDTO
                            {
                                Uid = r.Uid,
                                Name = r.Name,
                                Description = r.Description
                            }
                        )
                    );
                    return result;
                }
                else
                {
                    return new VerifyResponseDTO { Success = false, Failure = true };
                }
            }
            else if (!validate.Value)
            {
                return new VerifyResponseDTO { Success = false, Failure = true };
            }
            else 
            {  
                return new VerifyResponseDTO { Success = false, Failure = true };
            }
        }
        private async Task<string> GetAccessTokenAsync(Guid userUid)
        {
            bool isCreateRefreshToken = false;
            var user = await _userRepository.GetAsync(userUid);
            RefreshToken refreshToken = null;
            if (user.RefreshTokenUid is null)
            {
                refreshToken = await CreateRefreshToken();
                user.RefreshTokenUid = refreshToken.Uid;
                await _userRepository.UpdateAsync(user);
                isCreateRefreshToken = true;
            }
            else refreshToken = await _refreshTokenRepository.GetAsync((Guid)user.RefreshTokenUid);

            if (refreshToken.DateExpire <= DateTime.UtcNow)
            {
                refreshToken = await CreateRefreshToken();
                user.RefreshTokenUid = refreshToken.Uid;
                await _userRepository.UpdateAsync(user);
                isCreateRefreshToken = true;
            }

            if (!isCreateRefreshToken)
                await UpdateRefreshToken(refreshToken);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(refreshToken.Value));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "AuthSSO",
                audience: "AuthSSO",
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

        private async Task UpdateRefreshToken(RefreshToken token)
        {
            token.DateUpdate = DateTime.UtcNow;
            token.DateExpire = DateTime.UtcNow.AddMonths(1);
            await _refreshTokenRepository.UpdateAsync(token);
        }

        private async Task<bool?> ValidateAccessTokenAsync(string accessToken, string refreshTokenValue)
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
            catch (SecurityTokenExpiredException ex)
            {
                return null;
            }
            catch
            {
                return false;
            }
        }

        private async Task<RefreshToken> CreateRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                DateCreate = DateTime.UtcNow,
                DateUpdate = DateTime.UtcNow,
                DateExpire = DateTime.UtcNow.AddMonths(1),
                Value = RefreshTokenHelper.Generate(),
                IsActive = true,
            };
            refreshToken.Uid = await _refreshTokenRepository.CreateAsync(refreshToken);
            return refreshToken;
        }
    }
}
