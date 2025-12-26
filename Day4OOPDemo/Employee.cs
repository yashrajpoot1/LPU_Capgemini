using System;
namespace CompanyNamespace
{
  public class Employee
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int BasicSalary { get; set; }

    public virtual int calculateSalary()
    {
      int mySalary = 0;
      //salary=bs+hra+da+ta-deduction
      mySalary = BasicSalary + 15000 + 3000 + 1500 - 2500;
      return mySalary;
    }
  }
}