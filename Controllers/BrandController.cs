using Microsoft.AspNetCore.Mvc;
using Engage.Keyloop.Api.Models;
using Engage.Keyloop.Api.Models.Response;
using Engage.Keyloop.Api.Interface;

namespace Engage.Keyloop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IKeyloopService _keyloopService;

        public BrandController(ILogger<BrandController> logger, IKeyloopService keyloopService)
        {
            _logger = logger;
            _keyloopService = keyloopService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var brandResponse = await _keyloopService.GetBrandsAsync();
            return Ok(brandResponse);
        }
    }
}
