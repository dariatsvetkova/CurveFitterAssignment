using CurveFitter.Server;
using Microsoft.EntityFrameworkCore;
using MathNet.Numerics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using CurveFitter.Server.Controllers;
using CurveFitter.Server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite("Data Source=archive.sqlite"));

builder.Services.AddMvc();
builder.Services.AddScoped<UsersController, UsersController>();
builder.Services.AddScoped<ArchivesController, ArchivesController>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// TBD: set a CORS policy
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
