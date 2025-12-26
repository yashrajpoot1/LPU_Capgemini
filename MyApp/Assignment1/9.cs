using System;
namespace Assignment1Part1Question9
{
  class QuadrantFinder
  {
    public static void FindQuadrant()
    {
      Console.Write("Enter x coordinate: ");
      int x = Convert.ToInt32(Console.ReadLine());
      Console.Write("Enter y coordinate: ");
      int y = Convert.ToInt32(Console.ReadLine());
      
      if (x == 0 && y == 0)
      {
        Console.WriteLine("Point is at Origin");
      }
      else if (x == 0)
      {
        Console.WriteLine("Point lies on Y-axis");
      }
      else if (y == 0)
      {
        Console.WriteLine("Point lies on X-axis");
      }
      else
      {
        switch ((x > 0, y > 0))
        {
          case (true, true):
            Console.WriteLine("Point lies in Quadrant I");
            break;
          case (false, true):
            Console.WriteLine("Point lies in Quadrant II");
            break;
          case (false, false):
            Console.WriteLine("Point lies in Quadrant III");
            break;
          case (true, false):
            Console.WriteLine("Point lies in Quadrant IV");
            break;
        }
      }
    }
  }
}
