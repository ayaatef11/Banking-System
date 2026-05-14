using System.ComponentModel.DataAnnotations;

namespace Domain.Accounts;
 
public enum AccountType
{ 
    [Display(Name = "Savings Account")]
    Savings = 1, 
    [Display(Name = "Checking Account")]
    Checking = 2, 
    [Display(Name = "Business Account")]
    Business = 3
}
