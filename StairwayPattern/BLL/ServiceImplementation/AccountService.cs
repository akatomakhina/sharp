using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using BLL.Mappers;
using DAL.Interface.Interfaces;

namespace BLL.ServiceImplementation
{
    public class AccountService : IAccountService
    {
        private IRepository repository;

        public AccountService(IRepository repository) 
        {
            this.repository = repository;
        }

        public void OpenAccount(string name, AccountType accountType, IAccountNumberCreateService creator)
        {
            string accountNumber = creator.Create(GetNumberOfAccounts());

            Type type = Type.GetType($"BLL.Interface.Entities.{accountType}Account,BLL.Interface");//??

            Account account = (Account)Activator.CreateInstance(type, new object[] { accountNumber, name });

            // Account account = null;
            //switch (accountType)
            //{
            //    case AccountType.Base:
            //        account = new BaseAccount(accountNumber, name);
            //        break;
            //    case AccountType.Silver:
            //        account = new SilverAccount(accountNumber, name);
            //        break;
            //}

            repository.Create(account.ToAccountDto());
        }

        public void DepositAccount(string accountNumber, decimal amount)
        {
            Account account = repository.GetByNumber(accountNumber).ToAccount();

            account.Deposit(amount);

            repository.Update(account.ToAccountDto());
        }

        public void WithdrawAccount(string accountNumber, decimal amount)
        {
            Account account = repository.GetByNumber(accountNumber).ToAccount();

            account.Whithdraw(amount);

            repository.Update(account.ToAccountDto());
        }

        public IEnumerable<Account> GetAllAccounts() => repository.GetAllAccounts().Select(item => item.ToAccount());

        private int GetNumberOfAccounts() => GetAllAccounts().ToList().Count;
    }
}
