using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Xml.Linq;

interface IAlertStrategies
{
    bool Check(int value);
    string Reason(int value);
}

class ThresholdStrategy : IAlertStrategies
{
    private int Threshold;

    public ThresholdStrategy(int threshold)
    {
        this.Threshold = threshold;
    }

    public bool Check(int value)
    {
        return value > Threshold;
    }

    public string Reason(int value)
    {
        return $"{value} > {Threshold}";
    }

}

class RangeStrategy : IAlertStrategies
{
    private int Min;
    private int Max;
    private bool isMin = false;
    private bool isMax = false;

    public RangeStrategy(int min, int max)
    {
        Min = min;
        Max = max;
    }

    public bool Check(int value)
    {
        isMin = false;
        isMax = false;

        if (value < Min)
        {
            isMin = true;
            return true;
        }    
        if (value > Max)
        {
            isMax = true;
            return true;
        } 
        return false;
    }

    public string Reason(int value)
    {
        if (isMin)
        {
            return $"{value} < {Min}";
        } 
        if(isMax)
        {
            return $"{value} > {Max}";
        }

        return null;
    }

}

class DeltaStrategy : IAlertStrategies
{
    private int Delta;
    private int? LastValue = null;
    private int lastDiff = 0;
    public DeltaStrategy(int delta)
    {
        Delta = delta;
    }

    public bool Check(int value)
    {
        if (LastValue == null)
        {
            LastValue = value;
            return false;
        }

        lastDiff = Math.Abs(value - LastValue.Value);

        bool result = lastDiff > Delta;
        LastValue = value;

        return result;

    }

    public string Reason(int value)
    {
        return $"{lastDiff} > {Delta}";
    }
}
class Monitor
{
    public string Name { get; set; }
    public IAlertStrategies Strategy { get; set; }
    public Monitor(string name, IAlertStrategies strategy)
    {
        Name = name;
        Strategy = strategy;
    }
}

class Sensor
{
    public string Name { get; set; }
    public string Type { get; set; }
    public List<Monitor> monitors { get; set; }
    public Sensor(string name, string type)
    {
        Name = name;
        Type = type;
        monitors = new List<Monitor>();
    }

    
}

class Process
{
    Dictionary<string, Sensor> sensors = new Dictionary<string, Sensor>();

    private int countAlerts = 0;

    public void CountAlert()
    {
        Console.WriteLine($"Total alerts: {countAlerts}");
    }

    public void Create(string name, string type)
    {
        if (sensors.ContainsKey(name))
        {
            Console.WriteLine($"ERROR: {name} exits");
            return;
        } 
        
        if (type != "TEMP" && type != "PRESSURE" && type != "FLOW")
        {
            Console.WriteLine("ERROR: wrong syntax");
            return;
        }
        
        Sensor sensor = new Sensor(name, type);

        sensors.Add(sensor.Name, sensor);

        Console.WriteLine($"Sensor {name} [{type}] created");
    }

    public void Monitor(string monitorName, string sensorName, string strategy, int[] param)
    {
        if (sensors.TryGetValue(sensorName, out var sensor))
        {
            if (strategy == "THRESHOLD")
            {
                if (param.Length != 1)
                {
                    Console.WriteLine("ERROR: wrong syntax");
                    return;
                }    

                IAlertStrategies alertStrategies = new ThresholdStrategy(param[0]);
                Monitor monitor = new Monitor(monitorName, alertStrategies);
                
                sensor.monitors.Add(monitor);

                Console.WriteLine($"{monitorName} watching {sensorName} with {strategy}({param[0]})");
            } 
            if (strategy == "RANGE")
            {
                if (param.Length != 2)
                {
                    Console.WriteLine("ERROR: wrong syntax");
                    return;
                }

                IAlertStrategies alertStrategies = new RangeStrategy(param[0], param[1]);
                Monitor monitor = new Monitor(monitorName, alertStrategies);

                sensor.monitors.Add(monitor);

                Console.WriteLine($"{monitorName} watching {sensorName} with {strategy}({param[0]}, {param[1]})");
            }

            if (strategy == "DELTA")
            {
                if (param.Length != 1)
                {
                    Console.WriteLine("ERROR: wrong syntax");
                    return;
                }

                IAlertStrategies alertStrategies = new DeltaStrategy(param[0]);
                Monitor monitor = new Monitor(monitorName, alertStrategies);

                sensor.monitors.Add(monitor);

                Console.WriteLine($"{monitorName} watching {sensorName} with {strategy}({param[0]})");
            }
        }   
        else
        {
            Console.WriteLine($"ERROR: {sensorName} not exits");
        }    
    }

    public void Read(string sensorName, int value)
    {
        if (sensors.TryGetValue(sensorName, out var sensor))
        {
            Console.WriteLine($"{sensorName}: {value}");

            for (int i = 0; i < sensor.monitors.Count; i++)
            {
                if (sensor.monitors[i].Strategy is ThresholdStrategy threshold)
                {
                    if (threshold.Check(value) == false)
                    {
                        Console.WriteLine($"    {sensor.monitors[i].Name}: OK");
                    } 
                    else
                    {
                        countAlerts++;

                        Console.WriteLine($"    {sensor.monitors[i].Name}: ALERT ({threshold.Reason(value)})");
                    }

                    continue;
                }

                if (sensor.monitors[i].Strategy is RangeStrategy range)
                {
                    if (range.Check(value) == false)
                    {
                        Console.WriteLine($"    {sensor.monitors[i].Name}: OK");
                    }
                    else
                    {
                        countAlerts++;

                        Console.WriteLine($"    {sensor.monitors[i].Name}: ALERT ({range.Reason(value)})");
                    }

                    continue;
                }

                if (sensor.monitors[i].Strategy is DeltaStrategy delta)
                {
                    if (delta.Check(value) == false)
                    {
                        Console.WriteLine($"    {sensor.monitors[i].Name}: OK");
                    }
                    else
                    {
                        countAlerts++;

                        Console.WriteLine($"    {sensor.monitors[i].Name}: ALERT ({delta.Reason(value)})");
                    }

                    continue;
                }
            }    
        }   
        else
        {
            Console.WriteLine($"ERROR: {sensorName} not exits");
        }    
    }

    public void Remove(string monitorName, string sensorName)
    {
        if (sensors.TryGetValue(sensorName, out var sensor))
        {
            Monitor monitor = sensor.monitors.Find(x => x.Name == monitorName);

            if (monitor == null)
            {
                Console.WriteLine($"ERROR: {monitorName} not exits");
            } 
            else
            {
                sensor.monitors.Remove(monitor);

                Console.WriteLine($"{monitorName} removed from {sensorName}");
            }    
            
        }   
        else
        {
            Console.WriteLine($"ERROR: {sensorName} not exits");
        }    
    }
}

class Program
{
    static void Main()
    {
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

        Process process = new Process();

        for (int i = 0; i < N; i++)
        {
            string line1 = Console.ReadLine();
            if (line1 == null) return;

            string[] data = line1.Split(' ', 5);
            if (data[0] == "CREATE")
            {
                if (data.Length != 3) return;

                string sensorName = data[1];
                string type = data[2];

                process.Create(sensorName, type);
            }    

            if (data[0] == "MONITOR")
            {
                if (data.Length != 5) return;

                string monitorName = data[1];
                string sensorName = data[2];
                string strategy = data[3];

                int[] param = data[4]
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray(); ;

                process.Monitor(monitorName, sensorName, strategy, param);
            }
            
            if (data[0] == "READ")
            {
                if (data.Length != 3) return;

                string sensorName = data[1];
                int value = int.Parse(data[2]);

                process.Read(sensorName, value);
            }

            if (data[0] == "REMOVE")
            {
                if (data.Length != 3) return;

                string monitorName = data[1];
                string sensorName = data[2];

                process.Remove(monitorName, sensorName);
            }

        }

        process.CountAlert();
    }
}