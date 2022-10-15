namespace NetCoreRedisTalks.Search.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IAirportService _airportService;

        public OperationsController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpPost]
        [Route("CreateIndex")]
        public async Task<IActionResult> CreateIndex()
        {
            bool succeeded = await _airportService.CreateIndexAsync();
            if (!succeeded)
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        [Route("DropIndex")]
        public async Task<IActionResult> DropIndex()
        {
            bool succeeded = await _airportService.DropIndexAsync();
            if (!succeeded)
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        [Route("PushSampleData")]
        public async Task<IActionResult> PushSampleData()
        {
            await _airportService.PushSampleDataAsync();
            return Ok();
        }
    }
}
