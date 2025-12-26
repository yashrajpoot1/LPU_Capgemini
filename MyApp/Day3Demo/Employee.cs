using System;
namespace Day3demo1
{
  public class Employee : Person
  {
    public string Department;
    public double Salary;

    public Employee()
    {
      Department = string.Empty;
      Salary = 0.0;
      Console.WriteLine("Employee class default constructor called.");
    }
    ~Employee()
    {
      Console.WriteLine("Employee class destructor called.");
    }
    public Employee(string name, int age, string department, double salary) : base(name, age)
    {
      Department = department;
      Salary = salary;
    }
    public void DisplayInfo()
    {
      Console.WriteLine($"Name: {Name}, Age: {Age}, Department: {Department}, Salary: {Salary}");
    }
  }
}