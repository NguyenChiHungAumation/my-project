using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class TaskDependency
{
    public string TaskName { get; set; }
    public int InDegree { get; set; }
    public List<string> dependencys { get; set; } = new List<string>();
    
    public TaskDependency(string taskName, int indegree)
    {
        TaskName = taskName;
        InDegree = indegree;
    }
}

class Process
{
    private List<TaskDependency> taskDependencies = new List<TaskDependency>();
    public void AddTaskDependency(TaskDependency td)
    {
        taskDependencies.Add(td);

        //Console.WriteLine($"{td.TaskName}:");
        //string result = string.Join(", ", td.dependencys);
        //Console.WriteLine($"{td.TaskName}: {result}");
    }

    private async Task RunTaskAsync(TaskDependency task, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        await Task.Delay(1000, token);
    }
    public async Task ProcessResolverAsync(CancellationToken token)
    {
        List<TaskDependency> available = taskDependencies
            .OrderBy(p => p.TaskName)
            .ToList();

        List<TaskDependency> ready = available
            .Where(p => p.InDegree == 0)
            .ToList();

        int step = 1;
        int processedCount = 0;

            

        while (ready.Count > 0)
        {
            token.ThrowIfCancellationRequested();

            TaskDependency current = ready[0];
            ready.RemoveAt(0);

            await RunTaskAsync(current, token);

            Console.WriteLine($"{step}. {current.TaskName}");
            step++;
            processedCount++;
            
            // Sau khi hoàn thành, giảm InDegrree của những task phụ thuộc current

            foreach (var task in available)
            {
                if (task.InDegree > 0 && task.dependencys.Contains(current.TaskName))
                {
                    task.InDegree--;

                    if (task.InDegree == 0)
                    {
                        ready.Add(task);
                        ready = ready
                            .OrderBy(p => p.TaskName)
                            .ToList();
                    }    
                }    
            }    
        }
        if (processedCount < taskDependencies.Count)
        {
            Console.WriteLine("CIRCULAR DEPENDENCY DETECTED");
        }    
        


    }
}
class Program
{
    static async Task Main()
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
            if (line.Length == 0) return;

            if (line.Length == 1)
            {
                int indegree = line.Length - 1;
                TaskDependency td = new TaskDependency(line[0], indegree);
                

                process.AddTaskDependency(td);
                                                                   
            }

            if (line.Length > 1)
            {
                int indegree = line.Length - 1;
                TaskDependency td = new TaskDependency(line[0], indegree);

                for (int j = 1; j < line.Length; j++)
                {
                    td.dependencys.Add(line[j]);
                }   
                
                process.AddTaskDependency(td);
            }   
            
        }

        CancellationTokenSource cts = new CancellationTokenSource();
        try
        {
            await process.ProcessResolverAsync(cts.Token);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("PROCESS CANCELLED");
        }
    }
}