using System;
namespace Assignment1Part1Question6
{
  class ElectricityBillCalculator
  {
    public static void CalculateBill()
    {
      int unitsConsumed = Convert.ToInt32(Console.ReadLine());
      if(unitsConsumed<=199)
      {
        Console.WriteLine("Your electricity bill is: " + (unitsConsumed * 1.20) + " Rs");
      }
      else if(unitsConsumed>=200 && unitsConsumed<400)
      {
        Console.WriteLine("Your electricity bill is: " + (unitsConsumed * 1.50) + " Rs");
      }
      else if(unitsConsumed>=400 && unitsConsumed<600)
      {
        Console.WriteLine("Your electricity bill is: " + (unitsConsumed * 1.80) + " Rs");
      }
      else
      {
        Console.WriteLine("Your electricity bill is: " + (unitsConsumed * 2.00) + " Rs");
      }
    }
  }
}