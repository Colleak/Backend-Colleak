using Colleak_Back_end.Interfaces;
using Colleak_Back_end.Models;
using Colleak_Back_end.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ColleakDatabaseSettings>(
    builder.Configuration.GetSection("ColleakDatabase"));

builder.Services.AddSingleton<EmployeesService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Employees controller",
        Version = "v1"
    });
});

builder.Services.AddScoped<IEmployeesService, EmployeesService>();  

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
