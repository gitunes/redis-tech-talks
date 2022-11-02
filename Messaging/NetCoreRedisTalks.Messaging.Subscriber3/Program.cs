IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(new ConfigurationOptions
{
    EndPoints = { "localhost:6379" }
});

ISubscriber subscriber = connectionMultiplexer.GetSubscriber();

await subscriber.SubscribeAsync("ltunes-fm-radio-channel", (RedisChannel redisChannel, RedisValue message) =>
{
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("ltunes-fm-radio-channel: {0}", message);
});

Console.Title = "SUB 3 - LTunes Fm Radio Kanallarına Aboneyim.";

Console.ReadKey();