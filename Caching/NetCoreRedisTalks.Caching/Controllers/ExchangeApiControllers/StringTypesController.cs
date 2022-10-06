namespace NetCoreRedisTalks.Caching.Controllers.ExchangeApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringTypesController : ControllerBase
    {
        private readonly IDatabase _database;

        public StringTypesController(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        [HttpGet]
        public IActionResult SimpleStringOperations()
        {
            _database.StringIncrement("visitor", 5); //ziyaretçi değerini 5 5 arttır.
            _database.StringDecrement("visitor", 2); //ziyaretçi değerini 2 2 azalt.

            _database.StringGetRange("vehicle-name", 0, 3); //araç ismini 0'dan başlayarak 3 karakter getir. (substring)
            long cachedVehicleNameLength = _database.StringLength("vehicle-name"); //verinin uzunluğunu döner.

            var cachedVehicleName = _database.StringGet("vehicle-name");
            if (!cachedVehicleName.HasValue)
                return NotFound();

            return Ok();
        }

        [HttpGet]
        public IActionResult SetComplexData([FromBody] List<Vehicle> vehicles)
        {
            _database.StringSet("vehicle-name", "volkswagen");
            _database.StringSet("vehicles-distributed-cache", JsonSerializer.Serialize(vehicles));

            return Ok();
        }
    }
}
