namespace NetCoreRedisTalks.Caching.Controllers
{
    public class InMemoryCacheController : BaseApiController
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<InMemoryCacheController> _logger;

        public InMemoryCacheController(
            IMemoryCache memoryCache, 
            ILogger<InMemoryCacheController> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        /// <summary>
        /// Anahtar değere göre bellekten veri listeler
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            var cachedVehicles = _memoryCache.Get<List<Vehicle>>("vehicles-memory-cache");
            return Ok(cachedVehicles);
        }

        /// <summary>
        /// Anahtar değer ile birlikte belleğe veri kaydeder
        /// </summary>
        [HttpPost]
        public IActionResult Set()
        {
            var cachedVehicles = _memoryCache.Set("vehicles-memory-cache", FakeDbContext.Vehicles, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(2),
                Priority = CacheItemPriority.Low
            });

            return Ok(cachedVehicles);
        }

        /// <summary>
        /// Anahtar değere ait bellekten veri siler
        /// </summary>
        [HttpDelete]
        public IActionResult Remove()
        {
            _memoryCache.Remove("vehicles-memory-cache");

            return Ok();
        }

        /// <summary>
        /// Anahtar değere ait bellekte veri varsa out keyword'ü ile listeler, ve metot dönüş tipinde (boolean) işlem sonucunu döndürür
        /// </summary>
        [HttpGet]
        public IActionResult TryGetValue()
        {
            bool isExists = _memoryCache.TryGetValue("vehicles-memory-cache", out List<Vehicle> cachedVehicles);
            if (!isExists)
                return NotFound();

            return Ok(cachedVehicles);
        }

        /// <summary>
        /// Anahtar değere ait bellekte veri varsa listeler, yoksa belleğe veriyi kaydedip sonra listeler
        /// </summary>
        [HttpGet]
        public IActionResult GetOrCreate()
        {
            List<Vehicle> cachedVehicles = _memoryCache.GetOrCreate("vehicles-memory-cache", cacheEntry =>
            {
                cacheEntry.AbsoluteExpiration = DateTime.Now.AddHours(1);
                cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(3);

                return FakeDbContext.Vehicles;
            });

            return Ok(cachedVehicles);
        }

        [HttpGet]
        public IActionResult RegisterPostEvictionCallback()
        {
            DateTime date = _memoryCache.GetOrCreate<DateTime>("date", cacheEntry =>
            {
                cacheEntry.AbsoluteExpiration = DateTime.Now.AddSeconds(10);
                cacheEntry.RegisterPostEvictionCallback((key, value, reason, state) =>
                {
                    _logger.LogInformation($"--- Key : {key}\nValue : {value}\nReason : {reason}\nState : {state}");
                });

                DateTime value = DateTime.Now;
                _logger.LogInformation($"--- Set Cache : {value}");
                return value;
            });

            _logger.LogInformation($"--- Get Cache : {date}");

            return Ok();
        }
    }
}
