namespace NetCoreRedisTalks.Caching.Controllers.ExchangeApiControllers
{
    /// <summary>
    /// Key-value listesi
    /// Bir key’e karşılık birden fazla field ve value tutabileceğimiz object benzeri yapılardır.
    /// </summary>
    public class HashTypesController : BaseApiController
    {
        private const string HashTypesKey = "hash-types-key";

        private readonly IDatabase _database;

        public HashTypesController(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        [HttpGet]
        public IActionResult HashGetAll()
        {
            bool isExists = _database.KeyExists(HashTypesKey);
            if (!isExists)
                return NotFound();

            var hashEntries = _database.HashGetAll(HashTypesKey);
            return Ok(hashEntries);
        }

        [HttpPost]
        public IActionResult HashSet()
        {
            bool succeeded = _database.HashSet(HashTypesKey, "dogustechnology", "LTunes Tribe");
            if (!succeeded)
                return BadRequest();

            return Ok();
        }

        [HttpDelete]
        public IActionResult HashDelete()
        {
            _database.HashDelete(HashTypesKey, "dogustechnology");
            return Ok();
        }
    }
}
