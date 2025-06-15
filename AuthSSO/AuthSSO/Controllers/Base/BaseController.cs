using AuthSSO.Common.Constants;
using BL.Services.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace AuthSSO.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private protected string? ipAddress => HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        public BaseController()
        {
        }

        protected string? GetUserUid()
        {
            try
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(Request.Cookies[Constants.AccessToken]);
                return jwt.Claims.First(c => c.Type == Constants.UserUidClaim).Value;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
