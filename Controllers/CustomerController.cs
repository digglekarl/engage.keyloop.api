using Engage.Keyloop.Api.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Engage.Keyloop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly IKeyloopService _keyloopService;

    public CustomerController(ILogger<CustomerController> logger, IKeyloopService keyloopService)
    {
        _logger = logger;
        _keyloopService = keyloopService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var brandResponse = await _keyloopService.GetCustomersAsync();
        return Ok(brandResponse);
    }
}
