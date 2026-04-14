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

        for (int i = 0; i < N; i++)
        {
            arr[i] = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }   
        
        Dictionary<string, double> ss = new Dictionary<string, double>();

        
        double avg = 0;

        for (int i = 0;i < N;i++)
        {
            if (arr[i][0] == "SET" && arr[i].Length == 3)
            {
                if (!double.TryParse(arr[i][2], out double value)) return;

                ss[arr[i][1]] = value;
                Console.WriteLine($"Set {arr[i][1]} = {value:F2}");
                continue;
     
            }

            else if(arr[i][0] == "GET" && arr[i].Length == 2)
            {
                if (ss.ContainsKey(arr[i][1]))
                {
                    double value = ss[arr[i][1]];
                    Console.WriteLine($"{arr[i][1]} = {value:F2}");
                    continue;  
                }
                else Console.WriteLine($"Sensor '{arr[i][1]}' not found");

            }


            else if (arr[i][0] == "DELETE" && arr[i].Length == 2)
            {
                if (ss.ContainsKey(arr[i][1]))
                {
                    ss.Remove(arr[i][1]);
                    Console.WriteLine($"Deleted {arr[i][1]}");
                    continue;
                }
                else Console.WriteLine($"Sensor '{arr[i][1]}' not found");

            }

            else if (arr[i][0] == "LIST" && arr[i].Length == 1)
            {
                if (ss.Count != 0)
                {
                    foreach (var item in ss)
                    {
                        Console.WriteLine($"{item.Key}: {item.Value:F2}");
                    }
                    continue;
                }
                else Console.WriteLine($"No sensors");
            }

            else if (arr[i][0] == "AVG" && arr[i].Length == 1)
            {
                if (ss.Count != 0)
                {
                    double sum = 0;
                    foreach (var item in ss)
                    {
                        sum += item.Value;
                    }
                    avg = sum / ss.Count;
                    Console.WriteLine($"Average: {avg:F2}");
                    continue;
                }
                else Console.WriteLine($"No sensors");
            }
        }    
    }
}