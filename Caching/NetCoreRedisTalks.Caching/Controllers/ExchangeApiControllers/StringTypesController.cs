namespace NetCoreRedisTalks.Caching.Controllers.ExchangeApiControllers
{
    /// <summary>
    /// String işlemleri
    /// </summary>
    public class StringTypesController : BaseApiController
    {
        private readonly IDatabase _database;

        public StringTypesController(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        [HttpGet]
        public IActionResult SimpleStringOperations()
        {
            _database.StringSet("company-name", "Doğuş Teknoloji");

            _database.StringIncrement("visitor", 5); //ziyaretçi değerini 5 5 arttır.
            _database.StringDecrement("visitor", 2); //ziyaretçi değerini 2 2 azalt.

            RedisValue redisValue = _database.StringGetRange("company-name", 0, 3); //şirket ismini 0'dan başlayarak 3 karakter getir. (substring)
            long cachedVehicleNameLength = _database.StringLength("company-name"); //verinin uzunluğunu döner.

            var cachedVehicleName = _database.StringGet("company-name");
            if (!cachedVehicleName.HasValue)
                return NotFound();

            return Ok();
        }

        [HttpPost]
        public IActionResult SetComplexData()
        {
            _database.StringSet("company-name", "Doğuş Teknoloji");
            _database.StringSet("fruits-distributed-cache", JsonSerializer.Serialize(FakeDbContext.Fruits));

            return Ok();
        }
    }
}
