// filepath: /W:/DotNet/TodoApi/Program.cs
using Microsoft.EntityFrameworkCore;
using NSwag.AspNetCore;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
DotNetEnv.Env.Load();

// Construct the connection string from environment variables
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT");

if (string.IsNullOrEmpty(dbName) || string.IsNullOrEmpty(dbUser) || string.IsNullOrEmpty(dbPassword) || string.IsNullOrEmpty(dbHost) || string.IsNullOrEmpty(dbPort))
{
    throw new InvalidOperationException("One or more required environment variables are missing: DB_NAME, DB_USER, DB_PASSWORD, DB_HOST, DB_PORT");
}

var connectionString = $"Host={dbHost};Database={dbName};Username={dbUser};Password={dbPassword};Port={dbPort}";

// Add services to the container.
builder.Services.AddDbContext<UserDb>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<UserService>();
builder.Services.AddControllers(); // Add this line to register controllers
builder.Services.AddSwaggerConfig();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfig();
}

app.MapControllers();

app.Run();