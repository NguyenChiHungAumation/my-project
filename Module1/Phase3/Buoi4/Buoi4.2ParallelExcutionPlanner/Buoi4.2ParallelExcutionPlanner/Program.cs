using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class Task
{
    public string TaskName { get; set; }
    public double Duration { get; set; }
    public double Start {  get; set; }
    public double End { get; set; }
    public int InDegree { get; set; }
    
    public List<string> Dependencys { get; set; } = new List<string>();

    public Task(string taskName, double duration, int inDegree)
    {
        TaskName = taskName;
        Duration = duration;
        InDegree = inDegree;
    }
}

class Process
{
    private List<Task> tasks = new List<Task>();
    public void AddTask(Task task)
    {
        tasks.Add(task);
    }

    public void ProcessTask()
    {
        
        List<Task> available = tasks
            .OrderBy(p => p.TaskName)
            .ToList();

        List<Task> ready = tasks
            .Where(p => p.InDegree == 0)
            .ToList();

        double sequential = available.Sum(p => p.Duration);
        double parallel = 0;
        double speedup = 0;
        int processedCount = 0;

        foreach (var item in ready)
        {
            item.Start = 0;
            item.End = item.Duration;
        }    

        while (ready.Count > 0)
        {
            Task current = ready[0];
            ready.RemoveAt(0);
            processedCount++;

            parallel = Math.Max(parallel, current.End);

            Console.WriteLine($"{current.TaskName}: start={current.Start}ms, end={current.End}ms");

            foreach (var task in available)
            {
                if (task.InDegree > 0 && task.Dependencys.Contains(current.TaskName))
                {
                    task.InDegree--;
                    task.Start = Math.Max(task.Start, current.End);
                    task.End = task.Start;

                    if (task.InDegree == 0)
                    {
                        task.End += task.Duration;
                        ready.Add(task);
                        /*ready = ready
                            .OrderBy(p => p.TaskName)
                            .ToList();)*/
                    }    
                }    
            }    
        }

        if (processedCount < available.Count)
        {
            Console.WriteLine("CIRCULAR DEPENDENCY DETECTED");
            return;
        }

        speedup = sequential / parallel;
        Console.WriteLine($"Sequential: {sequential}ms");
        Console.WriteLine($"Parallel: {parallel}ms");
        Console.WriteLine($"Speedup: {speedup:F1}x");
                
    }
}

class Program
{
    static void Main()
    {
        Process process = new Process();

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
            string[] line = data[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (line.Length == 0 || line.Length == 1) return;

            if (line.Length == 2)
            {
                int inDegree = 0;
                string taskName = line[0];
                double duration = double.Parse(line[1]);

                Task task = new Task(taskName, duration,inDegree);

                process.AddTask(task);
            }

            if (line.Length > 2)
            {
                int inDegree = line.Length - 2;
                string taskName = line[0];
                double duration = double.Parse(line[1]);

                Task task = new Task(taskName, duration, inDegree);

                for (int j = 2; j < line.Length; j++)
                {
                    task.Dependencys.Add(line[j]);
                }

                process.AddTask(task);
            }    
        }

        process.ProcessTask();
    }
}