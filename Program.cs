using CIt_Demo_Project.Data;
using CIt_Demo_Project.Interface;
using CIt_Demo_Project.Model;
using CIt_Demo_Project.Repository;
using CIt_Demo_Project.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

//DB Connection

var connectionstring = Environment.GetEnvironmentVariable("CIT_DB");

if(string.IsNullOrWhiteSpace(connectionstring))
{
    throw new Exception("Database connection string missing");
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring));
});

//JWT Configuration

var jwtkey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrWhiteSpace(jwtkey))
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




builder.Services.AddControllers();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
