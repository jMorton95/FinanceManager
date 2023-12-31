using FinanceManager.Core.DataEntities;

namespace FinanceManager.Data.Read;

public interface IRead<TEntity> where TEntity : BaseModel
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(Guid id);
}