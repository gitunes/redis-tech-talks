namespace NetCoreRedisTalks.Caching.ApplicationLifespans
{
    public static class StaticDataCacheApplicationLifetime
    {
        private static IServiceProvider ServiceProvider { get; set; }

        public static void SetStaticDataToRedisDatabase(this IApplicationBuilder applicationBuilder)
        {
            ServiceProvider = applicationBuilder.ApplicationServices;
            IHostApplicationLifetime hostApplicationLifetime = ServiceProvider.GetRequiredService<IHostApplicationLifetime>();

            try
            {
                hostApplicationLifetime.ApplicationStarted.Register(() => OnStarted());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void OnStarted()
        {
            IConnectionMultiplexer connectionMultiplexer = ServiceProvider.GetRequiredService<IConnectionMultiplexer>();
            IDatabase database = connectionMultiplexer.GetDatabase();

            database.StringSet("cities", JsonSerializer.Serialize(FakeDbContext.Cities));
        }
    }
}
