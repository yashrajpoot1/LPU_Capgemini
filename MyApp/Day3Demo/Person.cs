using System;
namespace Day3demo1
{
  public class Person
  {
    public string Name;
    public int Age;
    public Person()
    {
      Name = string.Empty;
      Age = 0;
      Console.WriteLine("Person class default constructor called.");
    }
    ~Person()
    {
      Console.WriteLine("Person class destructor called.");
    }
    public Person(string name, int age)
    {
      Name = name;
      Age = age;
    }
  }
}