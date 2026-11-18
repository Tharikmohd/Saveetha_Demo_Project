using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Saveetha_Demo_Project.Data;
using Saveetha_Demo_Project.Interface;
using Saveetha_Demo_Project.Model;
using Saveetha_Demo_Project.Repository;
using Saveetha_Demo_Project.Service;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;


Env.Load();

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

//JWT Configuration

var jwtkey = builder.Configuration["Jwt:Key"];
if(string.IsNullOrWhiteSpace(jwtkey))
{
    throw new InvalidOperationException("Jwt Key is missing from environment Variable");
}

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtkey)),
        ClockSkew = TimeSpan.Zero

    };

});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthInterface, AuthService>();
builder.Services.AddScoped<IUserinterface, UserRepository>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IJwtService, JwtService>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
