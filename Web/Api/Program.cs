using Application.Model;
using Application.Registry;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Registry;

var builder = WebApplication.CreateBuilder(args);

// Database Configuration
builder.Services.AddDbContext<BaseDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionLink"));
});

// Smtp Configuration
builder.Services.Configure<SmtpModel>(
    builder.Configuration.GetSection("SmtpCredentials"));

// Email Configuration
builder.Services.Configure<EmailSettingsModel>(
    builder.Configuration.GetSection("EmailSettings"));

// Add services to the container.
builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
