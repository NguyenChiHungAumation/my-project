using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Order
{
    public string OrderId { get; set; }
    public string Product {  get; set; }
    public int Quantity { get; set; }
}

class Inspection
{
    public string InspID { get; set; }
    public string OrderID { get; set; }
    public string Result { get; set; }
    public double Score { get; set; }
}

class SubProgram
{
    public static void Merge(List<Order> orders, List<Inspection> inspections)
    {
        var result = orders.GroupJoin(
            inspections,
            o => o.OrderId,
            i => i.OrderID,
            (o, ins) => new
            {
                OrderId = o.OrderId,
                Product = o.Product,
                Quantity = o.Quantity,
                CountInspection = ins.Count(),
                CountPass = ins.Count(p => p.Result == "PASS"),
                CountFail = ins.Count(p => p.Result == "FAIL"),
                AverageScore = ins.Select(p => p.Score).DefaultIfEmpty(0).Average()
            });

        foreach (var item in result)
        {
            Console.WriteLine($"Order {item.OrderId} ({item.Product}, qty: {item.Quantity}):");
            if (item.CountInspection != 0)
            {
                Console.WriteLine($"Inspections: {item.CountInspection}");
                Console.WriteLine($"Pass: {item.CountPass}, Fail: {item.CountFail}");
                Console.WriteLine($"Avg score: {item.AverageScore:F2}");
            }
            else
            {
                Console.WriteLine("No inspections");
            }    
            
        }    
    }

    public static void Summary(List<Order> orders, List<Inspection> inspections)
    {
        Console.WriteLine("--- Summary ---");
        Console.WriteLine($"Total orders: {orders.Count}");
        Console.WriteLine($"Total inspections: {inspections.Count}");
        var sumPass = inspections.Any(p => p.Result == "PASS")
            ? inspections.Count(p => p.Result == "PASS") : 0;

        double avgPass = inspections.Count > 0 ? ((double)sumPass * 100) / inspections.Count() : 0;
        Console.WriteLine($"Overall pass rate: {avgPass:F2}%");
    }
}


class Program
{
    static void Main()
    {
        List<Order> orders = new List<Order>();
        List<Inspection> inspections = new List<Inspection>();

        if (!int.TryParse(Console.ReadLine(), out int N) || N < 0) return;

        for (int i = 0; i < N; i++)
        {
            string[] s = Console.ReadLine().Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
            if (s.Length != 3) return ;
            Order order = new Order();
            order.OrderId = s[0];
            order.Product = s[1];
            if (!int.TryParse(s[2], out int p) || p < 0) return;
            order.Quantity = p;
            orders.Add(order);

        }

        if (!int.TryParse(Console.ReadLine(), out int M) || M < 0) return;

        for (int i = 0; i < M; i++)
        {
            string[] s = Console.ReadLine().Split(' ', 4, StringSplitOptions.RemoveEmptyEntries);
            if (s.Length != 4) return;
            Inspection inspection = new Inspection();
            inspection.InspID = s[0];
            inspection.OrderID = s[1];
            inspection.Result = s[2];
            if (!double.TryParse(s[3], out double p) || p < 0) return;
            inspection.Score = p;
            inspections.Add(inspection);
        }

        SubProgram.Merge(orders, inspections);
        SubProgram.Summary(orders, inspections);
    }
}

