using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("ProjectManagementDb"));
app.MapGet("/", () => "Hello World!");

app.Run();