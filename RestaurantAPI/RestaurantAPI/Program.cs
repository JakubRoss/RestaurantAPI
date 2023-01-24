using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using RestaurantAPI;
using RestaurantAPI.Entities;
using RestaurantAPI.Middleware;
using RestaurantAPI.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

builder.Services.AddControllers();

builder.Services.AddDbContext<RestaurantDbContext>(options =>
{
    options.UseSqlServer(builder
        .Configuration
        .GetConnectionString("RestaurantDbConnection"));
});

builder.Services.AddScoped<RestaurantSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IRestaurantService,RestaurantService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IPasswordHasher<User>,PasswordHasher<User>>();
builder.Services.AddScoped<RequestTimeMiddleware>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<RestaurantSeeder>();
seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseMiddleware<RequestTimeMiddleware>();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json","RestaurantAPI");

});

//app.UseAuthorization();

app.MapControllers();

app.Run();
