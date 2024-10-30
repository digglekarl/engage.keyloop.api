using Engage.Keyloop.Api.Interface;
using Engage.Keyloop.Api.Models;
using Engage.Keyloop.Api.Models.Request;
using Engage.Keyloop.Api.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Engage.Keyloop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private readonly ILogger<PartsController> _logger;
        private readonly IKeyloopService _keyloopService;

        public PartsController(ILogger<PartsController> logger, IKeyloopService keyloopService)
        {
            _logger = logger;
            _keyloopService = keyloopService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(SearchPartRequest searchPartsRequest)
        {
            //TODO: Validate the searchPartsRequest

            //var partsResponse = await _keyloopService.SearchPartAsync(searchPartsRequest);

            var partsResponse = new PartsResponse
            {
                Part = new Part
                {
                    PartId = "F2130418",
                    PartCode = 2130418,
                    BrandCode = "FO",
                    Description = "CAR280178 BATTERY",
                }
            };

            return Ok(partsResponse);
        }

        [HttpGet("{partId}")]
        public async Task<IActionResult> Get(string partId)
        {
            //var partDetailsResponse = await _keyloopService.GetPartDetailsAsync(partId);

            var partDetailsResponse = new PartDetailsResponse
            {
                PartDetail = new PartDetail
                {
                    PartId = "F2130418",
                    PartCode = 2130418,
                    BrandCode = "FO",
                    Description = "CAR280178 BATTERY",
                    ListPrice = new ListPrice
                    {
                        NetValue = 100,
                        GrossValue = 120,
                        TaxValue = 20,
                        TaxRate = 20,
                        CurrencyCode = "GBP"
                    },
                    UnitOfSale = 6
                }
            };

            return Ok(partDetailsResponse);
        }
    }
}
