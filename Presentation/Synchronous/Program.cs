using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Starting synchronous operations...");

        // Perform a long operation synchronously
        PerformLongOperation();

        // Perform a short operation synchronously
        PerformShortOperation();

        Console.WriteLine("All synchronous operations completed.");
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    static void PerformLongOperation()
    {
        Console.WriteLine("Long operation started.");
        // Simulate a long operation (5 seconds)
        System.Threading.Thread.Sleep(5000);
        Console.WriteLine("Long operation completed.");
    }

    static void PerformShortOperation()
    {
        Console.WriteLine("Short operation started.");
        // Simulate a short operation (2 seconds)
        System.Threading.Thread.Sleep(2000);
        Console.WriteLine("Short operation completed.");
    }
}
