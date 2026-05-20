using System;
using System.Threading;
using System.Collections.Generic;

class Robin
{
    public string ThreadName { get; set; }
    public int Units { get; set; }
}
class RoundRobinThread
{
    

    public Thread[] thread;
    private List<Robin> robins = new List<Robin>();
    public int tick = 0;
    public object lockObj = new object();
    public void AddRobin(string threadName, int units)
    {
        robins.Add(new Robin
        {
            ThreadName = threadName,
            Units = units
        });
    }
    public void TinhThread()
    {
        thread = new Thread[robins.Count];

        for (int i = 0; i < robins.Count; i++)
        {
            int index = i;
            thread[i] = new Thread(() => RoundRobin(robins[index].ThreadName, robins[index].Units));
        }    

        for (int i = 0;i < robins.Count; i++)
        {
            thread[i].Start();
        }   
        
        for(int i = 0; i < robins.Count; i++)
        {
            thread[i].Join();
        }    
        
    }


    public void RoundRobin(string threadName, int units)
    {
        
        for (int i = 1; i <= units; i++)
        {
            lock (lockObj)
            {
                tick++;
                Console.WriteLine($"[Tick {tick}] {threadName} runs (remaining: {units - i})");
            }    
            
        }

        Console.WriteLine($"{threadName} completed");
    }
}

class Program
{
    
    static void Main()
    {
        RoundRobinThread roundRobin = new RoundRobinThread();

        int N;
        try
        {
            N = int.Parse(Console.ReadLine());
            if (N < 0)
                throw new Exception();
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Lỗi: không được để trống");
            return;
        }
        catch (FormatException)
        {
            Console.WriteLine("Lỗi: nhập vào phải là số nguyên");
            return;
        }
        catch (OverflowException)
        {
            Console.WriteLine("Lỗi: số quá lớn");
            return;
        }
        catch (Exception)
        {
            Console.WriteLine("Lỗi: số nhập vào phải >= 0");
            return;
        }

        string[] data = new string[N];

        try
        {
            for (int i = 0; i < N; i++)
            {

                data[i] = Console.ReadLine();
                if (data[i].Length == 0)
                    throw new Exception();
                               
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Lỗi :không được để rỗng");
        }

        for (int i = 0; i < N; i++)
        {
            string[] line = data[i].Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

            string threadName = line[0];
            int units = int.Parse(line[1]);

            roundRobin.AddRobin(threadName, units);
        }    

        roundRobin.TinhThread();
        Console.WriteLine($"All threads completed at tick {roundRobin.tick}");
    }
}