using System;
using System.Threading;

class Program
{
    static void Main()
    {
        void PrintNumbers()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Worker: {i}");
            }    
        }

        Thread worker = new Thread(PrintNumbers);
        worker.Start();
        worker.Join();

        
    }
    
    
}