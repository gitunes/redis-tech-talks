namespace NetCoreRedisTalks.RealTime.Api.Hubs
{
    public class DogusTechnologyHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (exception is null)
            {
                ///handle
            }
            else
            {
                //Do something
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
