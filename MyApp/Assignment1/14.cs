using System;
namespace Assignment1Part1Question14
{
  class RockPaperScissors
  {
    public static void PlayGame()
    {
      Console.Write("Player 1 - Enter choice (Rock/Paper/Scissors): ");
      string player1 = Console.ReadLine()!.ToLower();
      Console.Write("Player 2 - Enter choice (Rock/Paper/Scissors): ");
      string player2 = Console.ReadLine()!.ToLower();
      
      if (player1 == player2)
      {
        Console.WriteLine("It's a Draw!");
      }
      else if (player1 == "rock")
      {
        if (player2 == "scissors")
          Console.WriteLine("Player 1 Wins! Rock crushes Scissors");
        else if (player2 == "paper")
          Console.WriteLine("Player 2 Wins! Paper covers Rock");
        else
          Console.WriteLine("Invalid input from Player 2");
      }
      else if (player1 == "paper")
      {
        if (player2 == "rock")
          Console.WriteLine("Player 1 Wins! Paper covers Rock");
        else if (player2 == "scissors")
          Console.WriteLine("Player 2 Wins! Scissors cuts Paper");
        else
          Console.WriteLine("Invalid input from Player 2");
      }
      else if (player1 == "scissors")
      {
        if (player2 == "paper")
          Console.WriteLine("Player 1 Wins! Scissors cuts Paper");
        else if (player2 == "rock")
          Console.WriteLine("Player 2 Wins! Rock crushes Scissors");
        else
          Console.WriteLine("Invalid input from Player 2");
      }
      else
      {
        Console.WriteLine("Invalid input from Player 1");
      }
    }
  }
}
