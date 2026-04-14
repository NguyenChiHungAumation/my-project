using System;
using System.Collections.Generic;
using System.Text;

class Alarm
{
    public string Time {  get; set; }
    public string Code { get; set; }
    public string Message { get; set; }

}

class AlarmProcessing
{
    private static Queue<Alarm> alarmQueue = new Queue<Alarm>();

    public static void AddAlarm(Alarm alarm)
    {
        alarmQueue.Enqueue(alarm);
        Console.WriteLine($"Added: {alarm.Code}-{alarm.Message}-{alarm.Time}");
    }

    public static void ShowAlarm()
    {
        if(alarmQueue.Count > 0)
        {
            foreach (var item in alarmQueue)
            {
                Console.WriteLine($"{item.Code}-{item.Message}-{item.Time}");
            }    
        }
        else
        {
            Console.WriteLine("Danh sách Alarm rỗng!");
        }    
    }

    public static void ResetaAlarm()
    {
        if (alarmQueue.Count > 0)
        {
            while(alarmQueue.Count > 0)
            {
                Alarm item = alarmQueue.Dequeue();
                Console.WriteLine($"Đang sử lý Alarm: {item.Code}-{item.Message}-{item.Time}");
                
            }

            Console.WriteLine("Đã xử lý xong");
        }
        else
        {
            Console.WriteLine("Danh sách Alarm rỗng!");
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

        for(int i = 0; i < N; i++)
        {
            arr[i] = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (arr[i].Length == 0) return;
        }
        
        for(int i = 0;i < N; i++)
        {
            switch(arr[i][0].ToUpper())
            {
                case "ADD":
                    {
                        if (arr[i].Length ==4)
                        {
                            Alarm alarm = new Alarm();
                            alarm.Code = arr[i][1];
                            alarm.Message = arr[i][2];
                            alarm.Time = arr[i][3];

                            AlarmProcessing.AddAlarm(alarm);
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Câu lệnh {arr[i][0]} sai");
                            break;
                        }    
                    }
                case "SHOW":
                    {
                        if (arr[i].Length == 1)
                        {
                            AlarmProcessing.ShowAlarm();
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Câu lệnh {arr[i][0]} sai");
                            break;
                        }    
                    }
                case "RESET":
                    {
                        if (arr[i].Length == 1)
                        {
                            AlarmProcessing.ResetaAlarm();
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Câu lệnh {arr[i][0]} sai");
                            break;
                        }
                    }
            }
        }    
    }
}