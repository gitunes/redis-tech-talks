namespace NetCoreRedisTalks.Messaging.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BroadcastController : ControllerBase
    {
        public readonly ISubscriber _subscriber;

        public BroadcastController(IConnectionMultiplexer connectionMultiplexer)
        {
            _subscriber = connectionMultiplexer.GetSubscriber();
        }

        /// <summary>
        /// Kanala yeni video yükleme
        /// </summary>
        /// <remarks>Abone olmuş tüm kullanıcılara yeni video yüklendiğinde bildirim gönderir.</remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadNewVideo")]
        public async Task<IActionResult> UploadNewVideo()
        {
            long response = await _subscriber.PublishAsync("dogus-technology-youtube-channel", "Redis: Etkili Cache Yönetiminin En Popüler Yolu adında yeni video eklendi.");
            return Ok();
        }

        /// <summary>
        /// Radyo yayınını başlat
        /// </summary>
        /// <remarks>Radyo yayınını tüm kullanıcılara haber verir</remarks>
        [HttpPost]
        [Route("LTunesFm")]
        public async Task<IActionResult> LTunesFm()
        {
            long response = await _subscriber.PublishAsync("ltunes-fm-radio-channel", "LTunes fm yayına başlamak üzere..");
            return Ok();
        }
    }
}