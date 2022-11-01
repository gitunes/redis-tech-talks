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
        ChannelPrefix = "DogusTechnologyChannelPrefix",
        EndPoints = { "localhost:6379" }
    };

    options.Configuration = configurationOptions;

    options.ConnectionFactory = async writer =>
    {
        var connection = await ConnectionMultiplexer.ConnectAsync(configurationOptions, writer);
        connection.ConnectionFailed += (_, e) =>
        {
            Console.WriteLine("Connection to Redis failed.");
        };

        if (!connection.IsConnected)
        {
            Console.WriteLine("Did not connect to Redis.");
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
