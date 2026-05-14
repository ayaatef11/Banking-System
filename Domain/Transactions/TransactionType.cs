using System.ComponentModel.DataAnnotations;

namespace Domain.Transactions;
 
public enum TransactionType
{     
    [Display(Name = "Deposit")]
    Deposit = 1, 
    [Display(Name = "Withdraw")]
    Withdraw = 2,
    [Display(Name = "Transfer")]
    Transfer = 3
}
