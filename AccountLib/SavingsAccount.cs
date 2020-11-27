namespace AccountLib
{
    using System;

    public class SavingsAccount
    {
        public double Deposit(double amountToBeDeposited)
        {
            double currentBalance = GetBalance();

            currentBalance = currentBalance + amountToBeDeposited;

            UpdateBalanceInDB(currentBalance);

            return currentBalance;
        }

        public virtual void UpdateBalanceInDB(double balance)
        {
            Console.WriteLine("Balance updated in Database ...");
        }

        public virtual double GetBalance()
        {
            Console.WriteLine("Current Balance fetched from DB Server ...");
            return 0.0;
        }

        public double Withdraw(double amountToBeWithdrawn)
        {
            double currentBalance = GetBalance();

            if (currentBalance < amountToBeWithdrawn)
            {
                throw new Exception("Insufficient Balance");
            }

            currentBalance = currentBalance - amountToBeWithdrawn;

            UpdateBalanceInDB(currentBalance);

            return currentBalance;
        }
    }
}
