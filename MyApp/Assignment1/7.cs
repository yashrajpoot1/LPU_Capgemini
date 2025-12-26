using System;
namespace Assignment1Part1Question7
{
  class VowelConsonantChecker
  {
    public static void CheckVowelConsonant()
    {
      Console.Write("Enter a character: ");
      char ch = Convert.ToChar(Console.ReadLine()!);
      
      switch (char.ToLower(ch))
      {
        case 'a':
        case 'e':
        case 'i':
        case 'o':
        case 'u':
          Console.WriteLine($"{ch} is a Vowel");
          break;
        default:
          if (char.IsLetter(ch))
            Console.WriteLine($"{ch} is a Consonant");
          else
            Console.WriteLine($"{ch} is not a letter");
          break;
      }
    }
  }
}
