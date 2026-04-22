using System;
/// <summary>
/// Lớp dữ liệu cho event đọc dữ liệu
/// </summary>
class ConveyorReading : EventArgs
{
    public string PartID { get; set; }
    public double Weight { get; set; }
    public string Value { get; set; }

}

/// <summary>
/// Lớp sensor mỗi sensor có 2 event ghi dữ liệu và alarm
/// </summary>
class Conveyor
{
    public List<ConveyorReading> conveyorReadings = new List<ConveyorReading>();
    public event EventHandler<ConveyorReading> ReadingTaken;

    public double Min { get; private set; } = 0;
    public double Max { get; private set; } = 0;
    public string Value { get; private set; }
  
    public Conveyor(double min, double max)
    {
        Min = min;
        Max = max;
    }

    public void Read(string partID, double weight)
    {
        if (weight >= Min && weight <= Max)
        {
            ReadingTaken?.Invoke(this, new ConveyorReading
            {
                PartID = partID,
                Weight = weight,
                Value = "PASS"
            });

            Value = "PASS";
        }
        else
        {
            ReadingTaken?.Invoke(this, new ConveyorReading
            {
                PartID = partID,
                Weight = weight,
                Value = "FAIL"
            });

            Value = "FAIL";
        }

        conveyorReadings.Add(new ConveyorReading
        {
            PartID = partID,
            Weight = weight,
            Value = Value
        });
    }
    /// <summary>
    /// hiển thị danh sách event
    /// </summary>
    public void History()
    {
        if (conveyorReadings.Count != 0)
        {
            foreach (var item in conveyorReadings)
            {
                Console.WriteLine($"{item.PartID} {item.Weight:F2} [{item.Value}]");
            }
        }
        else
        {
            Console.WriteLine("No parts detected");
        }
    }
    /// <summary>
    /// đếm số lượng Pass và Fail
    /// </summary>
    public void Count()
    {
        var resultPass = conveyorReadings.Where(p => p.Value == "PASS");
        var resultFail = conveyorReadings.Where(p => p.Value == "FAIL");

        Console.WriteLine($"Total: {resultPass.Count() + resultFail.Count()}, Pass: {resultPass.Count()}, Fail: {resultFail.Count()}");
            
    }
            
}

class Logger
{
    public void OnReadingTaken(object sender, ConveyorReading e)
    {
        Console.WriteLine($"{e.PartID}: {e.Weight:F2} -> {e.Value}");
    }
            
}


class Program
{
    static void Main()
    {
        Conveyor conveyor = null;
        
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
                    case "SET_WEIGHT":
                        {
                            if (line.Length == 3)
                            {
                                double min = double.Parse(line[1]);
                                double max = double.Parse(line[2]);

                                conveyor = new Conveyor(min, max);
                                Logger logger = new Logger();

                                conveyor.ReadingTaken += logger.OnReadingTaken;
                                Console.WriteLine($"Weight range: [{min:F2}, {max:F2}]");

                                break;
                            }
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }
                        }
                    case "DETECT":
                        {
                            if (line.Length == 3)
                            {
                                string partID = line[1];
                                double weight = double.Parse(line[2]);

                                if (conveyor != null)
                                {
                                    conveyor.Read(partID, weight);
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
                    case "COUNT":
                        {
                            if (line.Length == 1)
                            {
                                if (conveyor != null)
                                {
                                    conveyor.Count();
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
                    case "HISTORY":
                        {
                            if (line.Length == 1)
                            {
                                if (conveyor != null)
                                {
                                    conveyor.History();
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