namespace NetCoreRedisTalks.Caching.Controllers.ExchangeApiControllers
{
    /// <summary>
    /// Key değerine ait birden fazla veri ekleme
    /// </summary>
    public class SetTypesController : BaseApiController
    {
        private const string SetTypesKey = "set-types-key";

        private readonly IDatabase _database;

        public SetTypesController(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        [HttpGet]
        public IActionResult SetMembers()
        {
            bool isExists = _database.KeyExists(SetTypesKey);
            if (!isExists)
                return NotFound();

            var redisValues = _database.SetMembers(SetTypesKey);

            return Ok(redisValues);
        }

        [HttpPost]
        public IActionResult SetAdd()
        {
            _database.SetAdd(SetTypesKey, "dogustechnology");
            _database.SetAdd(SetTypesKey, "ltunestribe");

            return Ok();
        }

        [HttpDelete]
        public IActionResult SetRemove()
        {
            _database.SetRemove(SetTypesKey, "dogustechnology");

            return NoContent();
        }
    }
}
