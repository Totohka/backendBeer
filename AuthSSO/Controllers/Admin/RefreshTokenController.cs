using BL.Services.Admins.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;

namespace AuthSSO.Controllers.Admin
{
    [Route("api/v1/admin/[controller]")]
    [ApiController]
    public class RefreshTokenController : ControllerBase
    {
        private readonly IBaseAdminService<RefreshToken> _refreshTokenService;
        public RefreshTokenController(IBaseAdminService<RefreshToken> refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(Guid uid)
        {
            return Ok(await _refreshTokenService.Get(uid));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int skip, int take)
        {
            return Ok(await _refreshTokenService.GetAll(skip, take));
        }

        [HttpPost]
        public async Task<IActionResult> Create(RefreshToken refreshToken)
        {
            return Ok(await _refreshTokenService.Create(refreshToken));
        }

        [HttpPut]
        public async Task<IActionResult> Update(RefreshToken refreshToken)
        {
            await _refreshTokenService.Update(refreshToken);
            return Ok();
        }

        [HttpDelete("{uid}")]
        public async Task<IActionResult> Delete(Guid uid)
        {
            await _refreshTokenService.Delete(uid);
            return Ok();
        }
    }
}
