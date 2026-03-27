using System;
using System.Collections.Generic;
 public interface IControllable
{
    public string Name { get; set; }
    public string Status { get; set; }

    public void Start();
    public void Stop();
    public void Reset();

}

class Motor : IControllable
{
    public string Name { get; set; }
    public int Speed { get; set; }

    public string Status { get; set; } = "Stopped";
    public void Start()
    {
        if (Status == "Stopped")
        {
            Status = "Running";
            Console.WriteLine($"{Name}: started at speed {Speed}");
        }

        else if (Status == "Running")
            Console.WriteLine($"{Name}: already running");
    }

    public void Stop()
    {
        if (Status == "Running")
        {
            Status = "Stopped";
            Console.WriteLine($"{Name}: stopped");
        }

        else if (Status == "Stopped")
            Console.WriteLine($"{Name}: already stopped");
    }

    public void Reset()
    {
        Status = "Stopped";
        Speed = 0;
        Console.WriteLine($"{Name}: reset");
    }

}


class Conveyor : IControllable
{
    public string Name{ get; set; }
    //public int Speed { get; set; }

    public string Status { get; set; } = "Stopped";
    public string Direction { get; set; }

    public void Start()
    {
        if (Status == "Stopped")
        {
            Status = "Running";
            Console.WriteLine($"{Name}: running {Direction}");
        }

        else if (Status == "Running")
            Console.WriteLine($"{Name}: already running");
    }
    public void Stop()
    {
        if (Status == "Running")
        {
            Status = "Stopped";
            Console.WriteLine($"{Name}: stopped");
        }

        else if (Status == "Stopped")
            Console.WriteLine($"{Name}: already stopped");
    }

    public void Reset()
    {
        Status = "Stopped";
        Direction = "Forward";
        Console.WriteLine($"{Name}: reset to Forward");
    }

}

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N) || N < 0) return;

        string[][] mang = new string[N][];

        for (int i = 0; i < N; i++)
        {
            string a = Console.ReadLine();
            mang[i] = a.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }    

        List<IControllable> controllables = new List<IControllable>();

        for (int i = 0; i < N; i++)
        {
            if (mang[i][0] == "CREATE" && mang[i].Length == 4)
            {
                if (mang[i][1] == "MOTOR")
                {
                    Motor motor = new Motor();
                    motor.Name = mang[i][2];
                    if (!int.TryParse(mang[i][3], out int speed)) return;
                    motor.Speed = speed;
                    controllables.Add(motor);
                    Console.WriteLine($"Created Motor '{motor.Name}'");
                    continue;

                }
                
                else if (mang[i][1] == "CONVEYOR")
                {
                    Conveyor conveyor = new Conveyor();
                    conveyor.Name = mang[i][2];
                    conveyor.Direction = mang[i][3];
                    controllables.Add(conveyor);
                    Console.WriteLine($"Created Conveyor '{conveyor.Name}'");
                    continue;
                }    
            }
            
            else if (mang[i][0] == "START" && mang[i].Length == 2)
            {
                for (int j = 0; j < controllables.Count; j++)
                {
                    if (mang[i][1] == controllables[j].Name)
                    {
                        controllables[j].Start();
                        break;
                    }
                    else continue;
                } 
                continue;
            }

            else if (mang[i][0] == "STOP" && mang[i].Length == 2)
            {
                for (int j = 0; j < controllables.Count; j++)
                {
                    if (mang[i][1] == controllables[j].Name)
                    {
                        controllables[j].Stop();
                        break;
                    }
                    else continue;
                }
                continue;
            }

            else if (mang[i][0] == "RESET" && mang[i].Length == 2)
            {
                for (int j = 0; j < controllables.Count; j++)
                {
                    if (mang[i][1] == controllables[j].Name)
                    {
                        controllables[j].Reset();
                        break;
                    }
                    else continue;
                }
                continue;
            }

            else if (mang[i][0] == "STATUS" && mang[i].Length == 2)
            {
                for (int j = 0; j < controllables.Count; j++)
                {
                    Console.WriteLine($"{controllables[j].Name}: {controllables[j].Status}");
                }
                continue;
            }


        }    


    }
}
