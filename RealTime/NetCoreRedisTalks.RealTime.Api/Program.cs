var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(corsOptions => //Taray�c�lar�n default olarak gelen some-origin policy g�venli�ini hafifletmek
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
        //Birden fazla signalr uygulamas�n�n bir adet redis sunucusunu kullanmas� halinde, uygulamay� di�erlerinden izole etmek
        ChannelPrefix = "DogusTechnologyChannelPrefix",
        EndPoints = { "localhost:6379" }
    };

    options.Configuration = configurationOptions;
});

var app = builder.Build();

app.MapControllers();

app.UseCors(nameof(DogusTechnologyHub));
app.MapHub<DogusTechnologyHub>("/dogustechnologyhub");

app.Run();
