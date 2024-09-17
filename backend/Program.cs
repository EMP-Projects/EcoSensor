using EcoSensorApi;
using Gis.Net.Core.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.StartEcoSensor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

// Apply migrations
app.ApplyMigrations<EcoSensorAddDbContext, EcoSensorDbContext>();

app.Run();