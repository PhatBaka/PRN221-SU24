internal class Program
{
static async Task Main(string[] args)
    {
        // Start long-running and short tasks
        Task longTask = LongRunningOperation();
        Task shortTask = ShortOperation();

        // Wait for both tasks to complete
        await Task.WhenAll(longTask, shortTask);

        // Start dependent task
        await DependentOperation();

        Console.WriteLine("All tasks completed.");
    }

    static async Task LongRunningOperation()
    {
        Console.WriteLine("Long-running operation started.");
        await Task.Delay(5000); // Simulate a long operation (5 seconds)
        Console.WriteLine("Long-running operation completed.");
    }

    static async Task ShortOperation()
    {
        Console.WriteLine("Short operation started.");
        await Task.Delay(1000); // Simulate a short operation (1 second)
        Console.WriteLine("Short operation completed.");
    }

    static async Task DependentOperation()
    {
        Console.WriteLine("Dependent operation started.");
        await Task.Delay(2000); // Simulate some additional work (2 seconds)
        Console.WriteLine("Dependent operation completed.");
    }
}