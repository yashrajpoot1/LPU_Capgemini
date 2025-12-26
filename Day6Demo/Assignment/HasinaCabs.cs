using System;

// Base Cab class with properties
public class Cab
{
    public string BookingID { get; set; }
    public string CabType { get; set; }
    public double Distance { get; set; }
    public int WaitingTime { get; set; }
}

// CabDetails class inheriting from Cab
public class CabDetails : Cab
{
    public bool ValidateBookingID()
    {
        // Check if booking ID length is 6
        if (BookingID.Length != 6)
            return false;

        // Check if it starts with "AC"
        if (!BookingID.StartsWith("AC"))
            return false;

        // Check if third character is '@'
        if (BookingID[2] != '@')
            return false;

        // Check if last 3 characters are digits
        string lastThree = BookingID.Substring(3);
        if (lastThree.Length != 3)
            return false;

        foreach (char c in lastThree)
        {
            if (!char.IsDigit(c))
                return false;
        }

        return true;
    }

    public double CalculateFareAmount()
    {
        double pricePerKm = 0;

        // Set price per km based on cab type
        switch (CabType)
        {
            case "Hatchback":
                pricePerKm = 10;
                break;
            case "Sedan":
                pricePerKm = 20;
                break;
            case "SUV":
                pricePerKm = 30;
                break;
        }

        // Calculate waiting charge (square root of waiting time)
        double waitingCharge = Math.Sqrt(WaitingTime);

        // Calculate fare
        double fare = Distance * pricePerKm + waitingCharge;

        // Return fare with two decimal places using Math.Floor
        return Math.Floor(fare * 100) / 100;
    }
}

public class HasinaCabsProgram
{
    public static void Main()
    {
        CabDetails cab = new CabDetails();

        Console.WriteLine("Enter the booking id");
        cab.BookingID = Console.ReadLine();

        // Validate booking ID
        if (cab.ValidateBookingID())
        {
            Console.WriteLine("Enter the cab type");
            cab.CabType = Console.ReadLine();

            Console.WriteLine("Enter the distance");
            cab.Distance = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter the waiting time");
            cab.WaitingTime = int.Parse(Console.ReadLine());

            // Calculate and display fare
            double fareAmount = cab.CalculateFareAmount();
            Console.WriteLine($"Fare Amount : {fareAmount:F2}");
        }
        else
        {
            Console.WriteLine("Invalid booking id");
        }
    }
}