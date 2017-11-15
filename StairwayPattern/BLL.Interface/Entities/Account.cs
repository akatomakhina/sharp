using System;

namespace BLL.Interface.Entities
{
    public abstract class Account
    {
        #region Fields
        private string accountNumber;
        private string name;
        private decimal balance;
        private int benefitPoints;
        #endregion

        #region Properties
        public string AccountNumber
        {
            get => accountNumber;
            private set => accountNumber = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException(nameof(accountNumber));
        }

        public string Name
        {
            get => name;
            private set => name = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException(nameof(name));
        }

        public decimal Balance
        {
            get => balance;
            private set => balance = IsValidBalance(value) ? value : throw new ArgumentException(nameof(balance));
        }

        public int BenefitPoints
        {
            get => benefitPoints;
            private set => benefitPoints = value >= 0 ? value : throw new ArgumentException(nameof(benefitPoints));
        }
        #endregion

        #region Ctors
        protected Account(string accountNumber, string name)
        {
            this.AccountNumber = accountNumber;
            this.Name = name;
        }

        protected Account(string accountNumber, string name, decimal balance) : this(accountNumber, name)
        {
            this.Balance = balance;
        }

        protected Account(string accountNumber, string name, decimal balance, int points) : this(accountNumber, name, balance)
        {
            this.BenefitPoints = points;
        }
        #endregion

        #region Public Methods
        public void Deposit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }

            Balance += amount;

            benefitPoints += CalculateBenifitPoints(amount);
        }

        public void Whithdraw(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }

            Balance -= amount;

            benefitPoints -= CalculateBenifitPoints(amount);
        }
        #endregion

        #region Abstract Methods
        protected abstract bool IsValidBalance(decimal value);

        protected abstract int CalculateBenifitPoints(decimal amount);
        #endregion

        #region overriding ToSrting()
        public override string ToString()
            => $"#{this.AccountNumber} Name: {this.Name} Balance: {this.Balance} Benefit points: {this.benefitPoints}";
        #endregion
    }
}