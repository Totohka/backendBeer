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
    public class UserRoleController : BaseController
    {
        private readonly IBaseAdminService<UserRole> _userRoleService;
        public UserRoleController(IBaseAdminService<UserRole> userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(Guid uid)
        {
            return Ok(await _userRoleService.Get(uid));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int skip, int take)
        {
            return Ok(await _userRoleService.GetAll(skip, take));
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserRole userRole)
        {
            return Ok(await _userRoleService.Create(userRole));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserRole userRole)
        {
            await _userRoleService.Update(userRole);
            return Ok();
        }

        [HttpDelete("{uid}")]
        public async Task<IActionResult> Delete(Guid uid)
        {
            await _userRoleService.Delete(uid);
            return Ok();
        }
    }
}
