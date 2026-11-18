using CIt_Demo_Project.Data;
using CIt_Demo_Project.Interface;
using CIt_Demo_Project.Model;
using CIt_Demo_Project.Repository;
using CIt_Demo_Project.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

// Add services to the container.




builder.Services.AddControllers();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
