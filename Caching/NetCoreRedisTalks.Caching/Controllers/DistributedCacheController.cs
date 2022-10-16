namespace NetCoreRedisTalks.Caching.Controllers
{
    public class DistributedCacheController : BaseApiController
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
                return NotFound();

            var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(cachedValue);
            return Ok(vehicles);
        }

        [HttpPost]
        public IActionResult SetString()
        {
            DistributedCacheEntryOptions distributedCacheEntryOptions = new()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(1)
            };

            var serilazedValue = JsonSerializer.Serialize(FakeDbContext.Vehicles);

            _distributedCache.SetString("vehicles-distributed-cache", serilazedValue, distributedCacheEntryOptions);

            return Ok();
        }

        [HttpPost]
        public IActionResult SetFile()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/volkswagen.png");
            byte[] imageByte = System.IO.File.ReadAllBytes(path);

            _distributedCache.Set("volkswagen-logo-file-distributed-cache", imageByte);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove()
        {
            _distributedCache.Remove("vehicles-distributed-cache");
            return Ok();
        }
    }
}
