using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class LogEntry
{
    public DateTime Time { get; set; }
    public string Level { get; set; }
    public string Message { get; set; }
}

class Order
{
    public int Id { get; set; }
    public string Name { get; set; }
}
class Inspection
{
    public int OrderId { get; set; }
    public double Score { get; set; }
    public bool Passed { get; set; }

}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
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
            },

            new LogEntry
            {
                Time = new DateTime(2026, 1, 1, 8, 20, 0),
                Level = "ERROR",
                Message = "Camera disconnected"
            }

        };

        List<Order> orders = new List<Order>
        {
            new Order { Id = 1, Name = "Cap" },
            new Order { Id = 2, Name = "Bottle" },
            new Order { Id = 3, Name = "Label" }
        };

        List<Inspection> inspections = new List<Inspection>
        {
            new Inspection { OrderId = 1, Score = 95, Passed = true },
            new Inspection { OrderId = 1, Score = 80, Passed = true },
            new Inspection { OrderId = 2, Score = 45, Passed = false },
            new Inspection { OrderId = 2, Score = 70, Passed = true },
            new Inspection { OrderId = 3, Score = 88, Passed = true }
        };

        Console.WriteLine("== Group Log theo Level ==");

        var groups = logEntries.GroupBy(p => p.Level);

        foreach (var group in groups)
        {
            Console.WriteLine($"Level: {group.Key} - Count={group.Count()}");
            foreach (var group2 in group)
            {
                Console.WriteLine($"{group2.Time} - {group2.Message}");
            }    
        }

        Console.WriteLine("== Group Log theo Time ==");

        var result = logEntries.GroupBy(p => p.Time);

        foreach (var item in result)
        {

            Console.WriteLine($"{item.Key} - Count={item.Count()}");
            foreach (var item1 in item)
            {
                Console.WriteLine($"{item1.Level} - {item1.Message}");
            }    
        }

        Console.WriteLine("== Gộp 2 danh sách ==");


        var joined = orders.Join(
            inspections,
            o => o.Id,
            p => p.OrderId,
            (o, p) => new
            {

                OrderID = o.Id,
                Product = o.Name,
                Score = p.Score,
                Passed = p.Passed,

            });

        foreach (var item in joined)
        {
            Console.WriteLine($"{item.OrderID} - {item.Product} - {item.Score} - {item.Passed}");

        }

        Console.WriteLine("== Nâng cao thống kê theo Order ==");

        var summaty = orders.GroupJoin(
            inspections,
            p => p.Id,
            i => i.OrderId,
            (p, ins) => new
            {
                Orderld = p.Id,
                Product = p.Name,
                InspectionCount = ins.Count(),
                PassCount = ins.Count(x => x.Passed),
                FailCount = ins.Count(x => !x.Passed),
                AverageScore = ins.Select(x => x.Score).DefaultIfEmpty(0).Average(),


            });

        foreach (var item in summaty)
        {
            Console.WriteLine($"Order: {item.Orderld} - {item.Product}");
            Console.WriteLine($"Inspection Count: {item.InspectionCount}");
            Console.WriteLine($"Pass Count: {item.PassCount}");
            Console.WriteLine($"Fail Count: {item.FailCount}");
            Console.WriteLine($"Average Score: {item.AverageScore:F2}");
        }

        Console.WriteLine("== Nâng cao thống kê theo Passed: false ==");

        var failed = orders.Join(
            inspections.Where(x => !x.Passed),
            i => i.Id,
            p => p.OrderId,
            (i, p) => new
            {
                Orderld = i.Id,
                Product = i.Name,
                Score = p.Score,
                Passed =p.Passed
            });


        foreach (var item in failed)
        {
            Console.WriteLine($"Order: {item.Orderld} - {item.Product}");
            Console.WriteLine($"Inspection : {item.Score} - {item.Passed}");
        }


        Console.WriteLine("== Lọc theo có điểm trung bình cao nhất ==");

        var averageMax = orders.GroupJoin(
            inspections,
            o => o.Id,
            i => i.OrderId,
            (o, ins) => new
            {
                Orderld = o.Id,
                Product = o.Name,
                AverageScore = ins.Select(x => x.Score).DefaultIfEmpty(0).Average()
            })
            .OrderByDescending(x => x.AverageScore)
            .FirstOrDefault();

        Console.WriteLine($"OrderId: {averageMax.Orderld}");
        Console.WriteLine($"Name: {averageMax.Product}");
        Console.WriteLine($"Average Max: {averageMax.AverageScore:F2}");

        Console.WriteLine("== Ví dụ 3 Parse command ==");

        string command = "MOVE G01 X100.0 Y200.0 Z50.0";
        string[] parts = command.Split(' ');

        string action = parts[0];
        string mode = parts[1];
        double x = double.Parse(parts[2].Substring(2));
        double y = double.Parse(parts[3].Substring(2));
        double z = double.Parse(parts[4].Substring(2));

        Console.WriteLine($"Action: {action}");
        Console.WriteLine($"Mode: {mode}");
        Console.WriteLine($"X: {x}");
        Console.WriteLine($"Y: {y}");
        Console.WriteLine($"Z: {z}");

    }
}