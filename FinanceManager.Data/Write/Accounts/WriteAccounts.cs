﻿using System.Security.Authentication;
using FinanceManager.Common.Entities;
using FinanceManager.Common.Services;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Data.Write.Accounts;

public interface IWriteAccounts : ICreateEntity<Account>
{
    Task<bool> EditAsync(Guid userId, string accountName);
}

public sealed class WriteAccounts(DataContext db, IUserContextService userContextService) : IWriteAccounts
{
    private readonly Guid? _userId = userContextService.GetCurrentUserId();
    public async Task<bool> CreateAsync(Account entity)
    {
        if (_userId == null)
        {
            //TODO: Integrate logger.
            throw new AuthenticationException("Error accessing User Context");
        }
        
        entity.User.Id = _userId.Value;
        
        db.Account.Add(entity);
        
        var saveResult = await db.SaveChangesAsync().ConfigureAwait(false);

        return saveResult > 0;
    }
    
    public async Task<bool> EditAsync(Guid userId, string accountName)
    {
        var account = await db.Account.FirstOrDefaultAsync(x => x.Id == userId && x.User.Id == _userId).ConfigureAwait(false);
         
        if (account == null)
        {
            //TODO: Integrate logger.
            throw new InvalidOperationException("Account does not exist");
        }
        
        account.Name = accountName;

        var saveResult = await db.SaveChangesAsync().ConfigureAwait(false);

        return saveResult > 0;
    }
}