using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Alram
{
    public string Level { get; set; }
    public string Message { get; set; }
}

class AlarmSummary
{
    public static List<Alram> alrams = new List<Alram>();

    public static void AddAlarm(Alram alram)
    {
        alrams.Add(alram);
        Console.WriteLine($"Added: {alram.Level} - {alram.Message}");
    }
    public static void LevelAlarm()
    {
        var group = alrams.GroupBy(x => x.Level);
        Console.WriteLine($"== Nhóm theo Level ==");
        foreach (var item in group)
        {
            foreach (var item2 in item)
            {
                Console.WriteLine($"{item2.Level} - {item2.Message}");
            }    
        }    
    }
    public static void CountAlarm()
    {
        var group = alrams.GroupBy(x => x.Level);
        Console.WriteLine($"== Đếm số lượng mỗi Level ==");
        foreach (var item in group)
        {
            Console.WriteLine($"Level: {item.Key} - Count: {item.Count()}");
        }    
    }
    public static void MaxAlarm()
    {
        var group = alrams.GroupBy(x => x.Level);
        var max = group.Max(p => p.Count());
        var maxGroups = group.Where(g => g.Count() == max);
        Console.WriteLine($"== Đếm số lượng mỗi Level Max ==");
        foreach (var item in maxGroups)
        {
            Console.WriteLine($"Level: {item.Key} - Count: {item.Count()}");
        }    
    }

    public static void MessageAlarm()
    {
        var group = alrams.GroupBy(x => x.Level);
        var low = group.Where(x => x.Key == "LOW");
        var medium = group.Where(x => x.Key == "MEDIUM");
        var high = group.Where(x => x.Key == "HIGH");
        Console.WriteLine($"== Nhóm Message theo Level ==");
        foreach (var item in group)
        {
            Console.WriteLine($"{item.Key}: ");
            foreach (var item2 in item)
            {
                Console.WriteLine($"{item2.Message}");
            }
        }
    }

}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        if (!int.TryParse(Console.ReadLine(), out int N)) return;

        string[][] arr = new string[N][];
        for (int i = 0; i < N; i++)
        {
            string s = Console.ReadLine();
            arr[i] = s.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
            if (arr[i].Length == 0) return;
        }

        for (int i = 0; i < N; i++)
        {
            switch (arr[i][0].ToUpper())
            {
                case "ADD":
                    {
                        if (arr[i].Length == 3)
                        {
                            Alram alram = new Alram();
                           alram.Level = arr[i][1];
                            alram.Message = arr[i][2];

                            AlarmSummary.AddAlarm(alram);
                            break;
                        }
                        
                        else
                        {
                            Console.WriteLine($"Câu lệnh sai cấu trúc");
                            break;
                        }    
                    }
                

                    default:
                    {
                        Console.WriteLine($"Câu lệnh sai cấu trúc");
                        break;
                    }
            }

            
        }

        AlarmSummary.LevelAlarm();
        AlarmSummary.CountAlarm();
        AlarmSummary.MaxAlarm();
        AlarmSummary.MessageAlarm();
    }
}