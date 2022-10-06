namespace NetCoreRedisTalks.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributedCacheController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;

        public DistributedCacheController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public IActionResult GetString()
        {
            string cachedValue = _distributedCache.GetString("vehicles-distributed-cache");
            if (string.IsNullOrEmpty(cachedValue))
                return NotFound("vehicle not found");

            var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(cachedValue);
            return Ok(vehicles);
        }

        [HttpPost]
        public IActionResult SetString([FromBody] List<Vehicle> vehicles)
        {
            DistributedCacheEntryOptions distributedCacheEntryOptions = new()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            };

            var serilazedValue = JsonSerializer.Serialize(vehicles);

            _distributedCache.SetString("vehicles-distributed-cache", serilazedValue, distributedCacheEntryOptions);

            return Ok();
        }

        [HttpPost]
        public IActionResult SetFile()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/volkswagen.jpg");
            byte[] imageByte = System.IO.File.ReadAllBytes(path);

            _distributedCache.Set("volkswagen-distributed-cache", imageByte);

            return Ok();
        }

        [HttpPost]
        public IActionResult Remove()
        {
            _distributedCache.Remove("vehicles-distributed-cache");
            return Ok();
        }
    }
}
