using System;

class WorkerThread
{
    public int Worker {  get; private set; }
    public List<string> NumWorker {  get; set; } = new List<string>();
    public List<(string, int)> TaskDuration { get; set; } = new List<(string, int)>();

    public WorkerThread(int worker)
    {
        Worker = worker;
    }
}



class Process
{
    public void AddWorker(WorkerThread workerThread)
    {
        for (int i = 0; i < workerThread.Worker; i++)
        {
            workerThread.NumWorker.Add($"W{i + 1}");
        }
    }

    public void AddTask(WorkerThread workerThread, string taskName, int duration)
    {
        workerThread.TaskDuration.Add((taskName, duration));
    }

    public void ProcessThread(WorkerThread workerThread)
    {        
        Queue<(string Name, int Duration)> taskQueue = new Queue<(string, int)>(workerThread.TaskDuration);

        string[] currentTask = new string[workerThread.Worker];
        int[] completeTick = new int[workerThread.Worker];
        int[] taskDuration = new int[workerThread.Worker];

        int tick = 1;

        while (taskQueue.Count() > 0 || currentTask.Any(p => p != null))
        {
            //Kiểm tra task hoàn thành ở tick hiện tại 
            for (int i = 0;i < workerThread.Worker;i++)
            {
                if (currentTask[i] != null && completeTick[i] == tick)
                {
                    Console.WriteLine($"[Tick {tick}] W{i + 1} completes {currentTask[i]}");

                    currentTask[i] = null;
                    
                }    
             

            // Gán task mới cho worker rảnh
            
            
                if (currentTask[i] == null && taskQueue.Count() > 0)
                {
                    var task = taskQueue.Dequeue();

                    currentTask[i] = task.Name;
                    taskDuration[i] = task.Duration;
                    completeTick[i] = tick + task.Duration;

                    Console.WriteLine($"[Tick {tick}] W{i + 1} starts {currentTask[i]} (duration: {taskDuration[i]})");

                }    
            }

            tick++;

        }

        Console.WriteLine($"All tasks completed at tick {tick - 1}");
    }


}

class Program
{
    static void Main()
    {
        Process process = new Process();
        WorkerThread workerThread = new WorkerThread(0);
        int numWorker;
        int numTasks;

        try
        {
            string[] s = Console.ReadLine().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

            if (!int.TryParse(s[0], out numWorker) || numWorker < 1) return;
            if (!int.TryParse(s[1], out numTasks) || numTasks < 1) return;

            if (numWorker < 1 || numTasks < 1)
                throw new Exception();

            workerThread = new WorkerThread(numWorker);

            process.AddWorker(workerThread);
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

        string[] data = new string[numTasks];

        try
        {
            for (int i = 0; i < numTasks; i++)
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


        for (int i = 0; i < numTasks; i++)
        {
            string[] line = data[i].Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

            string taskName = line[0];
            if (!int.TryParse(line[1], out int duration) || duration < 0) return;

            process.AddTask(workerThread, taskName, duration);
        }

        process.ProcessThread(workerThread);
    }
}