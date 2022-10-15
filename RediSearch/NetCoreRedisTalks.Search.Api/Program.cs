using NetCoreRedisTalks.Search.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect(new ConfigurationOptions
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

builder.Services.AddSingleton<IAirportService, AirportService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
