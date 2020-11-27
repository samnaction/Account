using System;

namespace AccountLib
{
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
    }
}
