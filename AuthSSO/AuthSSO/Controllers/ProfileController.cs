using AuthSSO.Attributes;
using AuthSSO.Common.Constants;
using AuthSSO.Controllers.Base;
using BL.RequestDTOs;
using BL.ResponseDTOs;
using BL.Services.Users;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AuthSSO.Controllers
{
    /// <summary>
    /// Контроллер профиля
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProfileController : BaseController
    {
        private readonly IUserService _userService;
        public ProfileController(IUserService userService) 
        {
            _userService = userService;
        }

        /// <summary>
        /// Получить свой профиль
        /// </summary>
        /// <returns></returns>
        [AuthorizeSSO(Policy = Constants.TokenPolicy)]
        [HttpGet("my")]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetUserByToken()
        {
            string? uid = GetUserUid();
            if (Guid.TryParse(uid, out Guid userUid))
            {
                return Ok(await _userService.GetUserByUidAsync(userUid));
            }
            return Unauthorized();
        }

        /// <summary>
        /// Поменять свой язык. Присваивается в куку
        /// </summary>
        /// <param name="culture">ru или en</param>
        /// <returns></returns>
        [HttpPut("change/language/{culture}")]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(LangError) ,(int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ChangeLanguage(string culture)
        {
            Response.Cookies.Append(
                Constants.CookieCulture,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Ok();
        }

        /// <summary>
        /// Сменить пароль
        /// </summary>
        /// <param name="dto">DTO смены пароля</param>
        /// <returns></returns>
        [AuthorizeSSO(Policy = Constants.TokenPolicy)]
        [HttpPut("change/password")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO dto)
        {
            string? uid = GetUserUid();
            if (Guid.TryParse(uid, out Guid userUid))
            {
                if (await _userService.ChangePasswordUserAsync(userUid, dto.OldPassword, dto.NewPassword))
                    return Ok();
                else return BadRequest();
            }
            return Unauthorized();
        }

        /// <summary>
        /// Сменить логин
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns></returns>
        [AuthorizeSSO(Policy = Constants.TokenPolicy)]
        [HttpPut("change/login/{login}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> ChangeLogin(string login)
        {
            string? uid = GetUserUid();
            if (Guid.TryParse(uid, out Guid userUid))
            {
                if (await _userService.ChangeLoginUserAsync(userUid, login))
                    return Ok();
                else return BadRequest();
            }
            return Unauthorized();
        }

        /// <summary>
        /// Сменить email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns></returns>
        [AuthorizeSSO(Policy = Constants.TokenPolicy)]
        [HttpPut("change/email/{email}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> ChangeEmail(string email)
        {
            string? uid = GetUserUid();
            if (Guid.TryParse(uid, out Guid userUid))
            {
                if (await _userService.ChangeEmailUserAsync(userUid, email))
                    return Ok();
                else return BadRequest();
            }
            return Unauthorized();
        }

        /// <summary>
        /// Сменить ФИО
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [AuthorizeSSO(Policy = Constants.TokenPolicy)]
        [HttpPut("change/fio")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> ChangeFIO(ChangeFIOUserDTO dto)
        {
            string? uid = GetUserUid();
            if (Guid.TryParse(uid, out Guid userUid))
            {
                if (await _userService.ChangeFIOUserAsync(userUid, dto))
                    return Ok();
                else return BadRequest();
            }
            return Unauthorized();
        }
    }
}
