using Engage.Keyloop.Api.Configuration;
using Engage.Keyloop.Api.Handlers;
using Engage.Keyloop.Api.Interface;
using Engage.Keyloop.Api.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Host.UseSerilog(build, config) =>
//{
//    config.ReadFrom.Configuration(builder.Configuration)
//        .Enrich.FromLogContext();
//});

// Register the KeyloopConfiguration class with the configuration system

//builder.Services.Configure<IOptions<KeyloopConfiguration>>(builder.Configuration.GetSection("KeyloopConfiguration"));

var keyloopConfig = new KeyloopConfiguration();
builder.Configuration.GetSection("KeylookConfiguration").Bind(keyloopConfig);
builder.Services.AddSingleton(keyloopConfig); 

// Register the KeyloopService class with the DI system

builder.Services.AddScoped<IKeyloopService, KeyloopService>();

// Register the HttpClient class with the DI system
builder.Services.AddScoped<OAuthHandler>();


builder.Services.AddHttpClient(nameof(KeyloopService), httpClient =>
{
    httpClient.BaseAddress = new Uri(keyloopConfig.Url);
}).AddHttpMessageHandler<OAuthHandler>();


builder.Services.AddHttpClient(nameof(OAuthHandler), httpClient =>
{
    httpClient.BaseAddress = new Uri(keyloopConfig.Url);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
