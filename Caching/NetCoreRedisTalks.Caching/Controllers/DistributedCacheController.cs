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
            string cachedValue = _distributedCache.GetString("fruits-distributed-cache");
            if (string.IsNullOrEmpty(cachedValue))
                return NotFound();

            var fruits = JsonSerializer.Deserialize<List<Fruit>>(cachedValue);
            return Ok(fruits);
        }

        [HttpPost]
        public IActionResult SetString()
        {
            DistributedCacheEntryOptions distributedCacheEntryOptions = new()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(3)
            };

            var fruits = JsonSerializer.Serialize(FakeDbContext.Fruits);

            _distributedCache.SetString("fruits-distributed-cache", fruits, distributedCacheEntryOptions);

            return Ok();
        }

        [HttpPost]
        public IActionResult SetFile()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/dogustechnology.jpeg");
            byte[] imageByte = System.IO.File.ReadAllBytes(path);

            _distributedCache.Set("dogustechnology-logo-file-distributed-cache", imageByte);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove()
        {
            _distributedCache.Remove("fruits-distributed-cache");
            return Ok();
        }
    }
}
