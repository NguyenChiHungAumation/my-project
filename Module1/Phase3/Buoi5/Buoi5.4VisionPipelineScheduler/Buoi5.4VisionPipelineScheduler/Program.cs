using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

class TimeFrame
{
    public string Id { get; set; }
    public string Result { get; set; }
    public double GrapTime_Start{ get; set; }
    public double GrapTime_End { get; set; }
    public double ProcessTime_Start { get; set; }
    public double ProcessTime_End { get; set; }
    public double ReportTime_Start { get; set; }
    public double ReportTime_End { get; set; }

    
}

class Process
{
    public double GrapTime { get; private set; }
    public double ProcessTime { get; private set; }
    public double ReportTime { get; private set; }

    public Process(double grapTime, double processTime, double reportTime)
    {
        GrapTime = grapTime;
        ProcessTime = processTime;
        ReportTime = reportTime;
    }

    private List<TimeFrame> timeFrames = new List<TimeFrame>();
    public void AddFrame(string frameId, string result)
    {
        TimeFrame newFrame = new TimeFrame { Id = frameId, Result = result };
        TimeFrame prevFrame = timeFrames.Count > 0 ? timeFrames[timeFrames.Count - 1] : null;
        
        if (prevFrame == null) // Frame đầu tiên
        {
            newFrame.GrapTime_Start = 0;
            newFrame.GrapTime_End = GrapTime;
            newFrame.ProcessTime_Start = GrapTime;
            newFrame.ProcessTime_End = newFrame.ProcessTime_Start + ProcessTime;
            newFrame.ReportTime_Start = newFrame.ProcessTime_End;
            newFrame.ReportTime_End = newFrame.ReportTime_Start + ReportTime;

        }    
        else
        {
            newFrame.GrapTime_Start = prevFrame.GrapTime_End;
            newFrame.GrapTime_End = newFrame.GrapTime_Start + GrapTime;

            newFrame.ProcessTime_Start = Math.Max(newFrame.GrapTime_End, prevFrame.ProcessTime_End);
            newFrame.ProcessTime_End = newFrame.ProcessTime_Start + ProcessTime;

            newFrame.ReportTime_Start = Math.Max(newFrame.ProcessTime_End, prevFrame.ReportTime_End);
            newFrame.ReportTime_End = newFrame.ReportTime_Start + ReportTime;
        } 
        
        timeFrames.Add(newFrame);

        PrintFrame(newFrame);
    }

    public void PrintFrame(TimeFrame f)
    {
        Console.WriteLine($"Frame {f.Id}: Grab[{f.GrapTime_Start}-{f.GrapTime_End}] Process[{f.ProcessTime_Start}-{f.ProcessTime_End}] Report[{f.ReportTime_Start}-{f.ReportTime_End}] -> {f.Result}");
        
    }

    public void TotalTime()
    {
        if (timeFrames.Count > 0)
        {
            TimeFrame last = timeFrames[timeFrames.Count -1];
            double totalTime = last.ReportTime_End;

            Console.WriteLine($"Total time: {totalTime}ms");

            double throughput = (double)((timeFrames.Count) * 1000) / totalTime;

            Console.WriteLine($"Throughput: {throughput:F1} fps");
        }
        else
        {
            Console.WriteLine("Total time: 0ms");
        }    
        
    }

    public void Yield()
    {
        int passFrame = timeFrames.Count(p => p.Result == "PASS");
        int failFrame = timeFrames.Count(p => p.Result == "FAIL");
        double yield = (double)(passFrame * 100) / (passFrame + failFrame);

        Console.WriteLine($"Pass: {passFrame}, Fail: {failFrame}, Yield: {yield:F1}%");
    }

    public void Speedup()
    {
        double sequentialTime = (timeFrames.Count) * (GrapTime + ProcessTime + ReportTime);
        TimeFrame last = timeFrames[timeFrames.Count - 1];
        double totalTime = last.ReportTime_End;

        double speedup = (double)sequentialTime / totalTime;

        Console.WriteLine($"Speedup vs sequential: {speedup:F1}x");
    }

}

class Program
{
    static void Main()
    {
        string[] s = Console.ReadLine().Split(' ', 3, StringSplitOptions.RemoveEmptyEntries); // Nhập thời gian Grap, Process và Report

        if (!double.TryParse(s[0], out double grapTime)) return;
        if (!double.TryParse(s[1], out double processTime)) return;
        if (!double.TryParse(s[2], out double reportTime)) return;

        Process process = new Process(grapTime, processTime, reportTime);

        int N; 
        try
        {
            N = int.Parse(Console.ReadLine()); // Nhập số Frame 
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

        for (int i = 0; i < N; i++)
        {
            string[] line = Console.ReadLine().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

            if (line.Length != 2) return;

            string id = line[0];
            string result = line[1];

            process.AddFrame(id, result);
        }

        process.TotalTime();
        process.Yield();
        process.Speedup();
    }
}