using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;

class TaskInfo
{
    public string TaskName { get; set; }
    public int Duration { get; set; }
    public int Remaining {  get; set; }
    public string Status {  get; set; }

    public TaskInfo(string taskName, int duration, string status)
    {
        TaskName = taskName;
        Duration = duration;
        Remaining = duration;
        Status = status;
    }
}

class Process
{
    private List<TaskInfo> taskInfos = new List<TaskInfo>();

    private int tick = 0;
    public void StartTask(string taskName, int duration)
    {
        bool exitTaskRuning = false;
        bool exitCancel = false;
        foreach (var task in taskInfos)
        {
            if (task.TaskName == taskName && task.Remaining != 0)
            {
                exitTaskRuning = true;
                //Console.WriteLine($"Task '{taskName}' already running");
            }
            
            if (task.Status == "CANCELLED" && task.Remaining != 0)
            {
                exitCancel = true;
            }    
        }  
        
        if (!exitTaskRuning || exitCancel)
        {
            taskInfos.Add(new TaskInfo(taskName, duration, "RUNNING"));

            Console.WriteLine($"Task '{taskName}' started (duration: {duration} ticks)");
        } 
        else
        {
            
            Console.WriteLine($"Task '{taskName}' already running");
        }    

    }

    public void TickTask()
    {
        tick++;
        Console.WriteLine($"--- Tick {tick} ---");
        foreach (var task in taskInfos)
        {
            if (task.Status == "RUNNING" && task.Remaining > 1)
            {
                task.Remaining--;

                Console.WriteLine($"{task.TaskName}: {task.Remaining} remaining");
            }
            else
            {
                if (task.Status == "RUNNING" && task.Remaining == 1)
                {
                    task.Remaining--;
                }
                if (task.Status == "RUNNING" && task.Remaining == 0)
                {
                    task.Status = "COMPLETED";

                    Console.WriteLine($"{task.TaskName}: {task.Status}");
                }

                if (task.Status != "RUNNING" && task.Remaining != 1)
                {
                    Console.WriteLine($"{task.TaskName}: {task.Status}");
                }
            }    
        }    
    }

    public void CancelTask(string taskName)
    {
        bool exitTaskName = false;
        bool notRunning = false;
        bool running = false;

           
        foreach (var task in taskInfos)
        {
            if (task.TaskName == taskName && task.Status =="COMPLETED" )
            {
                exitTaskName = true;
                notRunning = true;
                break;
            }  
            if (task.TaskName == taskName && task.Status == "RUNNING")
            {
                exitTaskName = true;
                running = true;
                task.Status = "CANCELLED";
                break;
            } 
            
            
        } 
        
        if (notRunning == true || exitTaskName == false)
        {
            Console.WriteLine($"Task '{taskName}' not running");
        } 
        
        if (running == true)
        {
            Console.WriteLine($"Task '{taskName}' cancelled'");
        }    
    }

    public void StatusTask()
    {
        foreach (var task in taskInfos)
        {
            Console.WriteLine($"{task.TaskName}: {task.Status}");
        }    
    }
}

class Program
{

    static void Main()
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
            try
            {
                string[] line = data[i].Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);

                switch (line[0])
                {
                    case "START":
                        {
                            if (line.Length == 3)
                            {
                                string taskName = line[1];
                                int duration = int.Parse(line[2]);
                                process.StartTask(taskName, duration);
                                break;
                            }   
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }    
                        }
                    case "TICK":
                        {
                            if (line.Length == 1)
                            {
                                process.TickTask();
                                break;
                            }   
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }    
                        }
                    case "CANCEL":
                        {
                            if (line.Length == 2)
                            {
                                string taskName = (string)line[1];
                                process.CancelTask(taskName);
                                break;
                            }   
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }    
                        }
                    case "STATUS":
                        {
                            if (line.Length == 1)
                            {
                                process.StatusTask();
                                break;
                            }   
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }    
                        }
                    default:
                        {
                            throw new FormatException($"{data[i]} sai định dạng");
                        }
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($" Lỗi: {ex.Message}");
            }
        }    
    }
}