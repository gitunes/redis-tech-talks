var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

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
        //Birden �ok SignalR uygulamas� i�in bir Redis sunucusu kullan�yorsan�z, her SignalR uygulamas� i�in farkl� bir kanal �neki kullan�n.
        ChannelPrefix = "SignalR-Hub-Prefix"
    };

    options.Configuration = configurationOptions;

    options.ConnectionFactory = async writer =>
    {
        var connection = await ConnectionMultiplexer.ConnectAsync(configurationOptions, writer);
        connection.ConnectionFailed += (_, e) =>
        {
            Console.WriteLine("Redis ba�lant�s� ba�ar�s�z.");
        };

        if (!connection.IsConnected)
        {
            Console.WriteLine("Redis'e ba�lanmad�.");
        }

        return connection;
    };
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseCors(nameof(DogusTechnologyHub));
app.MapHub<DogusTechnologyHub>("/dogustechnologyhub");

app.Run();
