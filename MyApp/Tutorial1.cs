using System;
namespace MyTutorial1
{
  class ArrayProcessor
  {
    public static void ProcessArray()
    {
      Console.WriteLine("Size of Array: ");
      int size = Convert.ToInt32(Console.ReadLine());
      int[] arr = new int[size];
      Console.WriteLine("Enter Array Elements: ");
      for (int i = 0; i < size; i++)
      {
        arr[i] = Convert.ToInt32(Console.ReadLine());
      }
      Console.WriteLine("Array Elements are: ");
      for (int i = 0; i < size; i++)
      {
        Console.Write(arr[i]);
        Console.Write(" ");
      }
    }
  }
}