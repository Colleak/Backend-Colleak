﻿using Colleak_Back_end.Interfaces;
using Colleak_Back_end.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Colleak_Back_end.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IMongoCollection<Employee> _employeesCollection;

        public EmployeesService(
            /*IOptions<ColleakDatabaseSettings> colleakDatabaseSettings*/)
        {
            var mongoClient = new MongoClient(
            ColleakDatabaseSettings.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ColleakDatabaseSettings.DatabaseName);

            _employeesCollection = mongoDatabase.GetCollection<Employee>(
                ColleakDatabaseSettings.EmployeeCollectionName);
        }

        public async Task<List<Employee>> GetEmployeeAsync() =>
            await _employeesCollection.Find(_ => true).ToListAsync();

        public async Task<List<Employee>> GetTrackedEmployeesAsync() =>
            await _employeesCollection.Find(x => x.AllowLocationTracking == true).ToListAsync();

        public async Task<List<Employee>> GetUntrackedEmployeesAsync() =>
            await _employeesCollection.Find(x => x.AllowLocationTracking == false).ToListAsync();

        public async Task<List<List<Employee>>> GetTrackedAndUntrackedEmployeesAsync()
        {
            List<List<Employee>> separateEmployees = new List<List<Employee>>();

            List<Employee> trackedEmployees = await GetTrackedEmployeesAsync();
            List<Employee> untrackedEmployees = await GetUntrackedEmployeesAsync();

            separateEmployees.Add(await GetTrackedEmployeesAsync());
            separateEmployees.Add(await GetUntrackedEmployeesAsync());

            return separateEmployees;
        }

        public async Task<List<Employee>> GetConnectedToDeviceEmployeesAsync() =>
            await _employeesCollection.Find(x => x.ConnectedToDevice == true).ToListAsync();

        public async Task<Employee?> GetEmployeeAsync(string id) =>
            await _employeesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateEmployeeAsync(Employee newEmployee) =>
            await _employeesCollection.InsertOneAsync(newEmployee);

        public async Task UpdateEmployeeAsync(string id, Employee updatedEmployee) =>
            await _employeesCollection.ReplaceOneAsync(x => x.Id == id, updatedEmployee);

        public async Task DeleteEmployeeAsync() =>
            await _employeesCollection.DeleteManyAsync(Builders<Employee>.Filter.Empty);

        public async Task DeleteEmployeeAsync(string id) =>
            await _employeesCollection.DeleteOneAsync(x => x.Id == id);

    }
}
