using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

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
class OrderInspection
{
    public static List<Order> orders = new List<Order>();
    public static List<Inspection> inspections = new List<Inspection>();

    public static void AddOrder(Order order)
    {
        var existingOrder = orders.FirstOrDefault(x => x.Id == order.Id);
        if (existingOrder == null)
        {
            orders.Add(order);
            Console.WriteLine($"Added Order: {order.Id} - {order.Name}");
        }  
        else
        {
            existingOrder.Name = order.Name;
            Console.WriteLine($"Update Name Order: {existingOrder.Id} - {existingOrder.Name}");

        }    
        
    }

    public static void AddInspection(Inspection inspection)
    {
        inspections.Add(inspection);
        Console.WriteLine($"Added Inspection: {inspection.OrderId} - {inspection.Score} - {inspection.Passed}");

    }

    public static void MergeList()
    {
        if (orders.Count == 0 || inspections.Count == 0)
        {
            if (orders.Count == 0)
            {
                Console.WriteLine($"Danh sách Order rỗng");
            }
            else
            {
                Console.WriteLine($"Danh sách Inspection rỗng");
            }    
        }
        else
        {
            var merge = inspections.Join(
                orders,
                o => o.OrderId,
                i => i.Id,
                (o, i) => new
                {
                    Orderld = i.Id,
                    Product = i.Name,
                    Score = o.Score,
                    Passed = o.Passed,
                });

            Console.WriteLine("== Gộp 2 danh sách theo Id ==");
            foreach (var item in merge)
            {
                Console.WriteLine($"{item.Orderld} - {item.Product} - {item.Score} - {item.Passed}");
            }    

        }    
        
    }

    public static void MergeListRule()
    {
        if (orders.Count == 0 || inspections.Count == 0)
        {
            if (orders.Count == 0)
            {
                Console.WriteLine($"Danh sách Order rỗng");
            }
            else
            {
                Console.WriteLine($"Danh sách Inspection rỗng");
            }
        }
        else
        {
            var merge = orders.GroupJoin(
                inspections,
                o => o.Id,
                i => i.OrderId,
                (o, ins) => new
                {
                    Orderld = o.Id,
                    Product = o.Name,
                    Count = ins.Count(),
                    PassCount = ins.Count(x => x.Passed),
                    FailCount = ins.Count(x => !x.Passed),
                    Averager = ins.Select(x => x.Score).DefaultIfEmpty(0).Average()
                });

            foreach (var item in merge)
            {
                Console.WriteLine($"{item.Orderld} - {item.Product}");
                Console.WriteLine($"Count={item.Count} - PassdCount={item.PassCount} - FailCount={item.FailCount} - Average={item.Averager}");
            }    
        }    
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        if (!int.TryParse(Console.ReadLine(), out int N)) return;

        string[][] arr = new string[N][];
        for (int i = 0; i < N; i++)
        {
            string s = Console.ReadLine();
            arr[i] = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        }

        for (int i = 0; i < N; i++)
        {
            switch (arr[i][0].ToUpper())
            {
                case "ORDER":
                    {
                        if (arr[i].Length == 3)
                        {
                            Order order = new Order();
                            if (!int.TryParse(arr[i][1], out int id)) return;
                            order.Id = id;
                            order.Name = arr[i][2];
                            OrderInspection.AddOrder(order);
                            break;
                        }   
                        else
                        {
                            Console.WriteLine($"Câu lệnh sai cú pháp");
                            break;
                        }    
                    }
                case "INSPECTION":
                    {
                        if (arr[i].Length == 4)
                        {
                            Inspection inspection = new Inspection();
                            if (!int.TryParse(arr[i][1], out int orderld)) return;
                            inspection.OrderId = orderld;
                            if (!double.TryParse(arr[i][2], out double score)) return;
                            inspection.Score = score;
                            if (!bool.TryParse(arr[i][3], out bool passed)) return;
                            inspection.Passed = passed;
                            OrderInspection.AddInspection(inspection);
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Câu lệnh sai cú pháp");
                            break;
                        }
                    }
                default:
                    {
                        Console.WriteLine($"Câu lệnh sai cú pháp");
                        break;
                    }
            }
        }

        OrderInspection.MergeList();
        OrderInspection.MergeListRule();
    }
}
