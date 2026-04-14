using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}

class LogEntry
{
    public DateTime Time { get; set; }
    public string Level { get; set; }
    public string Message { get; set; }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        List<Product> products = new List<Product>
        {
            new Product {Code = "P01", Name = "Motor", Price = 100, Quantity = 5},
            new Product { Code = "P02", Name = "Sensor", Price = 50, Quantity = 10 },
            new Product { Code = "P03", Name = "Camera", Price = 300, Quantity = 2 },
            new Product { Code = "P04", Name = "Cylinder", Price = 120, Quantity = 7 },
            new Product { Code = "P05", Name = "Conveyor", Price = 500, Quantity = 1 }

        };

        List<LogEntry> logEntries = new List<LogEntry>
        {
            new LogEntry
            {
                Time = new DateTime(2026,1,1,8,0,0),
                Level = "INFO",
                Message = "System started"
            },
            new LogEntry
            {
                Time = new DateTime(2026, 1, 1, 8, 5, 0),
                Level = "ERROR",
                Message = "Motor overload"
            },
            new LogEntry
            {
                Time = new DateTime(2026, 1, 1, 8, 10, 0),
                Level = "WARNING",
                Message = "Low pressure"
            },
            new LogEntry
            {
                Time = new DateTime(2026, 1, 1, 8, 15, 0),
                Level = "INFO",
                Message = "Recipe loaded"
            }
        };
        Console.WriteLine("--Danh sách Product--");
        foreach (var item in products)
        {
            Console.WriteLine($"{item.Code}-{item.Name}-{item.Price}-{item.Quantity}");
        }
        Console.WriteLine("--Danh sách Log--");
        foreach (var item in logEntries)
        {
            Console.WriteLine($"{item.Time}-{item.Level}-{item.Message}");
        }    

        // Sản phẩm có giá lớn hơn 100
        Console.WriteLine("--Ví dụ 1: Lọc sản phẩm giá cao--");

        var result1 = products.Where(p => p.Price > 100);
        Console.WriteLine("Sản phẩm có giá lớn hơn 100: ");

        foreach (var item in result1)
        {
            Console.WriteLine($"{item.Name} - {item.Price}");
        }
        // Sản phẩm có số lượng lớn hơn 3
        var result11 = products.Where(p => p.Quantity > 3);
        Console.WriteLine("Sản phẩm có số lượng lơn hơn 3: ");

        foreach (var item in result11)
        {
            Console.WriteLine($"{item.Name} - {item.Quantity}");
        }

        // Sản phẩm có chứa chữ o 
        Console.WriteLine("Sản phẩm có chứa chữ o: ");
        var result12 = products.Where(p => p.Name.Contains("o"));
        foreach (var item in result12)
        {
            Console.WriteLine($"{item.Name}");
        }

        // Sản phẩm có giá từ 100 đến 300 
        Console.WriteLine("Sản phẩm có giá từ 100 đến 300: ");
        var result13 = products.Where(p => p.Price >= 100 && p.Price <= 300);
        foreach (var item in result13)
        {
            Console.WriteLine($"{item.Name} - {item.Price}");
        }

        Console.WriteLine("--Ví Dụ 2: Chỉ lấy tên sản phẩm--");

        Console.WriteLine("Lấy tên sản phẩm:");
        var names2 = products.Select(p => p.Name);

        foreach (var name in names2)
        {
            Console.WriteLine(name);
        }

        Console.WriteLine("Lấy tên và giá sản phẩm:");
        var names21 = products.Select(p => $"{p.Name} - {p.Price}");

        foreach (var name in names21)
        {
            Console.WriteLine(name);
        }

        Console.WriteLine("--Ví Dụ 3: Sắp xếp theo tồn kho giảm dần--");
        var sorted = products.OrderByDescending(p => p.Quantity);

        foreach (var item in sorted)
        {
            Console.WriteLine($"{item.Name} - {item.Quantity}");
        }

        Console.WriteLine("Sắp xếp theo giá tăng dần: ");
        var sorted31 = products.OrderBy(p => p.Price);

        foreach (var item in sorted31)
        {
            Console.WriteLine($"{item.Name} - {item.Price}");
        }

        Console.WriteLine("Sắp xếp theo tên A-Z: ");
        var sorted32 = products.OrderBy(p => p.Name);

        foreach (var item in sorted32)
        {
            Console.WriteLine($"{item.Name}");
        }

        Console.WriteLine("--Ví Dụ 4: Kiểm tra có Log ERROR không--");

        bool hasError = logEntries.Any(p => p.Level == "ERROR");
        Console.WriteLine($"Có Log ERROR: {hasError}");
    }
}