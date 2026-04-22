using System;
using System.Diagnostics;

class ReadSensor
{
    public string SensorID { get; set; }
    public string Type { get; set; }
    public double Value { get; set; }
    public string Result { get; set; }

}

class ReadThreshold
{
    public string Type { get; set; }
    public double MaxValue { get; set; }
}

class Sensor
{
    public string Type { get; private set; }
    public double MaxValue { get; private set; }
    //public string Result { get; private set; }
    public List<ReadSensor> readSensors = new List<ReadSensor>();

    public List<ReadSensor> statusList = new List<ReadSensor>();
    public event EventHandler<ReadSensor> OnSensorChanged;

    public Sensor(string type, double maxValue)
    {
        Type = type;
        MaxValue = maxValue;
    }

    public void Read(string sensorID, string type, double value, double maxValue, bool hasThreshold)
    {
        string result;
        
        if (hasThreshold == true)
        {
            if (value <= maxValue)
            {
                OnSensorChanged?.Invoke(this, new ReadSensor
                {
                    Type = type,
                    Value = value,
                    SensorID = sensorID,
                    Result = "OK"
                });

                result = "OK";

                readSensors.Add(new ReadSensor
                {
                    SensorID = sensorID,
                    Type = type,
                    Value = value,
                    Result = result
                });
            }

            else
            {
                OnSensorChanged?.Invoke(this, new ReadSensor
                {
                    Type = type,
                    Value = value,
                    SensorID = sensorID,
                    Result = "ALARM"
                });

                result = "ALARM";

                readSensors.Add(new ReadSensor
                {
                    SensorID = sensorID,
                    Type = type,
                    Value = value,
                    Result = result
                });
            }
        }
        else
        {
            Console.WriteLine($"[{sensorID}] {type}: {value:F2} -> NO THRESHOLD");
        }    

        var existing = statusList.FirstOrDefault(p => p.SensorID == sensorID);
        if (existing != null)
        {
            existing.Value = value;
        }    
        else
        {
            statusList.Add( new ReadSensor
            {
                SensorID = sensorID,
                Value = value
            });
        }  
               
                          
               
    }

    public void Alarm()
    {
        var FindAlarm = readSensors.Any(p => p.Result == "ALARM");
        if (FindAlarm == false)
        {
            Console.WriteLine("No alarms");
        }
        else
        {
            var FilterTempAlarm = readSensors
                .Where(p => p.Type == "TEMP")
                .Where(o => o.Result == "ALARM");

            var FilterPressAlarm = readSensors
                .Where(p => p.Type == "PRESS")
                .Where(o => o.Result == "ALARM");

            var FilterVibrAlarm = readSensors
                .Where(p => p.Type == "VIBR")
                .Where(o => o.Result == "ALARM");

            if (FilterTempAlarm.Count() != 0)
            {
                Console.WriteLine($"TEMP: {FilterTempAlarm.Count()} alarms");
            }

            if (FilterPressAlarm.Count() != 0)
            {
                Console.WriteLine($"PRESS: {FilterPressAlarm.Count()} alarms");
            }

            if (FilterVibrAlarm.Count() != 0)
            {
                Console.WriteLine($"VIBR: {FilterVibrAlarm.Count()} alarms");
            }
        }
    }

    public void Status()
    {
        if (statusList.Count() == 0)
        {
            Console.WriteLine("No sensor");
        }
        else
        {
            foreach (var sensor in statusList)
            {
                {
                    Console.WriteLine($"{sensor.SensorID}: {sensor.Value:F2}");
                }
            }
        }

    }
}

class Logger
{
    public void OnSensor(object sender, ReadSensor e)
    {
        if (e.Result == "OK")
        {
            Console.WriteLine($"[{e.SensorID}] {e.Type}: {e.Value:F2} -> {e.Result}");
        }
        else
            Console.WriteLine($"[{e.SensorID}] {e.Type}: {e.Value:F2} -> {e.Result}!");
    }
}

class Program
{
    static void Main()
    {
        Sensor sensor = new Sensor("", 0);
        Logger logger = new Logger();
        sensor.OnSensorChanged += logger.OnSensor;

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
        for (int i = 0; i < N; i++)
        {
            try
            {
                data[i] = Console.ReadLine();
                if (data[i].Length == 0)
                    throw new Exception();
            }
            catch (Exception)
            {
                Console.WriteLine("Lỗi :không được để rỗng");
            }
        }

        List<ReadThreshold> thresholds = new List<ReadThreshold> ();
        for (int i = 0; i < N; i++)
        {
            try
            {
                string[] line = data[i].Split(' ', 4, StringSplitOptions.RemoveEmptyEntries);

                switch (line[0])
                {
                    case "THRESHOLD":
                        {
                            if (line.Length == 3)
                            {
                                string type = line[1];
                                double maxValue = double.Parse(line[2]);

                                if (type == "TEMP" ||
                                    type == "PRESS" ||
                                    type == "VIBR")
                                {
                                    thresholds.Add(new ReadThreshold
                                    {
                                        Type = type,
                                        MaxValue = maxValue
                                    });

                                    Console.WriteLine($"{type} max threshold: {maxValue:F2}");
                                                                    
                                    break;
                                }
                                else
                                {
                                    throw new FormatException($"{data[i]} sai định dạng");
                                }    
                            }
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }
                        }
                    case "READ":
                        {
                            if (line.Length == 4)
                            {
                                string sensorID = line[1];
                                string type = line[2];
                                double value = double.Parse(line[3]);
                                bool findType = false;

                                if (thresholds != null)
                                {
                                    double maxValue = 0;
                                    foreach (var item in thresholds)
                                    {
                                        if (item.Type == type)
                                        {
                                            findType = true;
                                            maxValue = item.MaxValue;
                                            break;
                                        }    
                                    }

                                    sensor.Read(sensorID, type, value, maxValue, findType);
                                    break;                                   
                                     
                                }
                                else
                                {
                                    throw new FormatException($"Nhập lệnh SET_WEIGHT trước lệnh DETECT");
                                }

                                break;
                            }

                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }
                        }
                    case "ALARMS":
                        {
                            if (line.Length == 1)
                            {
                                if (thresholds != null)
                                {
                                    sensor.Alarm();
                                    break;
                                }

                                else
                                {
                                    throw new FormatException($"Nhập lệnh SET_WEIGHT trước lệnh DETECT");
                                }
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
                                if (thresholds != null)
                                {
                                        sensor.Status();
                                    break;
                                }
                                else
                                {
                                    throw new FormatException($"Nhập lệnh SET_WEIGHT trước lệnh DETECT");
                                }

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
