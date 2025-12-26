using System;
namespace Assignment1Part1Question13
{
  class ProfitLossCalculator
  {
    public static void CalculateProfitLoss()
    {
      Console.Write("Enter Cost Price: ");
      double costPrice = Convert.ToDouble(Console.ReadLine());
      Console.Write("Enter Selling Price: ");
      double sellingPrice = Convert.ToDouble(Console.ReadLine());
      
      if (sellingPrice > costPrice)
      {
        double profit = sellingPrice - costPrice;
        double profitPercentage = (profit / costPrice) * 100;
        Console.WriteLine($"Profit: {profit} Rs");
        Console.WriteLine($"Profit Percentage: {profitPercentage:F2}%");
      }
      else if (costPrice > sellingPrice)
      {
        double loss = costPrice - sellingPrice;
        double lossPercentage = (loss / costPrice) * 100;
        Console.WriteLine($"Loss: {loss} Rs");
        Console.WriteLine($"Loss Percentage: {lossPercentage:F2}%");
      }
      else
      {
        Console.WriteLine("No Profit No Loss");
      }
    }
  }
}
