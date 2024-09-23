using EcoSensorApi;
using Gis.Net.Core.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.StartEcoSensor();

// configuring Cors
const string ecoSensorAllowSpecificOrigins = "_ecoSensorAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: ecoSensorAllowSpecificOrigins,
        policy  =>
        {
            // TODO: Change to specific origins (* is for all origins)
            policy.WithOrigins("*");
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseCors(ecoSensorAllowSpecificOrigins);

// Apply migrations
app.ApplyMigrations<EcoSensorAddDbContext, EcoSensorDbContext>();
app.Run();