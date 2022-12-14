using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamOv
{
    public class SavingAccount : BankAccount
    {
        private readonly decimal interestRate1 = 0.0m;
        private readonly decimal interestRate2 = 0.8m;
        private readonly decimal interestRate3 = 1.3m;
        private readonly decimal givingRate = 0.0m;
        public decimal InterestRate(decimal amount, decimal givingRate)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (amount < 10000)
            {
                Console.WriteLine("Your interest rate: " + interestRate1 + "%");
            }
            else if (amount >= 10000 && amount <= 50000)
            {
                Console.WriteLine("Your interest rate: " + interestRate2 + "%");
            }
            else if(amount >= 50000)
            {
                Console.WriteLine("Your interest rate: " + interestRate3 + "%");
            }
            return givingRate;
        }
        public void ChosenSavingAccount(string loggedInCustomer)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Please enter a name to your new account: ");
            var accountname = Console.ReadLine();
            string accountNumber = BankAccount.GenerateBankAccountNumber();
            string owner = loggedInCustomer;
            BankAccount.bankAccounts.Add(new BankAccount(accountNumber, accountname, owner, 0, "SEK", true)); //wrong accountname
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{accountname} account {accountNumber} created");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Do you want to make a first deposit? (Yes/No)");
            var firstDeposit = Console.ReadLine();
            if (firstDeposit.ToLower() == "y" || firstDeposit.ToLower() == "yes")
            {
                decimal amount;
                Console.Write("Enter amount: ");
                if (decimal.TryParse(Console.ReadLine(), out amount))
                {
                    string deposit = accountNumber;
                    var Deposit = BankAccount.bankAccounts.Find(a => a.AccountNumber == deposit);
                    Deposit.Balance += amount;
                    InterestRate(amount, givingRate);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Successful deposit {amount.ToString("N" + 2)} {Deposit.Currency}.");
                    Transactionservice.transactionslist.Add($"{DateTime.Now} Depsoit: {amount.ToString("N" + 2)} {Deposit.Currency} to account number: {Deposit.AccountNumber}");
                    Console.WriteLine();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("It´s ok, you can come back another time.");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("You can do it another time instead.");
            }

        }

    }
    
}
