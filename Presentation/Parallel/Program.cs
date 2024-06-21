internal class Program
{
    static void Main(string[] args)
    {
        Parallel.Invoke(
            () => DoWork("Task 1"),
            () => DoWork("Task 2")
        );

        Console.WriteLine("All tasks completed.");
    }

    static void DoWork(string name)
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"{name} - iteration {i}");
            Task.Delay(1000).Wait();
        }
    }
}