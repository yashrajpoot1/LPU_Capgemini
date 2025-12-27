using System;

public class QuickMartProfitCalculator
{
    // Stores one transaction at a time
    public class SaleTransaction
    {
        public string InvoiceNo;
        public string CustomerName;
        public string ItemName;
        public int Quantity;
        public decimal PurchaseAmount;
        public decimal SellingAmount;
        public string ProfitOrLossStatus;
        public decimal ProfitOrLossAmount;
        public decimal ProfitMarginPercent;

        // Calculates status and margin
        public void CalculateProfitDetails()
        {
            if (SellingAmount > PurchaseAmount)
            {
                ProfitOrLossStatus = "PROFIT";
                ProfitOrLossAmount = SellingAmount - PurchaseAmount;
            }
            else if (SellingAmount < PurchaseAmount)
            {
                ProfitOrLossStatus = "LOSS";
                ProfitOrLossAmount = PurchaseAmount - SellingAmount;
            }
            else
            {
                ProfitOrLossStatus = "BREAK-EVEN";
                ProfitOrLossAmount = 0;
            }

            ProfitMarginPercent = PurchaseAmount > 0
                ? (ProfitOrLossAmount / PurchaseAmount) * 100
                : 0;
        }
    }

    static SaleTransaction LastTransaction;
    static bool HasLastTransaction = false;

    public static void Main()
    {
        bool run = true;
        while (run)
        {
            run = ShowMenu();
        }
    }

    static bool ShowMenu()
    {
        Console.WriteLine("\n================= QuickMart Traders =================");
        Console.WriteLine("1. Create New Transaction");
        Console.WriteLine("2. View Last Transaction");
        Console.WriteLine("3. Calculate Profit/Loss");
        Console.WriteLine("4. Exit");
        Console.Write("Choose an option: ");

        string choice = Console.ReadLine();
        Console.WriteLine("----------------------------------------------------");

        switch (choice)
        {
            case "1": CreateTransaction(); return true;
            case "2": ViewTransaction(); return true;
            case "3": RecalculateProfit(); return true;
            case "4": Console.WriteLine("Application Closed. Thank You!"); return false;
            default: Console.WriteLine("Invalid choice, try again."); return true;
        }
    }

    static void CreateTransaction()
    {
        SaleTransaction st = new SaleTransaction();

        st.InvoiceNo = ReadRequiredText("Enter Invoice No");
        st.CustomerName = ReadOptionalText("Enter Customer Name");
        st.ItemName = ReadOptionalText("Enter Item Name");
        st.Quantity = ReadPositiveInt("Enter Quantity");
        st.PurchaseAmount = ReadPositiveDecimal("Enter Purchase Amount (Total)");
        st.SellingAmount = ReadNonNegativeDecimal("Enter Selling Amount (Total)");

        st.CalculateProfitDetails();
        LastTransaction = st;
        HasLastTransaction = true;

        Console.WriteLine("\nTransaction Saved.");
        PrintProfitDetails(st);
    }

    static void ViewTransaction()
    {
        if (!HasLastTransaction)
        {
            Console.WriteLine("No transaction found. Create one first.");
            return;
        }

        var st = LastTransaction;
        Console.WriteLine("\n------------- Last Transaction -------------");
        Console.WriteLine($"InvoiceNo: {st.InvoiceNo}");
        Console.WriteLine($"Customer: {st.CustomerName}");
        Console.WriteLine($"Item: {st.ItemName}");
        Console.WriteLine($"Quantity: {st.Quantity}");
        Console.WriteLine($"Purchase Amount: {st.PurchaseAmount:F2}");
        Console.WriteLine($"Selling Amount: {st.SellingAmount:F2}");
        Console.WriteLine($"Status: {st.ProfitOrLossStatus}");
        Console.WriteLine($"Profit/Loss Amount: {st.ProfitOrLossAmount:F2}");
        Console.WriteLine($"Profit Margin (%): {st.ProfitMarginPercent:F2}");
        Console.WriteLine("--------------------------------------------");
    }

    static void RecalculateProfit()
    {
        if (!HasLastTransaction)
        {
            Console.WriteLine("No transaction available.");
            return;
        }

        LastTransaction.CalculateProfitDetails();
        Console.WriteLine("\nRecalculated.");
        PrintProfitDetails(LastTransaction);
    }

    static void PrintProfitDetails(SaleTransaction st)
    {
        Console.WriteLine($"Status: {st.ProfitOrLossStatus}");
        Console.WriteLine($"Profit/Loss Amount: {st.ProfitOrLossAmount:F2}");
        Console.WriteLine($"Profit Margin (%): {st.ProfitMarginPercent:F2}");
        Console.WriteLine("------------------------------------------------------");
    }

    // Input Helpers
    static string ReadRequiredText(string message)
    {
        while (true)
        {
            Console.Write(message + ": ");
            string input = Console.ReadLine()?.Trim() ?? "";
            if (!string.IsNullOrEmpty(input)) return input;
            Console.WriteLine("This field cannot be empty.");
        }
    }

    static string ReadOptionalText(string message)
    {
        Console.Write(message + ": ");
        return Console.ReadLine()?.Trim() ?? "";
    }

    static int ReadPositiveInt(string message)
    {
        while (true)
        {
            Console.Write(message + ": ");
            if (int.TryParse(Console.ReadLine(), out int val) && val > 0)
                return val;
            Console.WriteLine("Enter a valid number.");
        }
    }

    static decimal ReadPositiveDecimal(string message)
    {
        while (true)
        {
            Console.Write(message + ": ");
            if (decimal.TryParse(Console.ReadLine(), out decimal val) && val > 0)
                return val;
            Console.WriteLine("Enter a valid amount.");
        }
    }

    static decimal ReadNonNegativeDecimal(string message)
    {
        while (true)
        {
            Console.Write(message + ": ");
            if (decimal.TryParse(Console.ReadLine(), out decimal val) && val >= 0)
                return val;
            Console.WriteLine("Enter a valid amount.");
        }
    }
}
