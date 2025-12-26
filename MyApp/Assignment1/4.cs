using System;
namespace Assignment1Part1Question4
{
  class QuadraticRootsFinder
  {
    public static void FindRoots()
    {
      int a=Convert.ToInt32(Console.ReadLine());
      int b = Convert.ToInt32(Console.ReadLine());
      int c = Convert.ToInt32(Console.ReadLine());
      int discriminant = (b * b) - (4 * a * c);
      if (discriminant > 0)
      {
        Console.WriteLine("Roots are real and different.");
      }else if (discriminant == 0)
      {
        Console.WriteLine("Roots are real and same.");
      }
      else
      {
        Console.WriteLine("Roots are complex and different.");
      }
    }
  }
}