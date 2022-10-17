var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect(new ConfigurationOptions
{
    EndPoints = { "localhost:6379" },
    AllowAdmin = true
}));

builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy(nameof(DogusTechnologyHub), builder =>
    {
        builder.WithOrigins(
            "http://localhost:5500",
            "http://127.0.0.1:5500")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
}).AddStackExchangeRedis("localhost:6379", options =>
{
    ConfigurationOptions configurationOptions = new()
    {
        ChannelPrefix = "SignalR-Hub-Prefix",
        AbortOnConnectFail = false,
        AsyncTimeout = 10000,
        ConnectTimeout = 15000,
        User = "default",
        Password = "4vNQ4FbYkngRA",
        DefaultDatabase = 0,
        AllowAdmin = true
    };

    options.Configuration = configurationOptions;

    options.ConnectionFactory = async writer =>
    {
        configurationOptions.EndPoints.Add("localhost:6379");
        configurationOptions.SetDefaultPorts();

        return await ConnectionMultiplexer.ConnectAsync(configurationOptions, writer);
    };
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseCors(nameof(DogusTechnologyHub));
app.MapHub<DogusTechnologyHub>("/dogustechnologyhub");

app.Run();
