using System;

class Process
{
    public double Min {  get; set; }
    public double Max { get; set; }

    private List<string> bins = new List<string>();
    public Process(double min, double max)
    {
        Min = min;
        Max = max;
    }

    
    public void Load(string partID, double weight, Action callback)
    {
        Console.WriteLine($"[LOAD] {partID} loaded");
        callback?.Invoke();
    }

    public void Move(string partID, double weight, Action callback)
    {
        Console.WriteLine($"[MOVE] {partID} moved to station");
        callback?.Invoke();
    }

    public void Inspect(string partID, double weight, Action callback)
    {
        string result;
        if (weight >= Min && weight <= Max)
        {
            result = "PASS";
            Console.WriteLine($"[INSPECT] {partID}: {weight:F2} -> {result}");
            callback?.Invoke();
        }
        else
        {
            result = "FAIL";
            Console.WriteLine($"[INSPECT] {partID}: {weight:F2} -> {result}");
            callback?.Invoke();
        }    
    }

    public void Unload(string partID, double weight, Action callback)
    {
        string result;
        if (weight >= Min && weight <= Max)
        {
            result = "Bin A";
            Console.WriteLine($"[UNLOAD] {partID} -> {result}");
            callback?.Invoke();
        }
        else
        {
            result = "Bin B";
            Console.WriteLine($"[UNLOAD] {partID} -> {result}");
            callback?.Invoke();
        }
        bins.Add( result );
    }

    public void OnCallBack(string partID, double weight)
    {
        Load(partID, weight, () =>
        {
            Move(partID, weight, () =>
            {
                Inspect(partID, weight, () =>
                {
                    Unload(partID, weight, () =>
                    {
                        
                    });
                });
            });
        });
    }

    public void Summarry()
    {
        var binA = bins.Count(p => p == "Bin A");
        var binB = bins.Count(p => p == "Bin B");

        Console.WriteLine($"Bin A (PASS): {binA} parts");
        Console.WriteLine($"Bin B (FAIL): {binB} parts");
    }
}

class Program
{
    static void Main()
    {
        Process process = new Process(0, 0);


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
                    case "SET_RANGE":
                        {
                            if (line.Length == 3)
                            {
                                double min = double.Parse(line[1]);
                                double max = double.Parse(line[2]);
                                if (min > max)
                                {
                                    throw new FormatException($"Cần nhập lệnh SET_RANGE min>max");
                                }
                                else
                                {
                                    process.Min = min;
                                    process.Max = max;

                                    Console.WriteLine($"Inspect range: [{min:F2}, {max:F2}]");
                                    break;
                                }
                                
                            }
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }
                        }
                    case "PROCESS":
                        {
                            if (line.Length == 3)
                            {
                                if (process.Min == 0 && process.Max == 0)
                                {
                                    string partID = line[1];
                                    double weight = double.Parse(line[2]);
                                    process.OnCallBack(partID, weight);

                                    break;
                                }
                                else
                                {
                                    throw new FormatException($"Cần nhập lệnh SET_RANGE trước min>max");
                                }    
                            }

                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }
                        }
                    case "SUMMARY":
                        {
                            if (line.Length == 1)
                            {
                                process.Summarry();
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