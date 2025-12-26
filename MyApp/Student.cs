using System;
namespace Name
{
  public class Student
  {
    #region Properties
    string name = string.Empty;
    int age;
    string address = string.Empty;
    #endregion
    /// <summary>
    /// Default Constructor for students
    /// </summary>
    public Student()
    {
      name = "Unknown";
      age = 0;
      address = "Not Available";
    }
    public Student(string name, int age, string address)
    {
      this.name = name;
      this.age = age;
      this.address = address;
    }
    public void DisplayInfo(Student s1)
    {
      Console.WriteLine($"Name: {s1.name}, Age: {s1.age}, Address: {s1.address}");
    }
  }
}
