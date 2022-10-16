namespace NetCoreRedisTalks.Caching.Controllers
{
    public class SessionsController : BaseApiController
    {
        [HttpGet]
        public IActionResult GetSession()
        {
            HttpContext.Session.TryGetValue("company", out byte[] value);
            string company = Encoding.UTF8.GetString(value);
            return Ok(company);
        }

        [HttpPost]
        public IActionResult SetSession()
        {
            HttpContext.Session.Set("company", Encoding.UTF8.GetBytes("Dogus Technology"));
            return Ok();
        }
    }
}
