using Devices.Api.Extensions;
using Devices.Api.Middleware;
using Devices.Application;
using Devices.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Hexagonal wiring
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Global exception handling
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Apply migrations + seed (startup)
await app.ApplyMigrationsAndSeedAsync();

app.Run();

public partial class Program { } // useful for integration tests
