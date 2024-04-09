﻿using Testcontainers.PostgreSql;

namespace FinanceManager.Tests.Integration.Setup;

public class SharedContainerFixture : IAsyncLifetime
{
    public static PostgreSqlContainer? DatabaseContainer { get; private set; }

    public async Task InitializeAsync()
    {
        if (DatabaseContainer != null)
        {
            return;
        }
        
        DatabaseContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .Build();

        await DatabaseContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        if (DatabaseContainer == null)
        {
            return;
        }

        await DatabaseContainer.StopAsync();
        DatabaseContainer = null;
    }
}