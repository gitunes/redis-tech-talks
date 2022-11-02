namespace NetCoreRedisTalks.Caching.Controllers.ExchangeApiControllers
{
    /// <summary>
    /// Sıralı liste (Score)
    /// Veri tekrarını engeller ve skor değerine göre sıralama yapar.
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

            var sortedSetEntries = _database.SortedSetScan(SortedSetKey); //Listeye ait tüm elemanları listeleme

            var sortedSetEntriesRanks = _database.SortedSetRangeByRank(SortedSetKey, 0, 5, Order.Descending); //Skora göre azalan şekilde sırala

            return Ok(sortedSetEntries);
        }

        [HttpPost]
        public IActionResult Add()
        {
            foreach (var fruit in FakeDbContext.Fruits)
            {
                _database.SortedSetAdd(SortedSetKey, fruit.FruitName, fruit.Score);
            }

            _database.KeyExpire(SortedSetKey, DateTime.Now.AddHours(2)); //Cache süresini belirleme

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            _database.SortedSetRemove(SortedSetKey, "sorted-set-key");
            
            return Ok();
        }
    }
}
