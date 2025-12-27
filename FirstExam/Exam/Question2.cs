using System;

public class RetailProfitAnalyzer
{
    // Immutable record-like structure
    public readonly struct SalesRecord
    {
        public readonly string InvoiceNumber;
        public readonly string ClientName;
        public readonly string ProductName;
        public readonly int ItemCount;
        public readonly decimal BuyPrice;
        public readonly decimal SellPrice;
        public readonly string ProfitStatus;
        public readonly decimal ProfitLossValue;
        public readonly decimal MarginPercentage;

        public SalesRecord(string invoice, string client, string product, int count, 
                          decimal buy, decimal sell, string status, decimal value, decimal margin)
        {
            InvoiceNumber = invoice;
            ClientName = client;
            ProductName = product;
            ItemCount = count;
            BuyPrice = buy;
            SellPrice = sell;
            ProfitStatus = status;
            ProfitLossValue = value;
            MarginPercentage = margin;
        }

        // Factory method for creating calculated record
        public static SalesRecord CreateWithCalculations(string invoice, string client, string product, 
                                                        int count, decimal buy, decimal sell)
        {
            var (status, value, margin) = CalculateProfitMetrics(buy, sell);
            return new SalesRecord(invoice, client, product, count, buy, sell, status, value, margin);
        }

        // Pure function for profit calculations
        private static (string status, decimal value, decimal margin) CalculateProfitMetrics(decimal buy, decimal sell)
        {
            if (sell > buy)
                return ("PROFIT", sell - buy, ((sell - buy) / buy) * 100);
            else if (sell < buy)
                return ("LOSS", buy - sell, ((buy - sell) / buy) * 100);
            else
                return ("BREAK-EVEN", 0m, 0m);
        }

        public SalesRecord Recalculate()
        {
            var (status, value, margin) = CalculateProfitMetrics(BuyPrice, SellPrice);
            return new SalesRecord(InvoiceNumber, ClientName, ProductName, ItemCount, 
                                 BuyPrice, SellPrice, status, value, margin);
        }
    }

    // Application state
    private static SalesRecord? storedRecord = null;

    // Main application entry
    public static void Main()
    {
        RunMainLoop();
    }

    private static void RunMainLoop()
    {
        bool continueApp = true;
        while (continueApp)
        {
            continueApp = DisplayMenuAndProcess();
        }
    }

    private static bool DisplayMenuAndProcess()
    {
        ShowApplicationHeader();
        int selection = GetUserMenuChoice();
        return ProcessUserSelection(selection);
    }

    private static void ShowApplicationHeader()
    {
        Console.WriteLine("================== QuickMart Traders ==================");
        Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
        Console.WriteLine("2. View Last Transaction");
        Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
        Console.WriteLine("4. Exit");
    }

    private static int GetUserMenuChoice()
    {
        while (true)
        {
            Console.WriteLine("Enter your option:");
            string input = Console.ReadLine() ?? "";
            
            if (int.TryParse(input, out int choice) && IsValidMenuOption(choice))
                return choice;
                
            Console.WriteLine("Invalid choice. Please select 1, 2, 3, or 4.");
        }
    }

    private static bool IsValidMenuOption(int option) => option >= 1 && option <= 4;

    private static bool ProcessUserSelection(int selection)
    {
        return selection switch
        {
            1 => ExecuteTransactionCreation(),
            2 => ExecuteTransactionDisplay(),
            3 => ExecuteProfitRecalculation(),
            4 => ExecuteApplicationExit(),
            _ => true
        };
    }

    private static bool ExecuteTransactionCreation()
    {
        Console.WriteLine();
        var record = CollectTransactionData()
            .ValidateAndProcess()
            .StoreRecord();
            
        DisplayTransactionResult(record);
        ShowSeparator();
        return true;
    }

    private static TransactionBuilder CollectTransactionData()
    {
        return new TransactionBuilder()
            .WithInvoice(PromptForRequiredText("Enter Invoice No"))
            .WithCustomer(PromptForOptionalText("Enter Customer Name"))
            .WithProduct(PromptForOptionalText("Enter Item Name"))
            .WithQuantity(PromptForPositiveInteger("Enter Quantity"))
            .WithPurchaseAmount(PromptForPositiveDecimal("Enter Purchase Amount (total)"))
            .WithSellingAmount(PromptForNonNegativeDecimal("Enter Selling Amount (total)"));
    }

    private static bool ExecuteTransactionDisplay()
    {
        Console.WriteLine();
        if (HasStoredRecord())
            DisplayStoredTransaction();
        else
            ShowNoTransactionMessage();
        ShowSeparator();
        return true;
    }

    private static bool ExecuteProfitRecalculation()
    {
        Console.WriteLine();
        if (HasStoredRecord())
        {
            storedRecord = storedRecord.Value.Recalculate();
            DisplayCalculationResults(storedRecord.Value);
        }
        else
            ShowNoTransactionMessage();
        ShowSeparator();
        return true;
    }

    private static bool ExecuteApplicationExit()
    {
        Console.WriteLine("Thank you. Application closed normally.");
        return false;
    }

    // Helper methods
    private static bool HasStoredRecord() => storedRecord.HasValue;

    private static void ShowNoTransactionMessage()
    {
        Console.WriteLine("No transaction available. Please create a new transaction first.");
    }

    private static void ShowSeparator()
    {
        Console.WriteLine("------------------------------------------------------");
    }

    private static void DisplayStoredTransaction()
    {
        var record = storedRecord.Value;
        Console.WriteLine("-------------- Last Transaction --------------");
        Console.WriteLine($"InvoiceNo: {record.InvoiceNumber}");
        Console.WriteLine($"Customer: {record.ClientName}");
        Console.WriteLine($"Item: {record.ProductName}");
        Console.WriteLine($"Quantity: {record.ItemCount}");
        Console.WriteLine($"Purchase Amount: {record.BuyPrice:F2}");
        Console.WriteLine($"Selling Amount: {record.SellPrice:F2}");
        Console.WriteLine($"Status: {record.ProfitStatus}");
        Console.WriteLine($"Profit/Loss Amount: {record.ProfitLossValue:F2}");
        Console.WriteLine($"Profit Margin (%): {record.MarginPercentage:F2}");
        Console.WriteLine("--------------------------------------------");
    }

    private static void DisplayTransactionResult(SalesRecord record)
    {
        Console.WriteLine("Transaction saved successfully.");
        DisplayCalculationResults(record);
    }

    private static void DisplayCalculationResults(SalesRecord record)
    {
        Console.WriteLine($"Status: {record.ProfitStatus}");
        Console.WriteLine($"Profit/Loss Amount: {record.ProfitLossValue:F2}");
        Console.WriteLine($"Profit Margin (%): {record.MarginPercentage:F2}");
    }

    // Input collection methods
    private static string PromptForRequiredText(string prompt)
    {
        while (true)
        {
            Console.WriteLine($"{prompt}:");
            string input = Console.ReadLine()?.Trim() ?? "";
            if (!string.IsNullOrEmpty(input)) return input;
            Console.WriteLine("This field cannot be empty!");
        }
    }

    private static string PromptForOptionalText(string prompt)
    {
        Console.WriteLine($"{prompt}:");
        return Console.ReadLine()?.Trim() ?? "";
    }

    private static int PromptForPositiveInteger(string prompt)
    {
        while (true)
        {
            Console.WriteLine($"{prompt}:");
            if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
                return value;
            Console.WriteLine("Please enter a positive integer!");
        }
    }

    private static decimal PromptForPositiveDecimal(string prompt)
    {
        while (true)
        {
            Console.WriteLine($"{prompt}:");
            if (decimal.TryParse(Console.ReadLine(), out decimal value) && value > 0)
                return value;
            Console.WriteLine("Please enter a positive amount!");
        }
    }

    private static decimal PromptForNonNegativeDecimal(string prompt)
    {
        while (true)
        {
            Console.WriteLine($"{prompt}:");
            if (decimal.TryParse(Console.ReadLine(), out decimal value) && value >= 0)
                return value;
            Console.WriteLine("Please enter a non-negative amount!");
        }
    }

    // Builder pattern for transaction creation
    private class TransactionBuilder
    {
        private string invoice = "";
        private string customer = "";
        private string product = "";
        private int quantity = 0;
        private decimal purchase = 0m;
        private decimal selling = 0m;

        public TransactionBuilder WithInvoice(string value) { invoice = value; return this; }
        public TransactionBuilder WithCustomer(string value) { customer = value; return this; }
        public TransactionBuilder WithProduct(string value) { product = value; return this; }
        public TransactionBuilder WithQuantity(int value) { quantity = value; return this; }
        public TransactionBuilder WithPurchaseAmount(decimal value) { purchase = value; return this; }
        public TransactionBuilder WithSellingAmount(decimal value) { selling = value; return this; }

        public TransactionBuilder ValidateAndProcess()
        {
            // Validation logic could go here
            return this;
        }

        public SalesRecord StoreRecord()
        {
            var record = SalesRecord.CreateWithCalculations(invoice, customer, product, quantity, purchase, selling);
            storedRecord = record;
            return record;
        }
    }
}