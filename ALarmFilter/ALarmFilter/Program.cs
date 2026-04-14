using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N)) return;

        string[][] arr = new string[N][];

        List<(string timestamp, string severity, string message)> alarm = new List<(string, string, string)>();

        for (int i = 0; i < N; i++)
        {
            arr[i] = Console.ReadLine().Split(' ', 4, StringSplitOptions.RemoveEmptyEntries);
            if (arr[i].Length == 0) return;
        }    



        for (int i = 0; i < N; i++)
        {
            if (arr[i][0] == "ADD" && arr[i].Length == 4)
            {
                if (arr[i][2] == "INFO" || arr[i][2] == "WARNING" || arr[i][2] == "ERROR")
                {
                    alarm.Add((arr[i][1], arr[i][2], arr[i][3]));
                    Console.WriteLine($"Added alarm at {arr[i][1]}");
                    continue;
                }
                else return;
            }
            
            else if (arr[i][0] == "FILTER" && arr[i].Length == 2)
            {
                if (arr[i][1] == "INFO" || arr[i][1] == "WARNING" || arr[i][1] == "ERROR")
                {
                    var info = alarm.Where(s => s.severity == arr[i][1]).ToList();
                    if (info.Count > 0)
                    {
                        foreach (var item in info)
                        {
                            Console.WriteLine($"[{item.timestamp}] {item.severity}: {item.message}");
                        }
                        continue;
                    }
                    else Console.WriteLine("No alarms");
                }
                else return;
                
            }

            else if (arr[i][0] == "SORT_TIME" && arr[i].Length == 1)
            {
                var info = alarm.OrderBy(s => s.timestamp).ToList();
                if (info.Count != 0)
                {
                    foreach (var item in info)
                    {
                        Console.WriteLine($"[{item.timestamp}] {item.severity}: {item.message}");
                    }
                    continue;
                }
                else Console.WriteLine("No alarms");
            }

            else if (arr[i][0] == "COUNT" && arr[i].Length == 2)
            {
                int count = 0;
                var info = alarm.Where(s => s.severity == arr[i][1]).ToList();
                count = info.Count;
                Console.WriteLine($"{arr[i][1]}: {count}");
                continue;
            }    

        }    
    }
}