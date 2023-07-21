using System;
using System.Collections.Generic;

namespace BankingConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the banking service with the in-memory repository (you can change it to SQLite implementation if needed).
            IBankingService bankingService = new BankingService(new InMemoryBankingRepository());

            // Display the main menu to the user.
            while (true)
            {
                Console.WriteLine("Banking Console Application");
                Console.WriteLine("==========================");
                Console.WriteLine("1. Create Bank Account");
                Console.WriteLine("2. Get All Bank Accounts");
                Console.WriteLine("3. Get Bank Account by ID");
                Console.WriteLine("4. Update Bank Account");
                Console.WriteLine("5. Close Bank Account");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice (1-6): ");
                string choiceStr = Console.ReadLine();
                
                if (int.TryParse(choiceStr, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            CreateBankAccount(bankingService);
                            break;
                        case 2:
                            GetAllBankAccounts(bankingService);
                            break;
                        case 3:
                            GetBankAccountById(bankingService);
                            break;
                        case 4:
                            UpdateBankAccount(bankingService);
                            break;
                        case 5:
                            CloseBankAccount(bankingService);
                            break;
                        case 6:
                            Console.WriteLine("Goodbye!");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

                Console.WriteLine();
            }
        }

        static void CreateBankAccount(IBankingService bankingService)
        {
            Console.WriteLine("Creating a new bank account...");
            
            Console.Write("Enter Account Number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter Account Holder Name: ");
            string accountHolderName = Console.ReadLine();

            // Validate the balance input.
            decimal balance = 0;
            while (true)
            {
                Console.Write("Enter Balance: ");
                string balanceStr = Console.ReadLine();

                if (decimal.TryParse(balanceStr, out balance))
                {
                    break;
                }

                Console.WriteLine("Invalid balance value. Please enter a valid number.");
            }

            // Validate IsActive input.
            bool isActive;
            while (true)
            {
                Console.Write("Is Active? (true/false): ");
                string isActiveStr = Console.ReadLine();

                if (bool.TryParse(isActiveStr, out isActive))
                {
                    break;
                }

                Console.WriteLine("Invalid input. Please enter 'true' or 'false'.");
            }

            // Validate Account Type input.
            AccountType accountType;
            while (true)
            {
                Console.WriteLine("Select Account Type:");
                Console.WriteLine("1. Savings");
                Console.WriteLine("2. Checking");
                Console.WriteLine("3. Loan");
                Console.WriteLine("4. Credit Card");
                Console.WriteLine("5. Fixed Deposit");
                Console.Write("Enter your choice (1-5): ");
                string accountTypeStr = Console.ReadLine();

                if (Enum.TryParse(accountTypeStr, out accountType) && Enum.IsDefined(typeof(AccountType), accountType))
                {
                    break;
                }

                Console.WriteLine("Invalid choice. Please enter a valid number (1-5).");
            }

            // Create the new bank account.
            BankAccount newAccount = new BankAccount
            {
                AccountNumber = accountNumber,
                AccountHolderName = accountHolderName,
                Balance = balance,
                IsActive = isActive,
                AccountType = accountType
            };

            bankingService.CreateAccount(newAccount);
            Console.WriteLine("Bank account created successfully.");
        }

        static void GetAllBankAccounts(IBankingService bankingService)
        {
            Console.WriteLine("Fetching all bank accounts...");
            
            IEnumerable<BankAccount> bankAccounts = bankingService.GetAllAccounts();

            if (bankAccounts != null && bankAccounts.Count() > 0)
            {
                foreach (var account in bankAccounts)
                {
                    Console.WriteLine($"Account ID: {account.Id}, Account Number: {account.AccountNumber}, " +
                                      $"Account Holder Name: {account.AccountHolderName}, Balance: {account.Balance}, " +
                                      $"Is Active: {account.IsActive}, Account Type: {account.AccountType}");
                }
            }
            else
            {
                Console.WriteLine("No bank accounts found.");
            }
        }

        static void GetBankAccountById(IBankingService bankingService)
        {
            Console.Write("Enter the Account ID to retrieve: ");
            string accountIdStr = Console.ReadLine();

            if (int.TryParse(accountIdStr, out int accountId))
            {
                BankAccount account = bankingService.GetAccountById(accountId);

                if (account != null)
                {
                    Console.WriteLine($"Account ID: {account.Id}, Account Number: {account.AccountNumber}, " +
                                      $"Account Holder Name: {account.AccountHolderName}, Balance: {account.Balance}, " +
                                      $"Is Active: {account.IsActive}, Account Type: {account.AccountType}");
                }
                else
                {
                    Console.WriteLine("Bank account not found with the specified ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        static void UpdateBankAccount(IBankingService bankingService)
        {
            Console.Write("Enter the Account ID to update: ");
            string accountIdStr = Console.ReadLine();

            if (int.TryParse(accountIdStr, out int accountId))
            {
                BankAccount account = bankingService.GetAccountById(accountId);

                if (account != null)
                {
                    Console.WriteLine($"Account ID: {account.Id}, Account Number: {account.AccountNumber}, " +
                                      $"Account Holder Name: {account.AccountHolderName}, Balance: {account.Balance}, " +
                                      $"Is Active: {account.IsActive}, Account Type: {account.AccountType}");

                    Console.Write("Enter new Account Holder Name: ");
                    string newAccountHolderName = Console.ReadLine();

                    // Update the account holder name.
                    account.AccountHolderName = newAccountHolderName;
                    bankingService.UpdateAccount(account);

                    Console.WriteLine("Bank account updated successfully.");
                }
                else
                {
                    Console.WriteLine("Bank account not found with the specified ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        static void CloseBankAccount(IBankingService bankingService)
        {
            Console.Write("Enter the Account ID to close: ");
            string accountIdStr = Console.ReadLine();

            if (int.TryParse(accountIdStr, out int accountId))
            {
                try
                {
                    bankingService.CloseAccount(accountId);
                    Console.WriteLine("Bank account closed successfully.");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }
}
