using System;

// Event delegate to notify when a bill is processed
public delegate void BillEventHandler(string message);

class ClinicBillProcessor
{
    // Represents a single patient's bill
    public class BillRecord
    {
        public string BillId;
        public string PatientName;
        public bool HasInsurance;
        public decimal ConsultationFee;
        public decimal LabCharges;
        public decimal MedicineCharges;
        public decimal GrossAmount;
        public decimal DiscountAmount;
        public decimal FinalPayable;
    }

    // Event trigger for bill processing
    public static event BillEventHandler OnBillProcessed;

    // Stores the last created bill only
    private static BillRecord? lastBill = null;

    // Menu options shown to the user
    private static readonly string[] MenuOptions = {
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

    // Runs the menu-driven application
    static void StartApplication()
    {
        while (true)
        {
            int choice = ShowMenu();
            if (!HandleOption(choice)) 
                break;
        }
    }

    // Displays menu options and reads user choice
    static int ShowMenu()
    {
        Console.WriteLine("\n================== MediSure Clinic Billing ==================");
        for (int i = 0; i < MenuOptions.Length; i++)
            Console.WriteLine($"{i + 1}. {MenuOptions[i]}");

        return ReadInteger("Enter your option", 1, 4);
    }

    // Performs selected menu action
    static bool HandleOption(int choice)
    {
        switch (choice)
        {
            case 1: CreateNewBill(); break;
            case 2: ShowLastBill(); break;
            case 3: ClearBill(); break;
            case 4:
                Console.WriteLine("Thank you. Application closed normally.");
                return false;
        }

        Console.WriteLine("------------------------------------------------------------");
        return true;
    }

    // Creates a new bill with user input
    static void CreateNewBill()
    {
        BillRecord bill = new BillRecord();
        
        bill.BillId = ReadText("Enter Bill Id", true);
        bill.PatientName = ReadText("Enter Patient Name", true);
        bill.HasInsurance = ReadYesNo("Is the patient insured? (Y/N)");
        bill.ConsultationFee = ReadDecimal("Enter Consultation Fee", 0.01m, decimal.MaxValue);
        bill.LabCharges = ReadDecimal("Enter Lab Charges", 0m, decimal.MaxValue);
        bill.MedicineCharges = ReadDecimal("Enter Medicine Charges", 0m, decimal.MaxValue);

        // Calculates bill amount based on given input
        bill = CalculateBill(bill);
        lastBill = bill;

        OnBillProcessed?.Invoke("Bill processed successfully"); // Notification event
        
        PrintBillSummary(bill);
    }

    // Calculates gross amount, discount, and final payable
    static BillRecord CalculateBill(BillRecord bill)
    {
        bill.GrossAmount = bill.ConsultationFee + bill.LabCharges + bill.MedicineCharges;
        bill.DiscountAmount = bill.HasInsurance ? bill.GrossAmount * 0.10m : 0m;
        bill.FinalPayable = bill.GrossAmount - bill.DiscountAmount;
        return bill;
    }

    // Shows the most recent bill
    static void ShowLastBill()
    {
        if (lastBill == null)
        {
            Console.WriteLine("No bill available. Please create a new bill first.");
            return;
        }

        BillRecord bill = lastBill;
        Console.WriteLine("----------- Last Bill -----------");
        Console.WriteLine($"Bill Id: {bill.BillId}");
        Console.WriteLine($"Patient Name: {bill.PatientName}");
        Console.WriteLine($"Insured: {(bill.HasInsurance ? "Yes" : "No")}");
        Console.WriteLine($"Consultation Fee: {bill.ConsultationFee:F2}");
        Console.WriteLine($"Lab Charges: {bill.LabCharges:F2}");
        Console.WriteLine($"Medicine Charges: {bill.MedicineCharges:F2}");
        Console.WriteLine($"Gross Amount: {bill.GrossAmount:F2}");
        Console.WriteLine($"Discount Amount: {bill.DiscountAmount:F2}");
        Console.WriteLine($"Final Payable: {bill.FinalPayable:F2}");
        Console.WriteLine("--------------------------------");
    }

    // Deletes the stored bill data
    static void ClearBill()
    {
        lastBill = null;
        Console.WriteLine("Last bill cleared.");
    }

    // Shows summary after bill creation
    static void PrintBillSummary(BillRecord bill)
    {
        Console.WriteLine("Bill created successfully.");
        Console.WriteLine($"Gross Amount: {bill.GrossAmount:F2}");
        Console.WriteLine($"Discount Amount: {bill.DiscountAmount:F2}");
        Console.WriteLine($"Final Payable: {bill.FinalPayable:F2}");
    }

    // Shows event messages
    static void DisplayNotification(string message)
    {
        Console.WriteLine("[Notification]: " + message);
    }

    // ---------------- INPUT HELPERS ----------------

    static string ReadText(string prompt, bool required)
    {
        while (true)
        {
            Console.Write(prompt + ": ");
            string input = Console.ReadLine()?.Trim() ?? "";
            if (!required || !string.IsNullOrEmpty(input)) return input;
            Console.WriteLine("This field is required!");
        }
    }

    static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt + ": ");
            string input = Console.ReadLine()?.Trim().ToUpper() ?? "";
            if (input == "Y") return true;
            if (input == "N") return false;
            Console.WriteLine("Please enter Y or N only.");
        }
    }

    static decimal ReadDecimal(string prompt, decimal min, decimal max)
    {
        while (true)
        {
            Console.Write(prompt + ": ");
            if (decimal.TryParse(Console.ReadLine(), out decimal value) && value >= min && value <= max)
                return value;

            Console.WriteLine($"Enter a valid amount between {min} and {max}.");
        }
    }

    static int ReadInteger(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt + ": ");
            if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                return value;

            Console.WriteLine($"Enter a number between {min} and {max}.");
        }
    }
}
