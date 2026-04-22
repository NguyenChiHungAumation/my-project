using System;
using System.Diagnostics.Contracts;

class Product
{
    public int AttemptCount {  get; set; }
    
    public string Connect { get; set; }
}

class Connection
{
    public static Dictionary<string, Product> products = new Dictionary<string, Product>();
    
    public static void Connect(string name, int maxRetry, int failCount)
    {

        int countFail = 0;
        int countSuccess = 0;
        bool connect = false;

        Console.WriteLine($"Connecting to {name}...");
        for (int i = 1; i < maxRetry + 1; i++)
        {
            if (i <= failCount)
            {
                countFail = i;
                connect = false;
                Console.WriteLine($"Attempt {i}/{maxRetry}: Failed - Connection refused");
            }
            else if (i == failCount + 1)
            {
                countSuccess = i;
                connect = true;
                Console.WriteLine($"Attempt {i}/{maxRetry}: Success");
                break;
            }       
        }

        try
        {

            if (connect == true)
            {
                Product product = new Product
                {
                    Connect = "Connected",
                    AttemptCount = countSuccess
                };
                if (!products.ContainsKey(name))
                {
                    products.Add(name, product);
                    Console.WriteLine($"Connected to {name} after {countSuccess} attempts");
                }
                else
                {
                    products.TryGetValue(name, out var dulicateProduct);

                    dulicateProduct.Connect = "Connected";
                    dulicateProduct.AttemptCount = countSuccess;
                    Console.WriteLine($"Connected to {name} after {countSuccess} attempts");
                }
            }
            else
            {
                Product product = new Product
                {
                    Connect = "Failed",
                    AttemptCount = countFail
                };
                if (!products.ContainsKey(name))
                {
                    products.Add(name, product);
                    Console.WriteLine($"Failed to connect to {name} after {countFail} attempts");
                }
                else
                {
                    products.TryGetValue(name, out var dulicateProduct);

                    dulicateProduct.Connect = "Failed";
                    dulicateProduct.AttemptCount = countFail;
                    Console.WriteLine($"Failed to connect to {name} after {countFail} attempts");
                }    
                    
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static void Status()
    {
        if (products.Count != 0)
        {
            foreach (var item in products)
            {
                Console.WriteLine($"{item.Key}: {item.Value.Connect} (attempts: {item.Value.AttemptCount})");
            }    
        } 
        else
        {
            Console.WriteLine("No devices");
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
            N = int.Parse(Console.ReadLine());
            if (N < 0)
                throw new Exception();
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Invalid number format");
            return;
        }
        catch (OverflowException)
        {
            Console.WriteLine("Error: Overflow");
            return;
        }
        catch (Exception)
        {
            Console.WriteLine("Error: Invalid number format");
            return;
        }

        string[] line = new string[N];
        try
        {
            for (int i = 0; i < N; i++)
            {
                
                line[i] = Console.ReadLine();
                if (line[i].Length == 0)
                    throw new FormatException();
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Invalid format");
            return;
        }


        for (int i = 0; i < N; i++)
        {
            string[] data = line[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            try
            {
                switch(data[0])
                {
                    case "CONNECT":
                        {
                            if (data.Length == 4)
                            {
                                string name = data[1];
                                int maxRetry = int.Parse(data[2]);
                                int failCount = int.Parse(data[3]);
                                if (maxRetry < 1) throw new InvalidOperationException("Number connect invalid format");
                                if (failCount < 0) throw new InvalidOperationException("Number connect invalid format");

                                Connection.Connect(name, maxRetry, failCount);
                                break;
                            }   
                            else
                            {
                                throw new FormatException();
                                break;
                            }    
                        }
                    case "STATUS":
                        {
                            if (data.Length == 1)
                            {
                                Connection.Status();
                                break;
                            }   
                            else
                            {
                                throw new FormatException();
                                break;
                            }    
                        }
                    default:
                        {
                            throw new FormatException();
                        }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Invalid format");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Error: Overflow");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }    

    }
}
