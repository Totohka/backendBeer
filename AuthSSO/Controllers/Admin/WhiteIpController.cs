using AuthSSO.Attributes;
using AuthSSO.Controllers.Base;
using BL.Services.Admins.Base;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;

namespace AuthSSO.Controllers.Admin
{
    [AuthorizeSSO(Roles = "Admin")]
    [Route("api/v1/admin/[controller]")]
    [ApiController]
    public class WhiteIpController : BaseController
    {
        private readonly IBaseAdminService<WhiteIp> _whiteIpService;
        public WhiteIpController(IBaseAdminService<WhiteIp> whiteIpService)
        {
            _whiteIpService = whiteIpService;
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(Guid uid)
        {
            return Ok(await _whiteIpService.Get(uid));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int skip, int take)
        {
            return Ok(await _whiteIpService.GetAll(skip, take));
        }

        [HttpPost]
        public async Task<IActionResult> Create(WhiteIp whiteIp)
        {
            return Ok(await _whiteIpService.Create(whiteIp));
        }

        [HttpPut]
        public async Task<IActionResult> Update(WhiteIp whiteIp)
        {
            await _whiteIpService.Update(whiteIp);
            return Ok();
        }

        [HttpDelete("{uid}")]
        public async Task<IActionResult> Delete(Guid uid)
        {
            await _whiteIpService.Delete(uid);
            return Ok();
        }
    }
}
