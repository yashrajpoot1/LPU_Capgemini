using System;
namespace Assignment1Part1Question5
{
  class AdmissionEligibilityChecker
  {
    public static void CheckEligibility()
    {
      int maths=Convert.ToInt32(Console.ReadLine());
      int physics = Convert.ToInt32(Console.ReadLine());
      int chemistry = Convert.ToInt32(Console.ReadLine());
      int total = maths + physics + chemistry;
      if(maths>=65 && physics>=55 && chemistry>=50 && (total>=180 || (maths+physics)>=140))
      {
        Console.WriteLine("The candidate is eligible for admission.");
      }
      else
      {
        Console.WriteLine("The candidate is not eligible for admission.");
      }
    }
  }
}