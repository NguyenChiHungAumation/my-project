using System;


class Register
{
    public event Action<string> OnTrigger;

    public void Trigger(string name, string type)
    {
        //Console.WriteLine($"Triggering '{type}' ...");
        OnTrigger?.Invoke($"{name}");

    }
}

class Trigger
{
    public void WriteTrigger(string message)
    {
        Console.WriteLine($"  -> {message} executed");
    }
}

class Program
{
    static void Main()
    {
        Register register = new Register();
        Trigger trigger = new Trigger();

        register.OnTrigger += trigger.WriteTrigger;

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
                if (data[i].Length == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                Console.WriteLine("Lỗi :không được để rỗng");
            }
        }

        Dictionary<string, string> callbacks = new Dictionary<string, string>();

        for (int i = 0; i < N; i++)
        {
            try
            {
                string[] line = data[i].Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
                switch (line[0])
                {
                    case "REGISTER":
                        {
                            if (line.Length == 3)
                            {
                                string name = line[1];
                                string type = line[2];
                                if (callbacks.ContainsKey(name))
                                {
                                    Console.WriteLine($"Callback '{name}' already exists");
                                    break;
                                }
                                else
                                {
                                    callbacks.Add(name, type);
                                    Console.WriteLine($"Registered: {name} [{type}]");
                                    break;
                                }
                            }   
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }    
                        }
                    case "TRIGGER":
                        {
                            if (line.Length == 2)
                            {
                                string type = line[1];
                                if (callbacks.ContainsValue(type))
                                {
                                    var sameType = callbacks.GroupBy(p => p.Value);
                                    foreach (var item in sameType)
                                    {
                                        if (item.Key == type)
                                        {
                                            Console.WriteLine($"Triggering '{type}' ...");
                                            foreach (var item2 in item)
                                            {
                                                string name = item2.Key;
                                                string type1 = item2.Value;
                                                register.Trigger(name, type1);

                                            }
                                            break;
                                        }
                                        else
                                            continue;
                                           
                                        
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine($"No callbacks for '{type}'");
                                    break;
                                }    
                            }   
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }    
                        }
                    case "LIST":
                        {
                            if (line.Length == 1)
                            {
                                if (callbacks.Count != 0)
                                {
                                    foreach(var item in callbacks)
                                    {
                                        Console.WriteLine($"{item.Key} [{item.Value}]");
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("No callbacks");
                                    break;
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
                Console.WriteLine($"Error: {ex.Message}");
            }
        }    
    }
}