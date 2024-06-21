internal class Program
{
    static void PrintNumbers()
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"Number: {i}");
            Thread.Sleep(2000); // Tạm dừng 2 giây
        }
    }

    static void PrintLetters()
    {
        for (char letter = 'A'; letter <= 'E'; letter++)
        {
            Console.WriteLine($"Letter: {letter}");
            Thread.Sleep(1000); // Tạm dừng 1 giây
        }
    }

    static void Main(string[] args)
    {
        Thread thread1 = new Thread(PrintNumbers);
        Thread thread2 = new Thread(PrintLetters);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine("Done");
    }
}