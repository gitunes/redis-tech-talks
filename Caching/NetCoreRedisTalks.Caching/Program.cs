var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

#region InMemoryCache Konfigürasyonu
builder.Services.AddMemoryCache();
#endregion

#region DistributedCache Konfigürasyonu
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});
#endregion

#region StackExchange.Redis Konfigürasyonu
builder.Services.TryAddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect(new ConfigurationOptions
{
    EndPoints = { "localhost:6379" },
    AbortOnConnectFail = false,
    AsyncTimeout = 10000,
    ConnectTimeout = 15000,
    User = "default",
    Password = "4vNQ4FbYkngRA",
    DefaultDatabase = 0,
    AllowAdmin = true
}));
#endregion

var app = builder.Build();
app.Run();
