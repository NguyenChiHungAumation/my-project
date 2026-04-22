using System;
using System.Text;
using System.Collections.Generic;

class ConfigReader
{
    public static void AddConfigLine(Dictionary<string, string> configs, string line)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            throw new ArgumentNullException("Dòng cấu hình rỗng");
        }

        string[] part = line.Split('=', 2);

        if (part.Length != 2)
        {
            throw new FormatException("Dòng phải có dạng key=value");
        }    

        string key = part[0];
        string value = part[1];

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new FormatException("Key không được để trống");
        }    
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new FormatException("Value không được để trống");
        }    

        if (configs.ContainsKey(key))
        {
            throw new InvalidOperationException($"Key: {key} đã tồn tại");
        }    

        configs.Add(key, value);
            
    }
}
class Program
{

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        Dictionary<string, string> configs = new Dictionary<string, string>();
        
        try
        {
            int N = int.Parse(Console.ReadLine());

            if (N < 0)
            {
                throw new FormatException("N phải lớn hơn 0");
            }
            string[][] arr = new string[N][];

            for (int i = 0; i < N; i++)
            {
                string line = Console.ReadLine();
                arr[i] = line.Split(' ', 1);
                if (arr[i].Length != 1)
                {
                    throw new FormatException("Dòng phải có dạng key=value");
                }    
                
            }

            for (int i = 0; i < N; i++)
            {
                string line = arr[i][0];
                ConfigReader.AddConfigLine(configs, line);
                Console.WriteLine("Đã lưu thành công");
            }    


        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Lỗi: {ex.Message}");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Lỗi: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Lỗi: {ex.Message}");
        }
        catch (OverflowException)
        {
            Console.WriteLine($"Lỗi: bị vượt phạm vi int");
        }
    }
}