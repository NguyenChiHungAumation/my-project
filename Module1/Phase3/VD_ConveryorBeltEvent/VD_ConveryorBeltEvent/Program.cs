using System;
using System.Text;

class Conveyor
{
    public event Action<string> OnConnect;
    public event Action<string> OnStart;
    public event Action<string> OnData;
    public event Action<string> OnStop;

    public void Connect()
    {
        Console.WriteLine("Conveyor: Connecting...");
        OnConnect?.Invoke("Conveyor connected");
    }

    public void Start()
    {
        Console.WriteLine("Conveyor: Starting...");
        OnStart?.Invoke("Conveyor started");
    }

    public void SendData(string data)
    {
        Console.WriteLine("Conveyor: Sending data...");
        OnData?.Invoke(data);
    }

    public void Stop()
    {
        Console.WriteLine("Conveyor: Stopping");
        OnStop?.Invoke("Conveyor stopped");
    }
}

class Logger
{
    public void WriteLog(string message)
    {
        Console.WriteLine($"[Logger] {message}");
    }
}

class Display
{
    public void ShowStatus(string message)
    {
        Console.WriteLine($"[Display] {message}");
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        Conveyor conveyor = new Conveyor();
        Logger logger = new Logger();
        Display display = new Display();

        conveyor.OnConnect += logger.WriteLog;
        conveyor.OnConnect += display.ShowStatus;

        conveyor.OnStart += logger.WriteLog;
        conveyor.OnStart += display.ShowStatus;

        conveyor.OnData += logger.WriteLog;
        conveyor.OnData += display.ShowStatus;

        conveyor.OnStop += logger.WriteLog;
        conveyor.OnStop += display.ShowStatus;

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

        for (int i = 0;i < N; i++)
        {
            try
            {
                string[] line = data[i].Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

                switch (line[0])
                {
                    case "CONNECT":
                        {
                            if (line.Length == 1)
                            {
                                conveyor.Connect();
                                break;
                            }   
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }    
                        }
                    case "START":
                        {
                            if (line.Length == 1)
                            {
                                conveyor.Start();
                                break;
                            }  
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }    
                        }
                    case "DATA":
                        {
                            if (line.Length == 2)
                            {
                                conveyor.SendData(line[1]);
                                break;
                            }
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }    
                        }
                    case "STOP":
                        {
                            if (line.Length == 1)
                            {
                                conveyor.Stop();
                                break;
                            }   
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }    
                        }
                    case "EXIT":
                        {
                            if (line.Length == 1)
                            {
                                return;
                            }    
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }    
                        }
                    case "HELP":
                        {
                            if (line.Length == 1)
                            {
                                Console.WriteLine("Các lệnh hỗ trợ:");
                                Console.WriteLine("CONNECT: Kết nối băng tải");
                                Console.WriteLine("START: Bắt đầu băng tai chạy");
                                Console.WriteLine("DATA <noi_dung>: Gửi dữ liệu vào băng tải");
                                Console.WriteLine("STOP: Dừng băng tải");
                                Console.WriteLine("HELP: Hiển thị danh sách cách lệnh");
                                Console.WriteLine("EXIT: Thoát chương trình");
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
        }    


                    
                


    }
}