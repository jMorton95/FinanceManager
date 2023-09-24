﻿using Microsoft.EntityFrameworkCore;

namespace CrabInABucket.Models;

public class CrabDbContext : DbContext
{
    public CrabDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> User => Set<User>();
    public DbSet<Role> Role => Set<Role>();
    public DbSet<UserRole> UserRole => Set<UserRole>();
    public DbSet<Account> Account => Set<Account>();
    public DbSet<BudgetTransaction> BudgetTransaction => Set<BudgetTransaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Username);
    }

    public override int SaveChanges()
    {
        var addedEntities = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added)
            .Select(x => x.Entity as BaseModel);

        foreach (var entity in addedEntities)
        {
            if (entity == null) continue;
            entity.CreatedDate = DateTime.UtcNow; 
            entity.UpdatedDate = DateTime.UtcNow; 
        }
    
        var modifiedEntities = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Modified)
            .Select(x => x.Entity as BaseModel);

        foreach (var entity in modifiedEntities)
        {
            if (entity != null)
            {
                entity.UpdatedDate = DateTime.UtcNow;
            }
        }

        return base.SaveChanges();
    }

}