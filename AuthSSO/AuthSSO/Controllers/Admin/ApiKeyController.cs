using BL.Services.Admins.Base;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;

namespace AuthSSO.Controllers.Admin
{
    [Route("api/v1/admin/[controller]")]
    [ApiController]
    public class ApiKeyController : ControllerBase
    {
        private readonly IBaseAdminService<ApiKey> _apiKeyService;
        public ApiKeyController(IBaseAdminService<ApiKey> apiKeyService)
        {
            _apiKeyService = apiKeyService;
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(Guid uid)
        {
            return Ok(await _apiKeyService.Get(uid));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int skip, int take)
        {
            return Ok(await _apiKeyService.GetAll(skip, take));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApiKey apiKey)
        {
            return Ok(await _apiKeyService.Create(apiKey));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ApiKey apiKey)
        {
            await _apiKeyService.Update(apiKey);
            return Ok();
        }

        [HttpDelete("{uid}")]
        public async Task<IActionResult> Delete(Guid uid)
        {
            await _apiKeyService.Delete(uid);
            return Ok();
        }
    }
}
