using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAllAccounts();
        void OpenAccount(string name, AccountType accountType, IAccountNumberCreateService creator);
        void DepositAccount(string accountNumber, decimal amount);
        void WithdrawAccount(string accountNumber, decimal amount);
    }
}