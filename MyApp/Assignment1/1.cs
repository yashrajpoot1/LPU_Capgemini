using System;

namespace Assignment1Part1Question1
{
  class HeightCategorizer
  {
    public static void CategorizeHeight()
    {
      int height = Convert.ToInt32(Console.ReadLine());

      switch (height)
      {
        case int _ when height < 150:
          Console.WriteLine("Dwarf");
          break;

        case int _ when height >= 150 && height <= 165:
          Console.WriteLine("Average");
          break;

        case int _ when height > 165 && height <= 195:
          Console.WriteLine("Tall");
          break;

        default:
          Console.WriteLine("Abnormal");
          break;
      }
    }
  }
}
