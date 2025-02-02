using System.Runtime.InteropServices;
using DotNetEnv;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PokedexApp.Data;
using PokedexApp.Middlewares;
using PokedexApp.Repositories;
using PokedexApp.Services;
using PokedexApp.Validators;

var builder = WebApplication.CreateBuilder(args);

// Optionally load environment variables from a .env file (if you use it)
Env.Load();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddTransient<IPokemonRepository, PokemonRepository>();
builder.Services.AddTransient<IPokemonService, PokemonService>();
builder.Services.AddControllers()
       .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PokemonValidator>());

// Add CORS policy for React frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "http://localhost:3001")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        }
    );
});

// Determine which database provider to use
// We'll use SQLite for macOS (or if you choose) and SQL Server for Windows.
if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
{
    // Use SQLite connection string from configuration.
    var sqliteConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                                 ?? "Data Source=PokedexDb.sqlite";
    builder.Services.AddDbContext<PokedexContext>(options =>
        options.UseSqlite(sqliteConnectionString)
    );
    Console.WriteLine("🔹 Using SQLite connection string: " + sqliteConnectionString);
}
else
{
    // Otherwise, default to SQL Server using connection string from environment or configuration.
    // (You can adjust this branch if you want to support SQL Server on Windows)
    var sqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                              ?? "Server=localhost,1433;Database=PokedexDb;User Id=sa;Password=YourPassword;TrustServerCertificate=True;";
    builder.Services.AddDbContext<PokedexContext>(options =>
        options.UseSqlServer(sqlConnectionString, sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
            );
        })
    );
    Console.WriteLine("🔹 Using SQL Server connection string: " + sqlConnectionString);
}

builder.Services.AddMemoryCache();

var app = builder.Build();

// Apply the CORS policy before other middlewares
app.UseCors("AllowReactApp");

// Global Exception Handling Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PokedexApp v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();