using BL.Services.Admins.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;

namespace AuthSSO.Controllers.Admin
{
    [Route("api/v1/admin/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IBaseAdminService<Role> _roleService;
        public RoleController(IBaseAdminService<Role> roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(Guid uid)
        {
            return Ok(await _roleService.Get(uid));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int skip, int take)
        {
            return Ok(await _roleService.GetAll(skip, take));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Role role)
        {
            return Ok(await _roleService.Create(role));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Role role)
        {
            await _roleService.Update(role);
            return Ok();
        }

        [HttpDelete("{uid}")]
        public async Task<IActionResult> Delete(Guid uid)
        {
            await _roleService.Delete(uid);
            return Ok();
        }
    }
}
