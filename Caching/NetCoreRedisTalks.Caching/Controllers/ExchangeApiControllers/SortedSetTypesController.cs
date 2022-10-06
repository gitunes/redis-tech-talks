namespace NetCoreRedisTalks.Caching.Controllers.ExchangeApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortedSetTypesController : ControllerBase
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

            var sortedSetEntriesRanks = _database.SortedSetRangeByRank(SortedSetKey, 0, 5, Order.Descending).ToList();

            return Ok(sortedSetEntries);
        }

        [HttpPost]
        public IActionResult Add()
        {
            _database.SortedSetAdd(SortedSetKey, "test", 1);
            _database.KeyExpire(SortedSetKey, DateTime.Now.AddMinutes(2)); //Cache süresini belirleme

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
