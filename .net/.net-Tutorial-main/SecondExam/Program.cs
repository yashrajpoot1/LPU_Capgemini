using System;
using System.Collections.Generic;

namespace DigitalPettyCashLedger
{
    // Interface for transaction reporting
    public interface IReportable
    {
        string GetSummary();
    }

    // Abstract base class for all financial transactions
    public abstract class Transaction : IReportable
    {
        public int Id { get; protected set; }
        public DateTime Date { get; protected set; }
        public decimal Amount { get; protected set; }
        public string Description { get; protected set; }

        protected Transaction(int id, DateTime date, decimal amount, string description)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty");

            Id = id;
            Date = date;
            Amount = amount;
            Description = description;
        }

        // Each transaction type will provide its own summary format
        public abstract string GetSummary();
    }

    // Expense transaction with Category property
    public class ExpenseTransaction : Transaction
    {
        public string Category { get; private set; }

        public ExpenseTransaction(int id, DateTime date, decimal amount, string description, string category)
            : base(id, date, amount, description)
        {
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Category cannot be empty");
            Category = category;
        }

        public override string GetSummary()
        {
            return $"[EXPENSE] {Date:yyyy-MM-dd} | ${Amount:F2} | {Category} | {Description}";
        }
    }

    // Income transaction with Source property
    public class IncomeTransaction : Transaction
    {
        public string Source { get; private set; }

        public IncomeTransaction(int id, DateTime date, decimal amount, string description, string source)
            : base(id, date, amount, description)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentException("Source cannot be empty");
            Source = source;
        }

        public override string GetSummary()
        {
            return $"[INCOME] {Date:yyyy-MM-dd} | ${Amount:F2} | From: {Source} | {Description}";
        }
    }

    // Generic ledger class for type-safe transaction management
    public class Ledger<T> where T : Transaction
    {
        private List<T> transactions;

        public Ledger()
        {
            transactions = new List<T>();
        }

        // Adds a transaction entry to the ledger
        public void AddEntry(T entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry), "Transaction entry cannot be null");

            transactions.Add(entry);
        }

        // Gets transactions for a specific date
        public List<T> GetTransactionsByDate(DateTime date)
        {
            var result = new List<T>();
            
            foreach (var transaction in transactions)
            {
                if (transaction.Date.Date == date.Date)
                {
                    result.Add(transaction);
                }
            }
            
            return result;
        }

        // Calculates total amount without using LINQ
        public decimal CalculateTotal()
        {
            decimal total = 0;
            
            foreach (var transaction in transactions)
            {
                total += transaction.Amount;
            }
            
            return total;
        }

        // Returns all transactions in the ledger
        public List<T> GetAllTransactions()
        {
            return new List<T>(transactions);
        }

        public int Count
        {
            get { return transactions.Count; }
        }
    }

    // Main Program Class
    class Program
    {
        private static int nextId = 1;
        private static Ledger<IncomeTransaction> incomeLedger = new Ledger<IncomeTransaction>();
        private static Ledger<ExpenseTransaction> expenseLedger = new Ledger<ExpenseTransaction>();

        static void Main(string[] args)
        {
            Console.WriteLine("=== Digital Petty Cash Ledger System ===\n");
            
            // Initialize with sample data as per requirements
            InitializeSampleData();
            
            bool running = true;
            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        AddIncomeTransaction();
                        break;
                    case "2":
                        AddExpenseTransaction();
                        break;
                    case "3":
                        ViewLedgerSummary();
                        break;
                    case "4":
                        ViewAllTransactions();
                        break;
                    case "5":
                        ViewTransactionsByDate();
                        break;
                    case "6":
                        running = false;
                        Console.WriteLine("Thank you for using Digital Petty Cash Ledger!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        static void InitializeSampleData()
        {
            // Step 1 & 2: Create Ledger<IncomeTransaction> and record $500 from "Main Cash"
            var fundReplenishment = new IncomeTransaction(
                id: nextId++,
                date: DateTime.Now,
                amount: 500.00m,
                description: "Monthly petty cash replenishment",
                source: "Main Cash"
            );
            incomeLedger.AddEntry(fundReplenishment);
            
            // Step 3 & 4: Create Ledger<ExpenseTransaction> and record $20 for "Stationery" and $15 for "Team Snacks"
            var stationeryExpense = new ExpenseTransaction(
                id: nextId++,
                date: DateTime.Now,
                amount: 20.00m,
                description: "Stationery",
                category: "Office"
            );
            
            var teamSnacksExpense = new ExpenseTransaction(
                id: nextId++,
                date: DateTime.Now,
                amount: 15.00m,
                description: "Team Snacks",
                category: "Food"
            );
            
            expenseLedger.AddEntry(stationeryExpense);
            expenseLedger.AddEntry(teamSnacksExpense);
            
            Console.WriteLine("✓ Sample data loaded as per use case:");
            Console.WriteLine("  - $500 replenishment from Main Cash");
            Console.WriteLine("  - $20 expense for Stationery (Office category)");
            Console.WriteLine("  - $15 expense for Team Snacks (Food category)\n");
        }

        static void ShowMenu()
        {
            Console.WriteLine("=== MENU ===");
            Console.WriteLine("1. Add Income Transaction");
            Console.WriteLine("2. Add Expense Transaction");
            Console.WriteLine("3. View Ledger Summary");
            Console.WriteLine("4. View All Transactions");
            Console.WriteLine("5. View Transactions by Date");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice (1-6): ");
        }

        static void AddIncomeTransaction()
        {
            Console.WriteLine("\n=== ADD INCOME TRANSACTION ===");
            
            try
            {
                Console.Write("Enter amount: $");
                decimal amount = decimal.Parse(Console.ReadLine());
                
                Console.Write("Enter description: ");
                string description = Console.ReadLine();
                
                Console.Write("Enter source (e.g., Main Cash, Bank Transfer): ");
                string source = Console.ReadLine();
                
                var income = new IncomeTransaction(nextId++, DateTime.Now, amount, description, source);
                incomeLedger.AddEntry(income);
                
                Console.WriteLine($"✓ Income transaction added successfully! ID: {income.Id}");
                Console.WriteLine($"  Amount: ${income.Amount:F2} | Source: {income.Source} | Description: {income.Description}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n");
            }
        }

        static void AddExpenseTransaction()
        {
            Console.WriteLine("\n=== ADD EXPENSE TRANSACTION ===");
            
            try
            {
                Console.Write("Enter amount: $");
                decimal amount = decimal.Parse(Console.ReadLine());
                
                Console.Write("Enter description: ");
                string description = Console.ReadLine();
                
                Console.Write("Enter category (e.g., Office, Travel, Food): ");
                string category = Console.ReadLine();
                
                var expense = new ExpenseTransaction(nextId++, DateTime.Now, amount, description, category);
                expenseLedger.AddEntry(expense);
                
                Console.WriteLine($"✓ Expense transaction added successfully! ID: {expense.Id}");
                Console.WriteLine($"  Amount: ${expense.Amount:F2} | Category: {expense.Category} | Description: {expense.Description}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n");
            }
        }

        static void ViewLedgerSummary()
        {
            Console.WriteLine("\n=== LEDGER SUMMARY ===");
            
            // Step 5: Use generic method to display totals from both ledgers
            decimal totalIncome = incomeLedger.CalculateTotal();
            decimal totalExpenses = expenseLedger.CalculateTotal();
            
            Console.WriteLine($"Total Income: ${totalIncome:F2}");
            Console.WriteLine($"Total Expenses: ${totalExpenses:F2}");
            
            // Step 6: Calculate Net Balance (Total Income - Total Expenses)
            decimal netBalance = totalIncome - totalExpenses;
            Console.WriteLine($"Net Balance: ${netBalance:F2}");
            
            Console.WriteLine($"Number of Income Transactions: {incomeLedger.Count}");
            Console.WriteLine($"Number of Expense Transactions: {expenseLedger.Count}");
            
            // Verify expected results from use case
            Console.WriteLine("\n--- Use Case Verification ---");
            Console.WriteLine($"Expected total spent: $35.00 | Actual: ${totalExpenses:F2} | {(totalExpenses == 35.00m ? "✓ PASS" : "✗ FAIL")}");
            Console.WriteLine($"Expected total received: $500.00 | Actual: ${totalIncome:F2} | {(totalIncome >= 500.00m ? "✓ PASS" : "✗ FAIL")}");
            Console.WriteLine();
        }

        static void ViewAllTransactions()
        {
            Console.WriteLine("\n=== ALL TRANSACTIONS (POLYMORPHIC OUTPUT) ===");
            Console.WriteLine("Loop iterating through List<Transaction> calling GetSummary():\n");
            
            var allTransactions = new List<Transaction>();
            
            // Add all transactions to demonstrate polymorphism
            foreach (var entry in incomeLedger.GetAllTransactions())
            {
                allTransactions.Add(entry);
            }
            
            foreach (var entry in expenseLedger.GetAllTransactions())
            {
                allTransactions.Add(entry);
            }
            
            if (allTransactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
            }
            else
            {
                // Display unique details for both Income and Expenses using polymorphism
                foreach (var entry in allTransactions)
                {
                    Console.WriteLine(entry.GetSummary());
                }
            }
            Console.WriteLine();
        }

        static void ViewTransactionsByDate()
        {
            Console.WriteLine("\n=== VIEW TRANSACTIONS BY DATE ===");
            
            try
            {
                Console.Write("Enter date (yyyy-mm-dd) or press Enter for today: ");
                string dateInput = Console.ReadLine();
                
                DateTime searchDate;
                if (string.IsNullOrWhiteSpace(dateInput))
                {
                    searchDate = DateTime.Now;
                }
                else
                {
                    searchDate = DateTime.Parse(dateInput);
                }
                
                Console.WriteLine($"\nTransactions for {searchDate:yyyy-MM-dd}:");
                
                var incomeForDate = incomeLedger.GetTransactionsByDate(searchDate);
                var expensesForDate = expenseLedger.GetTransactionsByDate(searchDate);
                
                bool hasTransactions = false;
                
                foreach (var income in incomeForDate)
                {
                    Console.WriteLine(income.GetSummary());
                    hasTransactions = true;
                }
                
                foreach (var expense in expensesForDate)
                {
                    Console.WriteLine(expense.GetSummary());
                    hasTransactions = true;
                }
                
                if (!hasTransactions)
                {
                    Console.WriteLine("No transactions found for this date.");
                }
                
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n");
            }
        }
    }
}