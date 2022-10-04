namespace NetCoreRedisTalks.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InMemoryCacheController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCacheController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult Get(string key)
        {
            var cachedVehicles = _memoryCache.Get<List<Vehicle>>(key);

            if (cachedVehicles is null)
                return NotFound("vehicle not found");

            return Ok(cachedVehicles);
        }

        [HttpPost]
        public IActionResult Set([FromBody] List<Vehicle> vehicles)
        {
            var cachedVehicles = _memoryCache.Set("vehicles-memory-cache", vehicles, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                SlidingExpiration = TimeSpan.FromSeconds(2),
                Priority = CacheItemPriority.Low
            });

            return Ok(cachedVehicles);
        }

        [HttpDelete]
        public IActionResult Remove(string key)
        {
            _memoryCache.Remove(key);

            return Ok();
        }

        [HttpGet]
        public IActionResult TryGetValue(string key)
        {
            bool isExists = _memoryCache.TryGetValue(key, out List<Vehicle> cachedVehicles);

            if (!isExists)
                return NotFound("vehicle not found");

            return Ok(cachedVehicles);
        }

        [HttpGet]
        public IActionResult GetOrCreate(string key)
        {
            List<Vehicle> cachedVehicles = _memoryCache.GetOrCreate<List<Vehicle>>(key, factory =>
            {
                factory.AbsoluteExpiration = DateTime.Now.AddHours(1);
                factory.SlidingExpiration = TimeSpan.FromMinutes(3);

                return FakeDbContext.Vehicles;
            });

            return Ok(cachedVehicles);
        }
    }
}
