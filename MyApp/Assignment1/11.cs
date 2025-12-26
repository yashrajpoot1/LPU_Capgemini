using System;
namespace Assignment1Part1Question11
{
  class DateValidator
  {
    public static void ValidateDate()
    {
      Console.Write("Enter day: ");
      int day = Convert.ToInt32(Console.ReadLine());
      Console.Write("Enter month: ");
      int month = Convert.ToInt32(Console.ReadLine());
      Console.Write("Enter year: ");
      int year = Convert.ToInt32(Console.ReadLine());
      
      bool isValid = false;
      
      if (month >= 1 && month <= 12)
      {
        int maxDays = 31;
        
        switch (month)
        {
          case 4:
          case 6:
          case 9:
          case 11:
            maxDays = 30;
            break;
          case 2:
            bool isLeapYear = (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
            maxDays = isLeapYear ? 29 : 28;
            break;
        }
        
        if (day >= 1 && day <= maxDays)
        {
          isValid = true;
        }
      }
      
      if (isValid)
      {
        Console.WriteLine($"{day}/{month}/{year} is a valid date");
      }
      else
      {
        Console.WriteLine($"{day}/{month}/{year} is an invalid date");
      }
    }
  }
}
