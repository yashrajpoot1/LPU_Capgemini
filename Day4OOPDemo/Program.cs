using System;
namespace CompanyNamespace
{
  class Program
  {
    static void Main(string[] args)
    {
      Employee emp = new Employee();
      emp.Id = 1;
      emp.Name = "John Doe";
      emp.BasicSalary = 50000;
      Console.WriteLine($"Employee Salary: {emp.calculateSalary()}");

      Manager mgr = new Manager();
      mgr.Id = 2;
      mgr.Name = "Jane Smith";
      mgr.BasicSalary = 70000;
      mgr.incentive = 10000;
      Console.WriteLine($"Manager Salary: {mgr.calculateSalary()}");

      clerk clk = new clerk();
      clk.Id = 3;
      clk.Name = "Jim Brown";
      clk.BasicSalary = 40000;
      clk.bonus = 5000;
      Console.WriteLine($"Clerk Salary: {clk.calculateSalary()}");
    }
  }
}