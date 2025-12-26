using System;
namespace CompanyNamespace
{
  class clerk : Employee
  {
    public int clrk_id
    {
      get; set;
    }
    public int bonus
    {
      get; set;
    }
    public new int calculateSalary()
    {
      int mySalary = 0;
      //salary=bs+hra+da+ta-deduction+bonus
      mySalary = BasicSalary + 15000 + 3000 + 1500 - 2500 + bonus;
      return mySalary;
    }
  }
}