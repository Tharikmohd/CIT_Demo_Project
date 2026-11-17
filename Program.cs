using CIt_Demo_Project.Data;
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
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
