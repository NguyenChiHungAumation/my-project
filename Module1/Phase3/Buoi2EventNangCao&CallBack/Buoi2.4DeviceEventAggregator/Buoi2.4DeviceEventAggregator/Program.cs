using System;
using System.Collections.Generic;
using System.Linq;

class Product : EventArgs
{
    public string DeviceID { get; set; }
    public string Status { get; set; }
    public double Value { get; set; }
    public string Result { get; set; }
    public double Threshold { get; set; }
    public int Index { get; set; }
}
class DeviceManager
{
    public double Threshold;
    public int Index;

    public DeviceManager(double threshold)
    {
        Threshold = threshold;
    }

    public event EventHandler<Product> ProductChanged;
    private Dictionary<string, string> keyDevice = new Dictionary<string, string>();
    private List<Product> products = new List<Product>();
    
    public void Add(string deviceID, string type)
    {
        if (keyDevice.ContainsKey(deviceID))
        {
            Console.WriteLine($"Device '{deviceID}' already exists");
        }    
        else
        {
            keyDevice.Add(deviceID, type);
            Console.WriteLine($"Device '{deviceID}' ({type}) added");
        }    
    }

    public void Remove(string deviceID)
    {
        if (keyDevice.ContainsKey(deviceID))
        {
            keyDevice.Remove(deviceID);
            Console.WriteLine($"Device '{deviceID}' removed");
        }   
        else
        {
            Console.WriteLine($"Device '{deviceID}' not found");
        }    
    }

    public void FoundEvent(string deviceID, string status, double value)
    {
        
        string result;

        if (keyDevice.ContainsKey(deviceID))
        {
            Index++;

            if (value <= Threshold)
            {
                result = "OK";

                ProductChanged?.Invoke(this, new Product
                {
                    DeviceID = deviceID,
                    Status = status,
                    Value = value,
                    Threshold = Threshold,
                    Index = Index,
                    Result = result
                });
            }
            else
            {
                result = "ALARM";

                ProductChanged?.Invoke(this, new Product
                {
                    DeviceID = deviceID,
                    Status = status,
                    Value = value,
                    Threshold = Threshold,
                    Index = Index,
                    Result = result
                });
            }

            products.Add(new Product
            {
                DeviceID = deviceID,
                Status = status,
                Value = value,
                Threshold = Threshold,
                Index = Index,
                Result = result
            });
                
        }   
        else
        {
            Console.WriteLine($"Device '{deviceID}' not found");
        }    
    }

    public void Log()
    {
        if (products.Count == 0)
        {
            Console.WriteLine($"No events");
        } 
        else
        {
            foreach (var item in products)
            {
                Console.WriteLine($"[{item.Index}] {item.DeviceID} {item.Status}: {item.Value:F2}");
            }    
        }    
    }

    public void Alarms()
    {
        var find = products.Where(p => p.Result == "ALARM");

        if (find.Count() == 0)
        {
            Console.WriteLine($"No alarms");
        }   
        else
        {
            foreach(var item in find)
            {
                Console.WriteLine($"[{item.Index}] {item.DeviceID}: {item.Value:F2} > {item.Threshold:F2}");
            }    
        }    
    }
}

class OnEvent
{
    public void OnProductChange(object sender, Product e)
    {
        if (e.Result == "OK"  )
        {
            Console.WriteLine($"[{e.DeviceID}] {e.Status}: {e.Value:F2}");
        } 
        else
        {
            Console.WriteLine($"[{e.DeviceID}] {e.Status}: {e.Value:F2}");
            Console.WriteLine($"*** ALARM: {e.DeviceID} value {e.Value:F2} exceeds {e.Threshold:F2}");
        }    
    }
}

class Program
{
    static void Main()
    {
        DeviceManager deviceManager = new DeviceManager(0);
        OnEvent onEvent = new OnEvent();
        deviceManager.ProductChanged += onEvent.OnProductChange;

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

                
                string[] line = data[i].Split(' ', 4, StringSplitOptions.RemoveEmptyEntries);
                if (line.Length == 0)
                    throw new NullReferenceException();
                
                switch (line[0])
                {
                    case "ADD_DEVICE":
                        {
                            if (line.Length == 3)
                            {
                                string deviceID = line[1];
                                string type = line[2];

                                deviceManager.Add(deviceID, type);
                                break;
                            }
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }
                        }
                    case "REMOVE_DEVICE":
                        {
                            if (line.Length == 2)
                            {
                                string deviceID = line[1];

                                deviceManager.Remove(deviceID);
                                break;

                            }
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }
                        }
                    case "EVENT":
                        {
                            if (line.Length == 4)
                            {
                                string deviceID = line[1];
                                string status = line[2];
                                double value = double.Parse(line[3]);

                                deviceManager.FoundEvent(deviceID, status, value);
                                break;
                            }
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }
                        }
                    case "SET_ALARM_THRESHOLD":
                        {
                            if (line.Length == 2)
                            {
                                double threshold = double.Parse(line[1]);
                                deviceManager.Threshold = threshold;
                                Console.WriteLine($"Alarm threshold: {threshold:F2}");
                                break;
                            }
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }
                        }
                    case "LOG":
                        {
                            if (line.Length == 1)
                            {
                                deviceManager.Log();
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
                                deviceManager.Alarms();
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
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine($" Lỗi: không được để rỗng");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine($" Lỗi: không được để rỗng");
            }

        }
    }
}