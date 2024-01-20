using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceManager.Core.DataEntities;

public class RecurringTransaction : BaseModel
{
    [Required]
    public decimal Amount { get; set; }
    
    [Required]
    public string TransactionName { get; set; }
    
    /// <summary>
    /// Specifies the number of days between transactions.
    /// </summary>
    [Required]
    [Range(1, 28)]
    public int TransactionInterval { get; set; }
    
    public DateTime? LastTransactionDate { get; set; }
    
    public DateTime NextTransactionDate { get; set; }
    
    public virtual Account RecipientAccountId { get; set; }
    
    public virtual Guid? SenderAccountId { get; set; }
}