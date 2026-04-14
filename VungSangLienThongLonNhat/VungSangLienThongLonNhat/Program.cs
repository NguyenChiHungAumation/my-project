using System;
using System.Collections.Generic;

public interface IDevice
{
    public string Name { get; set; }
    public string Type { get; set; }

    public void Initialize();
    public string GetInfo();
}

class CameraDevice : IDevice
{
    public string Name { get; set; }
    public string Type { get; set; }

    public CameraDevice(string name)
    {
        Name = name;
        Type = "Camera";
    }
    public void Initialize()
    {
        Console.WriteLine($"{Name}: camera initialized");    
    }

    public string GetInfo()
    {
        return $"[Camera] {Name}";
    }

}

class PLCDevice : IDevice
{
    public string Name { get; set; }
    public string Type { get; set; }

    public PLCDevice(string name)
    {
        Name = name;
        Type = "PLC";
    }

    public void Initialize()
    {
        Console.WriteLine($"{Name}: PLC initialized");
    }

    public string GetInfo()
    {
        return $"[PLC] {Name}";
    }
}

class RobotDevice : IDevice
{
    public string Name { get; set; }
    public string Type { get; set; }

    public RobotDevice(string name)
    {
        Name = name;
        Type = "Robot";
    }

    public void Initialize()
    {
        Console.WriteLine($"{Name}: robot initialized");
    }

    public string GetInfo()
    {
        return $"[Robot] {Name}";
    }

}


static class DeviceFactory
{
    
    public static IDevice Create(string type, string name)
    {
        if (type == "Camera")
        {
            IDevice cameraDevice = new CameraDevice(name);
            Console.WriteLine($"Created Camera '{name}'");
            return cameraDevice;
            
        }

        else if (type == "PLC")
        {
            IDevice pLCDevice = new PLCDevice(name);
            Console.WriteLine($"Created PLC '{name}'");
            return pLCDevice;
            
        }

        else if (type == "Robot")
        {
            IDevice robotDevice = new RobotDevice(name);
            Console.WriteLine($"Created Robot '{name}'");
            return robotDevice;
        }
        else
        {
            Console.WriteLine($"ERROR: Unknown device type '{type}'");
            return null;
        }




    }
}

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N) || N < 1) return;

        string[][] arr = new string[N][];

        for (int i = 0; i < N; i++)
        {
            string a = Console .ReadLine();
            arr[i] = a.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }
        List<IDevice> devices = new List<IDevice>();

        for (int i = 0; i < N; i++)
        {
           
            if (arr[i][0] == "CREATE" && arr[i].Length == 3)
            {
                IDevice device = DeviceFactory.Create(arr[i][1], arr[i][2]);
                if (device != null)
                {
                    devices.Add(device);
                    continue;
                }
                else continue;
                
            }

            if (arr[i][0] == "INIT" && arr[i].Length == 2)
            {
                for (int j = 0; j < devices.Count; j++)
                {
                    if (arr[i][1] == devices[j].Name)
                    {
                        devices[j].Initialize();
                        break;
                    } 
                    else continue;

                }
                continue;

            }

            if (arr[i][0] == "INFO" && arr[i].Length == 2)
            {
                for (int j = 0; j < devices.Count; j++)
                {
                    if (arr[i][1] == devices[j].Name)
                    {
                        Console.WriteLine(devices[j].GetInfo());
                        break;
                    }
                    else continue;

                }
                continue;

            }

            if (arr[i][0] == "LIST" && arr[i].Length == 1)
            {
                for (int j = 0; j < devices.Count; j++)
                {
                    Console.WriteLine(devices[j].GetInfo());
                }
                continue;

            }


        }


    }
}
