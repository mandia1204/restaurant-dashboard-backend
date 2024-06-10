using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models;
using restaurant_dashboard_backend;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
});

builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
// Add services to the container.

builder.Services.Configure<Secrets>(builder.Configuration.GetSection("Secrets"));
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("Database"));
builder.Services.ConfigureAuthAndToken(builder.Configuration);

var mockService = builder.Configuration.GetValue<bool>("MockService");
builder.Services.AddContainerDepencencies(mockService);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowAnyOrigin();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();