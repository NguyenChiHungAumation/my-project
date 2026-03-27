using System;
using System.Collections.Generic;
class Device
{
    public string Name { get; set; }
    public bool IsConnected { get; private set; } = false;

    /// <summary>
    /// method connect
    /// </summary>
    public void Connect()
    {
        IsConnected = true;
    }

    public void Disconnect()
    {
        IsConnected = false;
    }

    public virtual string GetInfo()
    {
        return $"Device: {Name}";
    }
}



class Camera : Device
{
    public string Resolution { get; set; }
    public override string GetInfo()
    {
        return $"Camera: {Name} [{Resolution}]";
            }
}

class PLC : Device
{
    public string IPAddress { get; set; }
    public override string GetInfo()
    {
        return $"PLC: {Name} ({IPAddress})";
    }
}

class Robot : Device
{
    public int AxisCount { get; set; }
    public override string GetInfo()
    {
        return $"Robot: {Name} {AxisCount}-axis";
    }
}

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N) || N < 0) return;

        string[][] arr = new string[N][];

       for (int i = 0; i < N; i++)
        {
            string a = Console.ReadLine();
            arr[i] = a.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

       List<Device> devices = new List<Device>();
       
       for (int i = 0; i < N; i++)
        {
            if (arr[i][0] == "CREATE" && arr[i].Length == 4)
            {
                if (arr[i][1] == "CAMERA")
                {
                    Camera camera = new Camera();
                    camera.Name = arr[i][2];
                    camera.Resolution = arr[i][3];
                    devices.Add(camera);
                    Console.WriteLine($"Created Camera '{camera.Name}'");
                    continue;
                }

                else if (arr[i][1] == "PLC")
                {
                    PLC plc = new PLC();
                    plc.Name = arr[i][2];
                    plc.IPAddress = arr[i][3];
                    devices.Add(plc);
                    Console.WriteLine($"Created PLC '{plc.Name}'");
                    continue;
                }

                else if (arr[i][1] == "ROBOT")
                {
                    Robot robot = new Robot();
                    robot.Name = arr[i][2];
                    if (!int.TryParse(arr[i][3], out int axisCount)) return;
                    robot.AxisCount = axisCount;
                    devices.Add(robot);
                    Console.WriteLine($"Created Robot '{robot.Name}'");
                    continue;
                }
                else continue;


            }
            
            else if (arr[i][0] == "CONNECT" && arr[i].Length == 2)
            {
                for (int j = 0; j < devices.Count; j++)
                {
                    if (arr[i][1] == devices[j].Name)
                    {
                        devices[j].Connect();
                        //Console.WriteLine($"{devices[j].Name}: Connected");
                        break;
                    }
                    else continue;
                }    
            }
            

            else if (arr[i][0] == "DISCONNECT" && arr[i].Length == 2)
            {
                for (int j = 0; j < devices.Count; j++)
                {
                    if (arr[i][1] == devices[j].Name)
                    {
                        devices[j].Disconnect();
                        //Console.WriteLine($"{devices[j].Name}: Disconnected");
                        break;
                    }
                    else continue;
                }
            }
            
            else if (arr[i][0] == "INFO" && arr[i].Length == 2)
            {
                for (int j = 0; j < devices.Count; j++)
                {
                    if (arr[i][1] == devices[j].Name)
                    {
                        devices[j].GetInfo();
                        Console.WriteLine(devices[j].GetInfo());

                        break;
                    }
                    else continue;
                }
            }
            
            else if (arr[i][0] == "STATUS" && arr[i].Length == 2)
            {
                for (int j = 0; j < devices.Count; j++)
                {
                    if (arr[i][1] == devices[j].Name)
                    {
                        if (devices[j].IsConnected == true)
                        {
                            Console.WriteLine($"{devices[j].Name}: Connected");
                        }
                        else
                            Console.WriteLine($"{devices[j].Name}: Disconnected");
                        break;
                    }
                    else continue;
                }
            }    
        }    

        
    }
}
