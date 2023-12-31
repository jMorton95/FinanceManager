﻿using FinanceManager.Core.DataEntities;
using FinanceManager.Core.Middleware.UserContext;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Data;

public class DataContext(DbContextOptions options, IUserContextService userContextService) : DbContext(options)
{
    public DbSet<User> User => Set<User>();
    public DbSet<Role> Role => Set<Role>();
    public DbSet<UserRole> UserRole => Set<UserRole>();
    public DbSet<Account> Account => Set<Account>();
    public DbSet<BudgetTransaction> BudgetTransaction => Set<BudgetTransaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        modelBuilder.Entity<Role>().HasIndex(r => r.Name).IsUnique();
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var userId = userContextService.CurrentUser?.UserAccessToken?.UserId;
        
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is not BaseModel entity ) continue;

            if (userId != null) entity.EditedBy = userId;
            entity.UpdatedDate = DateTime.UtcNow;

            switch (entry)
            {
                case { State: EntityState.Added }:
                    if (userId != null) entity.CreatedBy = userId;
                    break;
                
                case { State: EntityState.Modified }:
                    entity.RowVersion += 1;
                    break;
            }
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }


}