using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

class Log
{
    public DateTime Time { get; set; }
    public string Level { get; set; }
    public string Message { get; set; }
}

class LogParser
{
    private static List<Log> logs = new List<Log>();

    public static void AddLog(Log log)
    {
        logs.Add(log);
        Console.WriteLine($"Parsed: {log.Level} at {log.Time:yyyy-MM-dd HH:mm:ss}");
    }

    public static void FilterLog(string level)
    {
        var merge = logs.Where(p => p.Level == level);
        if (merge.Any())
        {
            foreach (var item in merge)
            {
                Console.WriteLine($"[{item.Time:yyyy-MM-dd HH:mm:ss}] {item.Level}: {item.Message}");
                                         
            }    
        }   
        else
        {
            Console.WriteLine($"No logs");
        }    
    }

    public static void CountByLevel()
    {
        var merge = logs
            .GroupBy(p => p.Level)
            .OrderBy(i => i.Key);
        foreach (var item in merge)
        {
            Console.WriteLine($"{item.Key}: {item.Count()}");
        }
    }

    public static void Between(DateTime date1, DateTime date2)
    {
        var result = logs.Where(p => p.Time.Date >= date1 && p.Time.Date <= date2);
        if (result.Any())
        {
            foreach (var item in result)
            {
                Console.WriteLine($"[{item.Time:yyyy-MM-dd HH:mm:ss}] {item.Level}: {item.Message}");
            }
        }
        else
        {
            Console.WriteLine("No logs");
        }    
    }
}


class Program
{
    static void Main()
    {
        if (!int.TryParse(Console.ReadLine(), out int N) || N < 1) return;

        string[][] arr = new string[N][];
        for (int i = 0; i < N; i++)
        {
            string s = Console.ReadLine();
            arr[i] = s.Split(' ', 5, StringSplitOptions.RemoveEmptyEntries);

        }

        for (int i = 0; i < N; i++)
        {
            switch (arr[i][0])
            {
                case "LOG":
                    {
                        if (arr[i].Length == 5)
                        {
                            Log log = new Log();
                            DateTime date = DateTime.Parse(arr[i][1] + " " + arr[i][2]);
                            string level = arr[i][3].Substring(1, arr[i][3].Length - 2);
                            string message = arr[i][4];
                            log.Time = date;
                            log.Level = level;
                            log.Message = message;

                            LogParser.AddLog(log);
                            break;
                        }
                        else
                        {
                            break;
                        }    
                    }
                case "FILTER":
                    {
                        if (arr[i].Length == 2)
                        {
                            LogParser.FilterLog(arr[i][1]);
                            break;
                        }   
                        else
                        {
                            break;
                        }    
                    }
                case "COUNT_BY_LEVEL":
                    {
                        if (arr[i].Length == 1)
                        {
                            LogParser.CountByLevel();
                            break;
                        }   
                        else
                        {
                            break;
                        }    
                    }
                case "BETWEEN":
                    {
                        if (arr[i].Length == 3)
                        { 
                            if (!DateTime.TryParse(arr[i][1], out DateTime date1)) return;
                            if (!DateTime.TryParse(arr[i][2], out DateTime date2)) return;

                            LogParser.Between(date1, date2);
                            break;
                        }   
                        else
                        {
                            break;
                        }    
                    }
            }
        }    
    }
}