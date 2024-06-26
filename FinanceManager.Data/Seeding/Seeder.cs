﻿using FinanceManager.Common.Constants;
using FinanceManager.Common.Entities;
using FinanceManager.Common.Services;
using FinanceManager.Common.Settings;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Data.Seeding;

public static class Seeder
{
    public static async Task InsertTestData(this DataContext dataContext, IPasswordHasher passwordHasher)
    {
        var testUser = await dataContext.User.FirstOrDefaultAsync(x => x.Username == TestConstants.Username);

        if (testUser == null)
        {
            var adminRole = await dataContext.EnsureAdminRoleCreated();

            var hashedPassword = passwordHasher.HashPassword(TestConstants.Password);
            var newSuperAdmin = new User { Username = TestConstants.Username,
                Password = hashedPassword,
                Roles = [new UserRole{Role = adminRole}]
            };

            dataContext.Add(newSuperAdmin);
        }

        if (dataContext.ChangeTracker.HasChanges())
        {
            await dataContext.SaveChangesAsync();
        }
    }

    public static async Task InsertProductionData(this DataContext dataContext, SuperAdminSettings settings, IPasswordHasher passwordHasher)
    {
        var isSuperAdminCreated = await dataContext.User.AnyAsync(x => x.Username == settings.Email);

        if (isSuperAdminCreated)
        {
            return;
        }

        var adminRole = await dataContext.EnsureAdminRoleCreated();

        var hashedPassword = passwordHasher.HashPassword(settings.Password);
        var newSuperAdmin = new User { Username = settings.Email,
            Password = hashedPassword,
            Roles = [new UserRole{Role = adminRole}]
        };

        dataContext.Add(newSuperAdmin);
        
        if (dataContext.ChangeTracker.HasChanges())
        {
            await dataContext.SaveChangesAsync();
        }
    }
    
    private static async Task<Role?> EnsureAdminRoleCreated(this DataContext dataContext)
    {
        var adminRole = await dataContext.Role.FirstOrDefaultAsync(x => x.Name == PolicyConstants.AdminRole);
        
        if (adminRole != null)
        {
            return adminRole;
        }
        
        adminRole = new Role { Name = PolicyConstants.AdminRole };
        
        dataContext.Add(adminRole);
        
        await dataContext.SaveChangesAsync();
        
        return await dataContext.Role.FirstOrDefaultAsync(x => x.Name == adminRole.Name);
    }

    public static async Task<Settings> ConfigureSettingsTable(this DataContext dataContext, bool shouldSimulate, bool shouldOverwrite)
    {
        var settings = await dataContext.Settings.OrderBy(x => x.Id).FirstOrDefaultAsync();

        if (settings != null)
        {
            if (settings.ShouldSimulate == shouldSimulate && settings.ShouldOverwrite == shouldOverwrite)
            {
                return settings;
            }
            
            settings.ShouldSimulate = shouldSimulate;
            settings.ShouldOverwrite = shouldOverwrite;
            dataContext.Update(settings);
        }
        else
        {
            dataContext.Add(new Settings{ ShouldSimulate = shouldSimulate, ShouldOverwrite = shouldOverwrite});
        }

        if (dataContext.ChangeTracker.HasChanges())
        {
            await dataContext.SaveChangesAsync();
        }
        
        return await dataContext.Settings.OrderBy(x => x.Id).FirstAsync();
    }
}