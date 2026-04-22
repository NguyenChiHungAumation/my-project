using System;

class Product
{
    public event Action<double, string> OnRead;

    private List<(double, string)> products = new List<(double, string)>();
    public void Read(double value, double? min, double? max)
    {
        string type = null;

        if (value >= min && value <= max)
        {
            type = "OK";
            OnRead?.Invoke(value, type);
        }

        else if (value < min)
        {
            type = "LOW";
            OnRead?.Invoke(value, type);
        }

        else if (value > max)
        {
            type = "HIGH";
            OnRead?.Invoke(value, type);
        }

        products.Add((value, type));

    }

    public void CountAlarm()
    {
         
        var lowAlarm = products
            .Where(p => p.Item2 == "LOW")
            .Count();
        var highAlarm = products
            .Where(p => p.Item2 == "HIGH")
            .Count();

        int countAlarm = lowAlarm + highAlarm;
        Console.WriteLine($"Alarms: {countAlarm}");
    }

    public void History()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("No readings");
        } 
        else
        {
            foreach (var item in products)
            {
                Console.WriteLine($"{item.Item1:F2} [{item.Item2}]");
            }    
        }    
    }
}

class Display
{
    public void WriteRead(double value, string message)
    {
        if (message == "OK")
        {
            Console.WriteLine($"{value:F2}: {message}");
        }
        else
        {
            Console.WriteLine($"{value:F2}: {message} ALARM!");
        }    
    }
}

class Program
{
    static void Main()
    {
        Product product = new Product();
        Display display = new Display();
        product.OnRead += display.WriteRead;
        //product.Read(12.5, 10, 30);

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

        double? min = null;
        double? max = null;

        for (int i = 0; i < N; i++)
        {
            try
            {
                string[] line = data[i].Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);

                switch (line[0])
                {
                    case "SET_THRESHOLD":
                        {
                            if (line.Length == 3)
                            {
                                if (min == null && max == null)
                                {
                                    min = double.Parse(line[1]);
                                    max = double.Parse(line[2]);
                                    Console.WriteLine($"Threshold set: [{min:F2}, {max:F2}]");
                                    break;
                                }
                                else
                                {
                                    throw new FormatException($"SET_THRESHOLD đã được gán");
                                }    
                            }
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }
                        }
                    case "READ":
                        {
                            if (min == null || max == null)
                                throw new FormatException("Nhập lệnh SET_THRESHOLD trước");

                            if (line.Length == 2)
                            {
                                double value =  double.Parse(line[1]);

                                product.Read(value, min, max);
                                break;
                            }
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }
                        }
                    case "HISTORY":
                        {
                            if (line.Length == 1)
                            {
                                product.History();
                                break;
                            }
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }
                        }
                    case "ALARM_COUNT":
                        {
                            if (line.Length == 1)
                            {

                                product.CountAlarm();
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
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Over format");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Không xác định");
            }

        }
    }
}



