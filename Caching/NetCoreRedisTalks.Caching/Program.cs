var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Memory Cache
builder.Services.AddMemoryCache();

var app = builder.Build();
app.Run();
