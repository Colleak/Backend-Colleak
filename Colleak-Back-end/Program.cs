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

SecretClientOptions options = new SecretClientOptions()
{
    Retry =
        {
            Delay= TimeSpan.FromSeconds(2),
            MaxDelay = TimeSpan.FromSeconds(16),
            MaxRetries = 5,
            Mode = RetryMode.Exponential
         }
};
var client = new SecretClient(new Uri("https://colleakdatabase.vault.azure.net/"), new DefaultAzureCredential(), options);

KeyVaultSecret ConnectionString = client.GetSecret("ConnectionString");
KeyVaultSecret DatabaseName = client.GetSecret("DatabaseName");
KeyVaultSecret EmployeeCollectionName = client.GetSecret("EmployeeCollectionName");

List<string> secretValue = new List<string>();
secretValue.Add(ConnectionString.Value);
secretValue.Add(DatabaseName.Value);
secretValue.Add(EmployeeCollectionName.Value);

// Add services to the container.
//builder.Services.Configure<ColleakDatabaseSettings>(
//    builder.Configuration.GetSection("ColleakDatabase"));


builder.Services.AddSingleton<EmployeesService>();
ColleakDatabaseSettings.ConnectionString = ConnectionString.Value;
ColleakDatabaseSettings.DatabaseName = DatabaseName.Value;
ColleakDatabaseSettings.EmployeeCollectionName = EmployeeCollectionName.Value;

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



app.MapGet("/", () => secretValue);

app.Run();
