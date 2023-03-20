using System.Runtime.CompilerServices;
using MongoDB.Driver;
using DriversAppApi.Models;
using DriversAppApi.Configurations;
using Microsoft.Extensions.Options;

namespace DriversAppApi.Services;

public class DriverService
{
    private readonly IMongoCollection<Driver> _driverCollections;

    public DriverService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _driverCollections = mongoDb.GetCollection<Driver>(databaseSettings.Value.CollectionName);

    }

    public async Task<List<Driver>> GetAsync() => await _driverCollections.Find(filter: _ => true).ToListAsync();
    public async Task<Driver> GetAsync(string id) =>
     await _driverCollections.Find(filter: Driver => Driver.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Driver driver) => await _driverCollections.InsertOneAsync(driver);


    public async Task UpdateAsync(Driver driver) =>
        await _driverCollections.ReplaceOneAsync(x => x.Id == driver.Id, driver);

    public async Task RemoveAsync(string id) => 
        await _driverCollections.DeleteOneAsync(x=> x.Id == id);

}