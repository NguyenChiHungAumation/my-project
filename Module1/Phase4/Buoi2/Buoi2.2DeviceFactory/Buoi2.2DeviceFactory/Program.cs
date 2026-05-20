using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

interface IDevice
{
    string Name { get; }
    void Initialize();
    void Execute(ICommand command);
    
}

interface ICommand
{

}

class MoveCommand : ICommand
{
    public int Pos;
    public MoveCommand(int pos)
    {
        Pos = pos;
    }
}
class ReadCommand : ICommand
{
    public double Value;
    public ReadCommand(double value)
    {
        Value = value;
    }
}
class SendComnand : ICommand
{
    public string Data;
    public SendComnand(string data)
    {
        Data = data;
    }
}
class Servo : IDevice
{
    public string Name { get; }
    public int Position { get; set; }
    public int Speed { get; set; }
    public Servo (string name, int position, int speed)
    {
        Name = name;
        Position = position;
        Speed = speed;
    }

    public void Initialize()
    {
        Console.WriteLine($"Created SERVO '{Name}' [pos={Position}, speed={Speed}]");
    }

    public void Execute (ICommand command)
    {
        if (command is MoveCommand move)
        {
            int time = Math.Abs(move.Pos - Position) * 1000 / Speed;
            Console.WriteLine($"{Name}: MOVE to {move.Pos}, time={time}ms");

            Position = move.Pos;
        }    
            
    }
    
}

class Sensor : IDevice
{
    public string Name { get; }
    public string Type { get; set; }
    public double Max { get; set; }
    public double Min { get; set; }
    public Sensor(string name, string type, double min, double max)
    {
        Name = name;
        Type = type;
        Max = max;
        Min = min;
    }

    public void Initialize()
    {
        Console.WriteLine($"Created SENSOR '{Name}' [type={Type}, range={Min}-{Max}]");
    }

    public void Execute(ICommand command)
    {
        if (command is ReadCommand Read)
        {
            string result = Read.Value >= Min && Read.Value <= Max ? "IN_RANGE" : "OUT_RANGE";

            Console.WriteLine($"{Name}: READ {Read.Value} -> {result}");
        }
    }
    
}

class Plc : IDevice
{
    public string Name { get; }
    public string Protocol { get; set; }
    public string Address { get; set; }
    public Plc(string name, string protocol, string address)
    {
        Name = name;
        Protocol = protocol;
        Address = address;
    }

    public void Initialize()
    {
        Console.WriteLine($"Created PLC '{Name}' [protocol={Protocol}, addr={Address}]");
    }

    public void Execute(ICommand command)
    {
        if (command is SendComnand Send)
        {
            Console.WriteLine($"{Name}: SEND '{Send.Data}' via {Protocol} to {Address}");
        }    
    }
    
}
class Process
{

    Dictionary<string, Servo> servos = new Dictionary<string, Servo>();
    Dictionary<string, Sensor> sensors = new Dictionary<string, Sensor>();
    Dictionary<string, Plc> plcs = new Dictionary<string, Plc>();
    int totalCount = 0;

    public void Final()
    {
        totalCount = sensors.Count + servos.Count + plcs.Count;

        Console.WriteLine($"Devices: {totalCount} ({servos.Count}S, {sensors.Count}R, {plcs.Count}P)");
    }

    public void Command(string command)
    {
        string[] parts = command.Split(' ', 6);

        string action = parts[0];
        switch (action)
        {
            case "CREATE":
                {
                    if (parts.Length == 5)
                    {
                        bool isServo = (parts[2] == "SERVO") ? true : false;
                        if (isServo)
                        {
                            Servo servo = new Servo(parts[1], int.Parse(parts[3]), int.Parse(parts[4]));

                            servo.Initialize();

                            servos.Add(parts[1], servo);
                            break;
                        }   
                        else
                        {
                            Plc plc = new Plc(parts[1], parts[3], parts[4]);

                            plc.Initialize();

                            plcs.Add(parts[1], plc);
                            break;
                        }

                        break;
                    }
                    if (parts.Length == 6 && parts[2] == "SENSOR")
                    {
                        Sensor sensor = new Sensor(parts[1], parts[3], int.Parse(parts[4]), int.Parse(parts[5]));

                        sensor.Initialize();

                        sensors.Add(parts[1], sensor);
                        break;
                    }
                    break;
                }
            case "ACTION":
                {
                    if (parts.Length == 4)
                    {
                        if (parts[2] == "MOVE") // is Servo
                        {
                            if (servos.TryGetValue(parts[1], out var result))
                            {
                                ICommand command1 = new MoveCommand(int.Parse(parts[3]));
                                
                                result.Execute(command1);
                                break;
                            } 
                            else
                            {
                                Console.WriteLine($"ERROR: '{parts[1]}' not found");
                                break;
                            }    
                        }   
                        if (parts[2] == "READ") // is Sensor
                        {
                            if (sensors.TryGetValue(parts[1], out var result))
                            {
                                ICommand command1 = new ReadCommand(double.Parse(parts[3]));
                                result.Execute(command1);

                                break;
                            }
                            else
                            {
                                Console.WriteLine($"ERROR: '{parts[1]}' not found");
                                break;
                            }
                        }
                        if (parts[2] == "SEND") // is Plc
                        {
                            if (plcs.TryGetValue(parts[1], out var result))
                            {
                                ICommand command1 = new SendComnand(parts[3]);
                                result.Execute(command1);

                                break;
                            }
                            else
                            {
                                Console.WriteLine($"ERROR: '{parts[1]}' not found");
                                break;
                            }
                        }

                        break;
                    }

                    break;
                }
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
            N = int.Parse(Console.ReadLine()); // Nhập số Frame 
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

        for (int i = 0; i < N; i++)
        {
            string line2 = Console.ReadLine();
            if (line2 == null) return;

            process.Command(line2);
        }

        process.Final();
    }
}

