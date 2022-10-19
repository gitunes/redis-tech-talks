namespace NetCoreRedisTalks.Search.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchsController : ControllerBase
    {
        private readonly IAirportService _airportService;

        public SearchsController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        /// <summary>
        /// Redis ile arama
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string word)
        {
            var airports = await _airportService.SearchAsync(word);
            if (airports is null)
                return NotFound();

            return Ok(airports);
        }

        /// <summary>
        /// Doküman kimliğine göre listeler
        /// </summary>
        [HttpGet("{docId}")]
        public async Task<IActionResult> Get(string docId)
        {
            Airport airport = await _airportService.GetAsync(docId);
            if (airport is null)
                return NotFound(airport);

            return Ok(airport);
        }

        /// <summary>
        /// Doküman ekler
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Add(string docId, Airport airport)
        {
            bool succeeded = await _airportService.AddAsync(docId, airport);
            if (!succeeded)
                return BadRequest();

            return Ok();
        }

        /// <summary>
        /// Doküman günceller
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Update(string docId, Airport airport)
        {
            bool succeeded = await _airportService.UpdateAsync(docId, airport);
            if (!succeeded)
                return BadRequest();

            return Ok();
        }

        /// <summary>
        /// Doküman siler
        /// </summary>

        [HttpPost]
        public async Task<IActionResult> Delete(string docId)
        {
            bool succeeded = await _airportService.DeleteAsync(docId);
            if (!succeeded)
                return BadRequest();

            return Ok();
        }
    }
}
