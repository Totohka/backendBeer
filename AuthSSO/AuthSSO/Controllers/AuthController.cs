using AuthSSO.Attributes;
using AuthSSO.Common.Constants;
using AuthSSO.Common.Resourses.Emails;
using AuthSSO.Controllers.Base;
using BL.RequestDTOs;
using BL.ResponseDTOs;
using BL.Services.Tokens;
using BL.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Net;

namespace AuthSSO.Controllers
{
    /// <summary>
    /// Контроллер аутентификации/авторизации
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IAccessTokenService _accessTokenService;
        private readonly IStringLocalizer<Email> _localizer;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IUserService userService,
                              IAccessTokenService accessTokenService, 
                              ILogger<AuthController> logger,
                              IStringLocalizer<Email> localizer,
                              IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
            _localizer = localizer;
            _accessTokenService = accessTokenService;
            _logger = logger;
        }

        /// <summary>
        /// Первый вход в SSO. Нужен для быстрого восстановления доступа Димасу, Жоре, Стёпе и Кире, если слетят все данные с базы.
        /// Пароль для входа приходит на электронную почту.
        /// </summary>
        /// <param name="dto">DTO первого входа</param>
        /// <returns></returns>
        [HttpPost("login/first")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> FirstLoginDefaultAccount(FirstLoginDefaultDTO dto)
        {
            if (await _authService.FirstLoginAsync(dto))
            {
                return Ok();
            }
            return Unauthorized();
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="dto">DTO авторизации</param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> LoginUserPass(LoginUserPassDTO dto)
        {
            var token = await _authService.Login(dto.Login, dto.Password);
            if (token is not null)
            {
                Response.Cookies.Append(Constants.AccessToken, token, new CookieOptions { HttpOnly = true });
                return Ok();
            }
            return Unauthorized();
        }

        /// <summary>
        /// Регистрация нового пользователя. Через админа нужно будет активировать аккаунт пользователя.
        /// </summary>
        /// <param name="dto">DTO регистрации</param>
        /// <returns></returns>
        [HttpPost("registration")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Registration(RegistrationUserDTO dto)
        {
            if (await _authService.Registration(dto))
                return Created();
            else return Conflict();
        }

        /// <summary>
        /// Обновить access токен. Если в куках есть AccessToken.greatmarch.beer, то жопу не парим, бэк всё сделает за тебя
        /// </summary>
        /// <returns></returns>
        [AuthorizeSSO(Policy = Constants.RefreshPolicy)]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                string? uid = GetUserUid();
                if (Guid.TryParse(uid, out Guid userUid))
                {
                    string token = await _accessTokenService.GetAccessTokenAsync(userUid);
                    Response.Cookies.Append(Constants.AccessToken, token, new CookieOptions { HttpOnly = true});
                    return Ok();
                }
                return Forbid();
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Возвращает все роли пользователя, связанные с приложением
        /// </summary>
        /// <param name="dto">DTO верификации</param>
        /// <returns></returns>
        [AuthorizeSSO(Policy = Constants.TokenPolicy)]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VerifyResponseDTO))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> VerifyToken(VerifyDTO dto)
        {
            try
            {
                var accessToken = Request.Cookies[Constants.AccessToken];
                string? uid = GetUserUid();
                if (Guid.TryParse(uid, out Guid userUid))
                {
                    var verify = await _authService.VerifyTokenAsync(userUid, accessToken, dto);
                    if (verify.Success && !verify.Failure)
                    {
                        return Ok(verify);
                    }
                    else if (!verify.Success && !verify.Failure)
                    {
                        return Forbid();
                    }
                    else if (!verify.Success && verify.Failure)
                    {
                        return Unauthorized();
                    }
                }
                return Forbid();
            }
            catch
            {
                return Unauthorized();
            }
        }
    }
}
