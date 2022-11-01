var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region InMemoryCache Konfig�rasyonu
builder.Services.AddMemoryCache();
#endregion

#region DistributedCache Konfig�rasyonu
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});
#endregion

#region StackExchange.Redis Konfig�rasyonu
builder.Services.TryAddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect(new ConfigurationOptions
{
    EndPoints = { "localhost:6379" },
    AllowAdmin = true
}));
#endregion

builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(20));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseSession();
app.MapControllers();

app.SetStaticDataToRedisDatabase();

app.Run();
