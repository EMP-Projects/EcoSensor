using Asp.Versioning;
using EcoSensorApi;
using EcoSensorApi.Auth;
using Gis.Net.Core.Tasks;
using Gis.Net.OpenMeteo;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNotificationService();
builder.AddAuthentication();

// connessione al database PostGis e registrazione servizi, repository e mapper
builder.AddEcoSensor();

// servizi per la qualità dell'aria
builder.Services.AddAirQuality(builder.Configuration["OpenMeteo:Url"]);

builder.Services.AddControllers();
builder.Services.AddHttpLogging(opt => opt.LoggingFields = HttpLoggingFields.All);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
    setup.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
    setup.ReportApiVersions = true; 
});

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