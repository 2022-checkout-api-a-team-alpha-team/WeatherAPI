using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using WeatherAPI.HealthCheck;
using WeatherAPI.Helper;
using WeatherAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IGeoService, GeoService>();

builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IAirQualityParticulateMatterService, AirQualityParticulateMatterService>();

builder.Services.AddHttpClient<IAirQualityPollenService, AirQualityPollenService>(client => 
    client.BaseAddress = new Uri(ConstantsHelper.AQ_BASE));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// For Health Check UI
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

// For Health Check Endpoint
builder.Services
    .AddHealthChecks()
    .AddUrlGroup(new Uri(ApiUrlConstants.GEO), nameof(ApiUrlConstants.GEO))
    .AddUrlGroup(new Uri(ApiUrlConstants.WEATHER), nameof(ApiUrlConstants.WEATHER))
    .AddUrlGroup(new Uri(ApiUrlConstants.AQ), nameof(ApiUrlConstants.AQ));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// For Health Check UI
app.MapHealthChecksUI();

// For Health Check Endpoint
app.UseHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
