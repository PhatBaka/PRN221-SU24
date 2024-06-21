internal class Program
{
    private static void Main(string[] args)
    {
        Thread[] threads = new Thread[5];

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(new ParameterizedThreadStart(Worker));
            threads[i].Start(i + 1);
        }

        // Đợi tất cả các luồng hoàn thành
        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine("All workers completed.");
    }

    static void Worker(object number)
    {
        Console.WriteLine($"Worker {number} started");
        Thread.Sleep(2000);  // Giả lập công việc tốn thời gian
        Console.WriteLine($"Worker {number} finished");
    }

}