﻿using System.Reflection;
using FinanceManager.Common.Constants;
using FinanceManager.Common.Contracts;
using FinanceManager.Common.Entities;
using FinanceManager.Common.Models;
using FinanceManager.Common.Services;
using FinanceManager.Data;
using FinanceManager.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using Range = FinanceManager.Common.Models.Range;

namespace FinanceManager.Simulation.Generation;

public class Simulator(DataContext db, IPasswordHasher passwordHasher) : ISimulator
{
    private static int GetNumberFromRange(Range range) 
        => new Random().Next(range.Min, range.Max);
    
    private async Task<List<User>> CreateUsers(SimulationParameters simulationParameters)
    {
        var numberOfUsersToSimulate = GetNumberFromRange(simulationParameters.Users.Count);
        var dummyPassword = passwordHasher.HashPassword(TestConstants.Password);

        for (var i = numberOfUsersToSimulate; i > 0; i--)
        {
            var userName = Faker.Internet.Email(Faker.Name.First());

            User user = new() { Username = userName, Password = dummyPassword, WasSimulated = true};

            await db.AddAsync(user);
        }

        await db.SaveChangesAsync();

        var seededUsers = await db.User
            .OrderByDescending(x => x.Id)
            .Where(y => y.WasSimulated)
            .Take(numberOfUsersToSimulate)
            .ToListAsync();
        
        return seededUsers;
    }

    // private async Task<bool> BuildUserAccounts()
    // {
    //     
    // }

    public async Task<bool> RunSimulation(SimulationParameters simulationParameters, Settings settings)
    {
        var simUsersResult = await CreateUsers(simulationParameters);

        List<int> results = [simUsersResult.Count];

        if (!results.TrueForAll(x => x > 0))
        {
            return false;
        }
        
        settings.HasBeenSimulated = true;
        db.Settings.Update(settings);
        
        return await db.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveSimulatedData(Settings settings)
    {
        await db.User.Where(x => x.WasSimulated).ExecuteDeleteAsync();
        
        settings.HasBeenSimulated = false;
        db.Settings.Update(settings);
        
        return await Task.FromResult(true);
    }

    public async Task<bool> SimulateFromConfiguration(SimulationParameters simulationParameters)
    {
        var settings = await db.ConfigureSettingsTable(simulationParameters.ShouldSimulate);

        return settings switch
        {
            { HasBeenSimulated: false, ShouldSimulate: true } => await RunSimulation(simulationParameters, settings),
            { HasBeenSimulated: true, ShouldSimulate: false } => await RemoveSimulatedData(settings),
            _ => false
        };
    }
}