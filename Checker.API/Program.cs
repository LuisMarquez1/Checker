using Checker.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Checker.Application.Extensions;
using Checker.Hardware.Extensions;
using Checker.Persistance.Extensions;
using Checker.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Checker Services
builder.Services.AddApplication();
builder.Services.AddPersistence();
builder.Services.AddHardware();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure();
builder.Services.AddSwaggerGen();

// DbContext from Checker.Persistance
builder.Services.AddDbContext<CheckerDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("CheckerDb")));

// Accept browser to allow requests from React
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactClient", policy =>
    {
        policy
        .WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("ReactClient");

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
