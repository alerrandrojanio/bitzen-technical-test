using MeetingRooms.API.Mapping;
using MeetingRooms.API.Middlewares;
using MeetingRooms.DI.IoC;
using MeetingRooms.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    if (string.IsNullOrEmpty(connectionString))
        throw new Exception("A ConnectionString não pode ser nula.");

    options.UseNpgsql(connectionString);
});

MappingConfiguration.RegisterMappings();

builder.Services.AddServices();

builder.Services.ConfigureSettings(builder.Configuration);

builder.Services.AddVersioning();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddFluentValidators(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<GlobalErrorHandlerMiddleware>();

app.Run();
