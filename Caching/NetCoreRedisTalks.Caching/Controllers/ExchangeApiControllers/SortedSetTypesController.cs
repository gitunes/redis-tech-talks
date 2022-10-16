namespace NetCoreRedisTalks.Caching.Controllers.ExchangeApiControllers
{
    /// <summary>
    /// Sıralı liste (Score)
    /// </summary>
    public class SortedSetTypesController : BaseApiController
    {
        private const string SortedSetKey = "sorted-set-key";
        private readonly IDatabase _database;

        public SortedSetTypesController(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        [HttpGet]
        public IActionResult Show()
        {
            bool isExists = _database.KeyExists(SortedSetKey);
            if (!isExists)
                return NotFound();

            var sortedSetEntries = _database.SortedSetScan(SortedSetKey);

            var sortedSetEntriesRanks = _database.SortedSetRangeByRank(SortedSetKey, 0, 5, Order.Descending); //Skora göre azalan şekilde sırala

            return Ok(sortedSetEntries);
        }

        [HttpPost]
        public IActionResult Add()
        {
            foreach (var vehicle in FakeDbContext.Vehicles)
            {
                _database.SortedSetAdd(SortedSetKey, vehicle.BrandName, vehicle.Score);
            }

            _database.KeyExpire(SortedSetKey, DateTime.Now.AddHours(2)); //Cache süresini belirleme

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            _database.SortedSetRemove(SortedSetKey, "test");

            return Ok();
        }
    }
}
