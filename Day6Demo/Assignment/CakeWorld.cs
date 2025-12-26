using System;

// Custom exception class for invalid flavour
public class InvalidFlavourException : Exception
{
    public InvalidFlavourException(string message) : base(message)
    {
    }
}

// Cake class with properties and methods
public class Cake
{
    public string Flavour { get; set; }
    public int QuantityInKg { get; set; }
    public double PricePerKg { get; set; }

    public bool CakeOrder()
    {
        // Validate flavour
        if (Flavour != "Chocolate" && Flavour != "Red Velvet" && Flavour != "Vanilla")
        {
            throw new InvalidFlavourException("Flavour not available. Please select the available flavour");
        }

        // Validate quantity
        if (QuantityInKg <= 0)
        {
            throw new Exception("Quantity must be greater than zero");
        }

        return true;
    }

    public double CalculatePrice()
    {
        double totalPrice = QuantityInKg * PricePerKg;
        double discount = 0;

        // Set discount based on flavour
        switch (Flavour)
        {
            case "Vanilla":
                discount = 3;
                break;
            case "Chocolate":
                discount = 5;
                break;
            case "Red Velvet":
                discount = 10;
                break;
        }

        double discountedPrice = totalPrice - (totalPrice * discount / 100);
        return discountedPrice;
    }
}

public class CakeWorldProgram
{
    public static void Main()
    {
        try
        {
            Cake cake = new Cake();

            Console.WriteLine("Enter the flavour");
            cake.Flavour = Console.ReadLine();

            Console.WriteLine("Enter the quantity in kg");
            cake.QuantityInKg = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the price per kg");
            cake.PricePerKg = double.Parse(Console.ReadLine());

            // Validate the cake order
            if (cake.CakeOrder())
            {
                Console.WriteLine("Cake order was successful");
                double finalPrice = cake.CalculatePrice();
                Console.WriteLine($"Price after discount is {finalPrice}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}