using FitWallet.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Retrieve configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

// Add services to the container.
var migrationsAssemblyName = typeof(ApplicationDatabaseContext).Assembly.GetName().Name;
builder.Services.AddDbContext<ApplicationDatabaseContext>(options =>
    options.UseNpgsql(
        configuration.GetConnectionString("Default"),
        x => x.MigrationsAssembly(migrationsAssemblyName)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
