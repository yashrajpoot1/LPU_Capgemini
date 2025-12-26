using System;
namespace Assignment1Part1Question12
{
  class ATMWithdrawal
  {
    public static void ProcessWithdrawal()
    {
      Console.Write("Is card inserted? (yes/no): ");
      string cardInserted = Console.ReadLine()!.ToLower();
      
      if (cardInserted == "yes")
      {
        Console.Write("Enter PIN: ");
        string pin = Console.ReadLine()!;
        
        if (pin == "1234")
        {
          Console.Write("Enter withdrawal amount: ");
          int amount = Convert.ToInt32(Console.ReadLine());
          int balance = 10000;
          
          if (amount <= balance)
          {
            balance -= amount;
            Console.WriteLine($"Withdrawal successful! Amount: {amount} Rs");
            Console.WriteLine($"Remaining balance: {balance} Rs");
          }
          else
          {
            Console.WriteLine("Insufficient balance");
          }
        }
        else
        {
          Console.WriteLine("Invalid PIN");
        }
      }
      else
      {
        Console.WriteLine("Please insert card");
      }
    }
  }
}
