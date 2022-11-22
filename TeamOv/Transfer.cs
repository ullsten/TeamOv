﻿using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TeamOv
{
    public class Transfer : BankAccount
    {

        public void Deposit(decimal amount)
        {
            if (bankAccounts.Count < 1)
            {
                Console.WriteLine("No accounts found to deposit money into");
            }
            Console.WriteLine("Enter amount to deposit: ");
            decimal Amount = decimal.Parse(Console.ReadLine());
            if (Amount <= 0)
            {
                Console.WriteLine("Amount cant be less then 1");
            }
            else
            {
                Balance += Amount;
                //Amount += Balance;
                bankAccounts.Add(this);
            }
        }
        public void Withdraw(decimal amount)
        {
            Balance -= amount;

        }
        public void CheckBalance()
        {
            Console.WriteLine($"Account balance: {Balance}.{Currency.SEK}");
        }

        public void TransferAmount(string loggedInCustomer)
        {
            string input = "Y";
            while (input == "Y" || input == "y")
            {
                CustomerMenu.PrintAccountInfo(loggedInCustomer);
                Console.Write("Enter accountID to transfer from: ");
                int fromAccount = int.Parse(Console.ReadLine());
                Console.Write("Transfer to: ");
                int toAccount = int.Parse(Console.ReadLine());
                Console.Write("Please enter amount: ");
                decimal amount;
                if (decimal.TryParse(Console.ReadLine(), out amount))
                {
                    if (amount <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Transfer amount must be positive");
                        Console.ResetColor();
                    }
                    if (Balance < amount)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Amount cant be more then balance");
                        Console.ResetColor();
                    }
                    else
                    {
                        var FromAccount = bankAccounts.Find(a => a.AccountId == fromAccount);
                        var ToAccount = bankAccounts.Find(a => a.AccountId == toAccount);
                        FromAccount.Balance -= amount;
                        ToAccount.Balance += amount;

                        Console.WriteLine();
                        Console.WriteLine($"You have: {FromAccount.Balance}{Currency.SEK} left on your {FromAccount.AccountName}");
                        Console.WriteLine($"You have: {ToAccount.Balance}{Currency.SEK} left on your {ToAccount.AccountName}");
                        Console.WriteLine();
                    }
                //decimal amount = decimal.Parse(Console.ReadLine());
                }
                Console.WriteLine("Do you want to try again? Y/N?");
                input = Console.ReadLine();
            }
        }
        //public void GetAccount(int FromAccountId, int ToAccountId)
        //{
        //    bankAccounts.Find(a => a.AccountId == FromAccountId);
        //    bankAccounts.Find(a => a.AccountId == ToAccountId);
        //}
    }
}
