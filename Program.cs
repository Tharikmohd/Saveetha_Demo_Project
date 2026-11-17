using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Saveetha_Demo_Project.Data;
using Saveetha_Demo_Project.Interface;
using Saveetha_Demo_Project.Model;
using Saveetha_Demo_Project.Repository;
using Saveetha_Demo_Project.Service;

var builder = WebApplication.CreateBuilder(args);

//DB Connection
var connectionstring = Environment.GetEnvironmentVariable("DB_Connect");

if(string.IsNullOrWhiteSpace(connectionstring))
{
    throw new InvalidOperationException("DB_Connect environment variable is missing");
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring));
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthInterface, AuthService>();
builder.Services.AddScoped<IUserinterface, UserRepository>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
