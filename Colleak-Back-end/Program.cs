using Colleak_Back_end.Interfaces;
using Colleak_Back_end.Models;
using Colleak_Back_end.Services;
using System;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

SecretClientOptions options = new SecretClientOptions();
var client = new SecretClient(new Uri("https://colleakdatabase.vault.azure.net/"), new DefaultAzureCredential(), options);

KeyVaultSecret ConnectionString = client.GetSecret("ConnectionString");
KeyVaultSecret DatabaseName = client.GetSecret("DatabaseName");
KeyVaultSecret EmployeeCollectionName = client.GetSecret("EmployeeCollectionName");

// Add services to the container.
//builder.Services.Configure<ColleakDatabaseSettings>(
//    builder.Configuration.GetSection("ColleakDatabase"));


builder.Services.AddSingleton<EmployeesService>();
builder.Services.AddSingleton<RouterService>();

ColleakDatabaseSettings.ConnectionString = ConnectionString.Value;
ColleakDatabaseSettings.DatabaseName = DatabaseName.Value;
ColleakDatabaseSettings.EmployeeCollectionName = EmployeeCollectionName.Value;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmployeesService, EmployeesService>();
builder.Services.AddScoped<IRouterService, RouterService>();

builder.Services.AddHostedService<TimedHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x
                .WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
