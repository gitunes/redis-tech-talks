IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(new ConfigurationOptions
{
    EndPoints = { "localhost:6379" },
    AbortOnConnectFail = false,
    AsyncTimeout = 10000,
    ConnectTimeout = 15000,
    User = "default",
    Password = "4vNQ4FbYkngRA",
    DefaultDatabase = 0,
    AllowAdmin = true
});

ISubscriber subscriber = connectionMultiplexer.GetSubscriber();

await subscriber.SubscribeAsync("dogus-technology-youtube-channel", (RedisChannel redisChannel, RedisValue message) =>
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("dogus-technology-youtube-channel: {0}", message);
});

await subscriber.SubscribeAsync("ltunes-fm-radio-channel", (RedisChannel redisChannel, RedisValue message) =>
{
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("ltunes-fm-radio-channel: {0}", message);
});

Console.ReadKey();