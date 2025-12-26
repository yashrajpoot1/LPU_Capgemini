using System;
namespace CompanyNamespace
{
  public class Manager : Employee
  {
    public int mgrid
    {
      get; set;
    }
    public int incentive
    {
      get; set;
    }
    public override int calculateSalary()
    {
      int mySalary = 0;
      //salary=bs+hra+da+ta-deduction+incentive
      mySalary = BasicSalary + 15000 + 3000 + 1500 - 2500 + incentive;
      return mySalary;
    }
  }
}
