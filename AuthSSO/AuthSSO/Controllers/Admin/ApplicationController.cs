using BL.Services.Admins.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;

namespace AuthSSO.Controllers.Admin
{
    [Route("api/v1/admin/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IBaseAdminService<Application> _applicationService;
        public ApplicationController(IBaseAdminService<Application> applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(Guid uid)
        {
            return Ok(await _applicationService.Get(uid));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int skip, int take)
        {
            return Ok(await _applicationService.GetAll(skip, take));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Application application)
        {
            return Ok(await _applicationService.Create(application));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Application application)
        {
            await _applicationService.Update(application);
            return Ok();
        }

        [HttpDelete("{uid}")]
        public async Task<IActionResult> Delete(Guid uid)
        {
            await _applicationService.Delete(uid);
            return Ok();
        }
    }
}
