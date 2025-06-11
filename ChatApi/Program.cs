using ChatApi.Hubs; // Asegúrate de tener el namespace correcto si cambiaste el nombre
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Servicios necesarios
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR(); // Agrega el servicio de SignalR

var app = builder.Build();

// Configurar Swagger solo en entorno de desarrollo

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

// Endpoint básico de ejemplo
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// Aquí exponemos el Hub de SignalR
app.MapHub<ChatHub>("/chatHub");

app.Run();

// Clase record para el endpoint de prueba
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
