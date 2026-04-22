using System;
using System.Text;

class ControlPanel
{
    public event Action<string> ButtonPressed;
    public event Action<string> EmergencyStop;
    public event Action<string> ResetRequested;

    public void PressBuntton()
    {
        ButtonPressed?.Invoke("Button pressed");
    }
    public void TriggerEmergencyStop()
    {
        EmergencyStop?.Invoke("Emergency strop activated");
    }
    public void RequestReset()
    {
        ResetRequested?.Invoke("Reset requested");
    }
}

class Logger
{
    public void WriteLogger(string message)
    {
        Console.WriteLine($"[Logger] {message}");
    }
}

class UINotifier
{
    public void WriteUINotifier(string message)
    {
        Console.WriteLine($"[UI] {message}");
    }
}

class Help
{
    public static void ShowHelp()
    {
        Console.WriteLine("Các lệnh hỗ trợ:");
        Console.WriteLine("PRESS: Nhấn nút điều kiển");
        Console.WriteLine("EMERGENCY: Kích hoạt dưng khẩn cấp");
        Console.WriteLine("RESET: Gửi yêu cầu reset hệ thống");
        Console.WriteLine("HELP: Hiển thị danh sách lệnh");
        Console.WriteLine("EXIT: Thoát chương trình");
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        ControlPanel controlPanel = new ControlPanel();
        Logger logger = new Logger();
        UINotifier uINotifier = new UINotifier();

        controlPanel.ButtonPressed += logger.WriteLogger;
        controlPanel.ButtonPressed += uINotifier.WriteUINotifier;

        controlPanel.EmergencyStop += logger.WriteLogger;
        controlPanel.EmergencyStop += uINotifier.WriteUINotifier;

        controlPanel.ResetRequested += logger.WriteLogger;
        controlPanel.ResetRequested += uINotifier.WriteUINotifier;

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
                string[] line = data[i].Split(' ', 1, StringSplitOptions.RemoveEmptyEntries);
                switch (line[0])
                {
                    case "PRESS":
                        {
                            if (line.Length == 1)
                            {
                                controlPanel.PressBuntton();
                                break;
                            }   
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }    
                        }
                    case "EMERGENCY":
                        {
                            if (line.Length == 1)
                            {
                                controlPanel.TriggerEmergencyStop();
                                break;
                            }   
                            else
                            {
                                throw new FormatException($"{data[i]} sai định dạng");
                            }    
                        }
                    case "RESET":
                        {
                            if (line.Length == 1)
                            {
                                controlPanel.RequestReset();
                                break;
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
                                Help.ShowHelp();
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