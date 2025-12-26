using System;

// Candy class with properties and methods
public class Candy
{
    public string Flavour { get; set; }
    public int Quantity { get; set; }
    public int PricePerPiece { get; set; }
    public double TotalPrice { get; set; }
    public double Discount { get; set; }

    public bool ValidateCandyFlavour()
    {
        if (Flavour == "Strawberry" || Flavour == "Lemon" || Flavour == "Mint")
        {
            return true;
        }
        return false;
    }
}

public class CandyCrazeProgram
{
    public static Candy CalculateDiscountedPrice(Candy candy)
    {
        // Calculate total price
        candy.TotalPrice = candy.Quantity * candy.PricePerPiece;

        // Set discount percentage based on flavour
        double discountPercentage = 0;
        switch (candy.Flavour)
        {
            case "Strawberry":
                discountPercentage = 15;
                break;
            case "Lemon":
                discountPercentage = 10;
                break;
            case "Mint":
                discountPercentage = 5;
                break;
        }

        // Calculate discounted price
        candy.Discount = candy.TotalPrice - (candy.TotalPrice * discountPercentage / 100);

        return candy;
    }

    public static void Main()
    {
        Candy candy = new Candy();

        Console.WriteLine("Enter the flavour");
        candy.Flavour = Console.ReadLine();

        Console.WriteLine("Enter the quantity");
        candy.Quantity = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the price per piece");
        candy.PricePerPiece = int.Parse(Console.ReadLine());

        // Validate candy flavour
        if (candy.ValidateCandyFlavour())
        {
            // Calculate discounted price
            candy = CalculateDiscountedPrice(candy);

            // Display results
            Console.WriteLine($"Flavour : {candy.Flavour}");
            Console.WriteLine($"Quantity : {candy.Quantity}");
            Console.WriteLine($"Price Per Piece : {candy.PricePerPiece}");
            Console.WriteLine($"Total Price : {candy.TotalPrice}");
            Console.WriteLine($"Discount Price : {candy.Discount}");
        }
        else
        {
            Console.WriteLine("Invalid flavour");
        }
    }
}