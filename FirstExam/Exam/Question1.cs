using System;

public delegate void BillEventHandler(string message);

class ClinicBillProcessor
{
    // Using structs instead of classes for data storage
    public struct BillRecord
    {
        public string Id;
        public string Name;
        public bool Insurance;
        public decimal Consultation;
        public decimal Lab;
        public decimal Medicine;
        public decimal Total;
        public decimal Discount;
        public decimal Payment;
    }

    // Event for notifications
    public static event BillEventHandler OnBillProcessed;

    private static BillRecord? currentBill = null;
    private static readonly string[] MenuItems = {
        "Create New Bill (Enter Patient Details)",
        "View Last Bill", 
        "Clear Last Bill",
        "Exit"
    };

    static void Main()
    {
        OnBillProcessed += DisplayNotification;
        StartApplication();
    }

    static void StartApplication()
    {
        while (true)
        {
            int choice = PresentMenu();
            if (!HandleUserChoice(choice)) break;
        }
    }

    static int PresentMenu()
    {
        Console.WriteLine("\n================== MediSure Clinic Billing ==================");
        
        for (int i = 0; i < MenuItems.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {MenuItems[i]}");
        }
        
        return ReadIntegerInput("Enter your option", 1, 4);
    }

    static bool HandleUserChoice(int choice)
    {
        switch (choice)
        {
            case 1: ProcessNewBill(); break;
            case 2: ShowBillDetails(); break;
            case 3: RemoveBill(); break;
            case 4: 
                Console.WriteLine("Thank you. Application closed normally.");
                return false;
        }
        Console.WriteLine("------------------------------------------------------------");
        return true;
    }

    static void ProcessNewBill()
    {
        BillRecord bill = new BillRecord();
        
        // Collect bill information
        bill.Id = ReadTextInput("Enter Bill Id", true);
        bill.Name = ReadTextInput("Enter Patient Name", false);
        bill.Insurance = ReadBooleanInput("Is the patient insured? (Y/N)");
        bill.Consultation = ReadDecimalInput("Enter Consultation Fee", 0.01m, decimal.MaxValue);
        bill.Lab = ReadDecimalInput("Enter Lab Charges", 0m, decimal.MaxValue);
        bill.Medicine = ReadDecimalInput("Enter Medicine Charges", 0m, decimal.MaxValue);
        
        // Process calculations
        bill = CalculateBillTotals(bill);
        currentBill = bill;
        
        // Trigger event
        OnBillProcessed?.Invoke("Bill processing completed successfully");
        
        // Display summary
        PrintBillSummary(bill);
    }

    static BillRecord CalculateBillTotals(BillRecord bill)
    {
        bill.Total = bill.Consultation + bill.Lab + bill.Medicine;
        bill.Discount = bill.Insurance ? bill.Total * 0.10m : 0m;
        bill.Payment = bill.Total - bill.Discount;
        return bill;
    }

    static void ShowBillDetails()
    {
        if (!currentBill.HasValue)
        {
            Console.WriteLine("No bill available. Please create a new bill first.");
            return;
        }

        BillRecord bill = currentBill.Value;
        Console.WriteLine("----------- Last Bill -----------");
        Console.WriteLine($"BillId: {bill.Id}");
        Console.WriteLine($"Patient: {bill.Name}");
        Console.WriteLine($"Insured: {(bill.Insurance ? "Yes" : "No")}");
        Console.WriteLine($"Consultation Fee: {bill.Consultation:F2}");
        Console.WriteLine($"Lab Charges: {bill.Lab:F2}");
        Console.WriteLine($"Medicine Charges: {bill.Medicine:F2}");
        Console.WriteLine($"Gross Amount: {bill.Total:F2}");
        Console.WriteLine($"Discount Amount: {bill.Discount:F2}");
        Console.WriteLine($"Final Payable: {bill.Payment:F2}");
        Console.WriteLine("--------------------------------");
    }

    static void RemoveBill()
    {
        currentBill = null;
        Console.WriteLine("Last bill cleared.");
    }

    static void PrintBillSummary(BillRecord bill)
    {
        Console.WriteLine("Bill created successfully.");
        Console.WriteLine($"Gross Amount: {bill.Total:F2}");
        Console.WriteLine($"Discount Amount: {bill.Discount:F2}");
        Console.WriteLine($"Final Payable: {bill.Payment:F2}");
    }

    static void DisplayNotification(string message)
    {
        // Event handler for bill processing notifications
    }

    // Utility methods for input handling
    static string ReadTextInput(string prompt, bool required)
    {
        while (true)
        {
            Console.WriteLine(prompt + ":");
            string input = Console.ReadLine()?.Trim() ?? "";
            if (!required || !string.IsNullOrEmpty(input))
                return input;
            Console.WriteLine("This field is required!");
        }
    }

    static bool ReadBooleanInput(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt + ":");
            string input = Console.ReadLine()?.Trim().ToUpper() ?? "";
            if (input == "Y") return true;
            if (input == "N") return false;
            Console.WriteLine("Please enter Y or N only.");
        }
    }

    static decimal ReadDecimalInput(string prompt, decimal min, decimal max)
    {
        while (true)
        {
            Console.WriteLine(prompt + ":");
            if (decimal.TryParse(Console.ReadLine(), out decimal value))
            {
                if (value >= min && value <= max)
                    return value;
            }
            Console.WriteLine($"Please enter a valid amount between {min} and {max}.");
        }
    }

    static int ReadIntegerInput(string prompt, int min, int max)
    {
        while (true)
        {
            Console.WriteLine(prompt + ":");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                if (value >= min && value <= max)
                    return value;
            }
            Console.WriteLine($"Please enter a number between {min} and {max}.");
        }
    }
}