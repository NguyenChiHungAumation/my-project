using System;
using System.Text;

class Product
{
    private List<Subscription> subType = new List<Subscription>();
    private List<Log> logs = new List<Log>();
    private int counter = 0;

    //public event Action<string, string, string> OnEmit;
    public void Emit(string type, string data)
    {
        if (subType.Count != 0)
        {
            var matched = subType.Where(p => p.Type == type);
            List<string> receives = new List<string>();

            if (matched == null)
            {
                Console.WriteLine($"No subscribers");
            }
            else
            {
                Console.WriteLine($"Emitting '{type}: {data}'");
                foreach (var item in matched)
                {
                    item.Handler(item.Sub, type, data);
                    receives.Add(item.Sub);
                                        
                 
                }

                if (receives.Count == 0)
                    Console.WriteLine("No subscribers");
                logs.Add(new Log
                {
                    Index = ++counter,
                    Type = type,
                    Data = data,
                    SubInType = receives
                });
                                
            }
        }
        else
        {
            Console.WriteLine($"No subscribers");
        }    
    }

    public void AddEvent(string sub, string type, Action<string, string, string> handler)
    {
        Subscription subscription = new Subscription
        {
            Sub = sub,
            Type = type,
            Handler = handler
        };
        if (subType.Contains(subscription))
        {
            Console.WriteLine($"{sub} already subscribed to {type}");
        }
        else
        {
            subType.Add(subscription);
            Console.WriteLine($"{sub} subscribed to {type}");
        }
    }

    public void SubEvent(string sub, string type, Action<string, string, string> handler)
    {
        var item = subType.FirstOrDefault(x =>
        x.Sub == sub &&
        x.Type == type &&
        x.Handler == handler);

        if (subType.Contains(item))
        {
            subType.Remove(item);
            Console.WriteLine($"{sub} unsubscribed from {type}");
        }
        else
        {
            Console.WriteLine($"{sub} not subscribed to {type}");
        }    
    }

    public void LogEvent()
    {
        if (logs.Count == 0)
        {
            Console.WriteLine("No event");
        } 
        foreach (var item in logs)
        {
            string receiverText = item.SubInType.Count > 0
                ? string.Join(", ", item.SubInType)
                : "";

            Console.WriteLine($"[{item.Index}] {item.Type}: {item.Data} -> [{receiverText}]");
        }    
           
    }
}

class WriteEvent
{
    public void WriteData(string sub, string type, string data)
    {
        Console.WriteLine($"-> {sub} received '{type}': {data}");
    }
}

class Subscription
{
    public string Sub {  get; set; }
    public string Type { get; set; }
    public Action<string, string, string> Handler { get; set; }
}

class Log
{    
    public int Index { get; set; }
    public string Type { get; set; }
    public string Data { get; set; }
    public List<string> SubInType { get; set; } = new List<string>();
}


class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        Product product = new Product();
        WriteEvent writeEvent = new WriteEvent();

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


        for (int i = 0; i < N; i++)
        {
            try
            {
                string[] line = data[i].Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);

                switch (line[0])
                {
                    case "SUBSCRIBE":
                        {
                            if (line.Length == 3)
                            {
                                string sub = line[1];
                                string type = line[2];

                                product.AddEvent(sub, type, writeEvent.WriteData);
                                break;
                            }
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }
                        }
                    case "EMIT":
                        {
                            if (line.Length == 3)
                            {
                                string type = line[1];
                                string datas = line[2];

                                product.Emit(type, datas);
                                break;

                            }    
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }    
                        }
                    case "UNSUBSCRIBE":
                        {
                            if (line.Length == 3)
                            {
                                string sub = line[1];
                                string type = line[2];
                                product.SubEvent(sub, type, writeEvent.WriteData);
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
                                product.LogEvent();
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




