using System;

// Book class with fields, constructors and methods
public class Book
{
    public string title;
    public string author;
    public int numPages;
    public DateTime dueDate;
    public DateTime returnedDate;

    // Default constructor
    public Book()
    {
    }

    // Parameterized constructor
    public Book(string title, string author, int numPages, DateTime dueDate, DateTime returnedDate)
    {
        this.title = title;
        this.author = author;
        this.numPages = numPages;
        this.dueDate = dueDate;
        this.returnedDate = returnedDate;
    }

    public double AveragePagesReadPerDay(int daysToRead)
    {
        return (double)numPages / daysToRead;
    }

    public double CalculateLateFee(double dailyLateFeeRate)
    {
        // Calculate number of days late
        TimeSpan lateDays = returnedDate - dueDate;
        int numberOfDaysLate = lateDays.Days;

        // Calculate late fee
        double lateFee = numberOfDaysLate * dailyLateFeeRate;
        return lateFee;
    }
}

public class LibraryManagementProgram
{
    public static void Main()
    {
        Console.WriteLine("Enter the title");
        string title = Console.ReadLine();

        Console.WriteLine("Enter the author");
        string author = Console.ReadLine();

        Console.WriteLine("Enter the number of pages");
        int numPages = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the due date");
        DateTime dueDate = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Enter the return date");
        DateTime returnedDate = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Enter the days to read");
        int daysToRead = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the daily late feeRate");
        double dailyLateFeeRate = double.Parse(Console.ReadLine());

        // Create book object using parameterized constructor
        Book book = new Book(title, author, numPages, dueDate, returnedDate);

        // Calculate and display results
        double averagePages = book.AveragePagesReadPerDay(daysToRead);
        double lateFee = book.CalculateLateFee(dailyLateFeeRate);

        Console.WriteLine($"Average Pages Read Per Day : {(int)averagePages}");
        Console.WriteLine($"Late Fee : {(int)lateFee}");
    }
}