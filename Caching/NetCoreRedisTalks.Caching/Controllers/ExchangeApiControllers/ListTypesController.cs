namespace NetCoreRedisTalks.Caching.Controllers.ExchangeApiControllers
{
    /// <summary>
    /// Gelişmiş liste tipi
    /// Değerleri koleksiyonel olarak karşılayan türdür. Koleksiyonun başına veya sonuna ekleme yapılabilir
    /// </summary>
    public class ListTypesController : BaseApiController
    {
        private const string ListTypesKey = "list-types-key";
        private readonly IDatabase _database;

        public ListTypesController(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        [HttpGet]
        public IActionResult ListRange()
        {
            bool isExists = _database.KeyExists(ListTypesKey);
            if (!isExists)
                return NotFound();

            var redisValues = _database.ListRange(ListTypesKey);
            //var redisValues = _database.ListRange(ListTypesKey, 0 , 10); //0'dan başla 10 item getir
            return Ok(redisValues);
        }

        [HttpPost]
        public IActionResult ListLeftOrRightPush()
        {
            _database.ListRightPush(ListTypesKey, "technology"); //Listenin sonuna technology ekleyecek
            _database.ListLeftPush(ListTypesKey, "dogus"); //Listenin başına dogus ekleyecek
            
            return Ok();
        }

        [HttpDelete]
        public IActionResult ListRemove()
        {
            _database.ListRemove(ListTypesKey, "dogus");

            _database.ListLeftPop(ListTypesKey); //Listeye ait ilk öğeyi silecek
            _database.ListRightPop(ListTypesKey); //Listeye ait son öğeyi silecek

            return NoContent();
        }
    }
}
