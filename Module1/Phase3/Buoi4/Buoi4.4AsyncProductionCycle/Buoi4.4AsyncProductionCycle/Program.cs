using System;
using System.Collections.Generic;
using System.Linq;

class Task
{
    public string Name { get; set; }
    public double Duration { get; set; }
    
    public Task(string name, double duration)
    {
        Name = name;
        Duration = duration;       
    }
}
class Phase
{
    public string Name { get; set; }
    public string Mode { get; set; }
    public List<Task> Tasks { get; set; } = new List<Task>();
    
    public Phase(string name, string mode)
    {
        Name = name;
        Mode = mode;        
    }

    public void AddTaskToPhase(Task task)
    {
        Tasks.Add(task);
    }
}

class Process
{
    private List<Phase> phases = new List<Phase>();    
    
    public void AddPhase(string name, string mode)
    {
        if (mode == "PARALLEL" || mode == "SEQUENTIAL")
        {
            phases.Add(new Phase(name, mode));

            Console.WriteLine($"=== Phase: {name} ({mode}) ===");
        }
        else
            return;
           
    }

    public void AddTask(string name, double duration, int phaseIndex)
    {
        if (phaseIndex >= 0 && phaseIndex < phases.Count)
        {
            phases[phaseIndex].Tasks.Add(new Task(name, duration));

            Console.WriteLine($"{name}: {duration}ms");
        }
        else
            return;
    }

    public void PhaseTime(string mode, int phaseIndex, out double times)
    {
        if (mode == "PARALLEL")
        {
            double time = phases[phaseIndex].Tasks.Max(p => p.Duration);
            times = time;

            Console.WriteLine($"Phase time: {time}ms");
        }
        else
        {
            double sum = phases[phaseIndex].Tasks.Sum(p => p.Duration);
            times = sum;

            Console.WriteLine($"Phase time: {sum}ms");
        }    


    }


}

class Program
{
    static void Main()
    {
        Process process = new Process();

        // Nhập số Phase
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
        double time = 0;
        double totalTime = 0;
        // Tạo mảng chứa N Phase
        //string[] dataPhase = new string[N];
        for (int i = 0; i < N; i++)
        {
            // Nhập Phase
            string[] line = Console.ReadLine().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            if (line.Length != 2) return;

            string name = line[0];
            string mode = line[1];
            process.AddPhase(name, mode);

            if (!int.TryParse(Console.ReadLine(), out int T)) return;

            for (int j = 0; j < T; j++)
            {
                string[] lineTask = Console.ReadLine().Split(" ", 2, StringSplitOptions.RemoveEmptyEntries);

                if (lineTask.Length != 2) return;
                if (!double.TryParse(lineTask[1], out double duration)) return;

                string nameTask = lineTask[0];
                process.AddTask(nameTask, duration, i);

            }

            process.PhaseTime(mode, i, out time);

            totalTime += time;
        }

        Console.WriteLine($"Total time: {totalTime}ms");
    }
}