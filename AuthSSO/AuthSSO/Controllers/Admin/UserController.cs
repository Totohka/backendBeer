using AuthSSO.Attributes;
using AuthSSO.Controllers.Base;
using BL.Services.Admins;
using BL.Services.Admins.Base;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;

namespace AuthSSO.Controllers.Admin
{
    [AuthorizeSSO(Roles = "Admin")]
    [Route("api/v1/admin/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserAdminService _userService;
        public UserController(IUserAdminService userService) 
        {
            _userService = userService;
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(Guid uid)
        {
            return Ok(await _userService.Get(uid));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int skip, int take)
        {
            return Ok(await _userService.GetAll(skip, take));
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            return Ok(await _userService.Create(user));
        }

        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            await _userService.Update(user);
            return Ok();
        }

        [HttpDelete("{uid}")]
        public async Task<IActionResult> Delete(Guid uid)
        {
            await _userService.Delete(uid);
            return Ok();
        }

        [HttpPut("{uid}")]
        public async Task<IActionResult> ChangeActiveUser(Guid uid)
        {
            await _userService.ChangeActiveUser(uid);
            return Ok();
        }
    }
}
