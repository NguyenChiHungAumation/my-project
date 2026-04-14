using System;
using System.Collections.Generic;
using System.Text;


class Log
{
    public string Time {  get; set; }
    public string Level { get; set; }
    public string Message { get; set; }

}

class LogEntry
{
    private static List<Log> logs = new List<Log>();
    private static Dictionary<string, int> NumLog = new Dictionary<string, int>();
    public static void AddLog(Log log)
    {
        logs.Add(log);
        Console.WriteLine($"Added: {log.Time} {log.Level} {log.Message}");
    }

    public static void CountLog()
    {
        NumLog.Clear();
        foreach (var log in logs)
        {
            if(NumLog.ContainsKey(log.Level))
            {
                NumLog[log.Level]++;
            }
            else
            {
                NumLog[log.Level] = 1;
            }    
        }
        
        foreach (var item in NumLog)
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }    
    }

    public static void CountLogName(string name)
    {
        NumLog.Clear();
        foreach (var log in logs)
        {
            if (NumLog.ContainsKey(log.Level))
            {
                NumLog[log.Level]++;
            }
            else
            {
                NumLog[log.Level] = 1;
            }
        }

        foreach (var item in NumLog)
        {
            if (item.Key == name)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }

    }

    
}

class Program
{
    static void Main()
    {
        if (!int.TryParse(Console.ReadLine(), out int N)) return;
        string[][] arr = new string[N][];
        for (int i = 0; i < N; i++)
        {
            arr[i] = Console.ReadLine().Split(' ', 5, StringSplitOptions.RemoveEmptyEntries);
            if (arr[i].Length == 0) return;
        }
        
        for (int i = 0;i < N; i++)
        {
            switch(arr[i][0].ToUpper())
            {
                case "ADD":
                    {
                        if (arr[i].Length == 5)
                        {
                            Log log = new Log();
                            log.Time = arr[i][1] + " " + arr[i][2];
                            log.Level = arr[i][3];
                            log.Message = arr[i][4];
                            LogEntry.AddLog(log);
                            break;
                        }   
                        else
                        {
                            Console.WriteLine($"Câu lệnh {arr[i][0]} sai!");
                            break;
                        }    
                    }
                case "COUNT":
                    {
                        if (arr[i].Length == 1)
                        {
                            LogEntry.CountLog();
                            break;
                        }
                        else if(arr[i].Length == 2)
                        {
                            LogEntry.CountLogName(arr[i][1]);
                            break;
                        }    
                        else
                        {
                            Console.WriteLine($"Câu lệnh {arr[i][0]} sai!");
                            break;
                        } 
                            
                    }
            }
        }    
    }
}

