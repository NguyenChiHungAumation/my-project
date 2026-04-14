using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantily {  get; set; }
}


class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        Console.WriteLine("Lesson06 - Collections");

        Product p = new Product();
        p.Code = "P01";
        p.Name = "Motor";
        p.Price = 100;
        p.Quantily = 5;
        Console.WriteLine($"{p.Code} - {p.Name} - {p.Price} - {p.Quantily}");

        Console.WriteLine("-- Bước 3 --");

        List<Product> products = new List<Product>();

        products.Add(new Product { Code = "P01", Name = "Motor", Price = 100, Quantily = 5});
        products.Add(new Product { Code = "P02", Name = "Sensor", Price = 50, Quantily = 10});
        products.Add(new Product { Code = "P03", Name = "Camera", Price = 300, Quantily = 2});
        products.Add(new Product { Code = "P04", Name = "Cylinder", Price = 120, Quantily = 7});
        products.Add(new Product { Code = "P05", Name = "Conveyor", Price = 500, Quantily = 1});

        Console.WriteLine(products.Count);

        Console.WriteLine("-- Step 4");

        foreach (var item in products)
        {
            Console.WriteLine($"{item.Code} - {item.Name} - {item.Price} - {item.Quantily}");
        }

        Console.WriteLine("-- Step 5");

        ///Tìm sản phẩm theo Code
        static Product FindByCode(List<Product> products, string code)
        {
            foreach(var product in products)
            {
                if(product.Code == code) return product;
            }

            return null; 
        }

        Product foundProduct = FindByCode(products, "P10");
        if (foundProduct != null)
        {
            Console.WriteLine($"Tìm thấy: {foundProduct.Code} - {foundProduct.Name}");
        }
        else Console.WriteLine("Không tìm thấy sản phẩm.");

        Console.WriteLine("-- Bước 6 --");
        Dictionary<string, Product> ProductMap = new Dictionary<string, Product>();

        foreach (var product in products)
        {
            ProductMap[product.Code] = product;
        }
        Console.Write("Tìm kiếm Code: ");
        string input = Console.ReadLine();
        
        if (ProductMap.ContainsKey(input))
        {
            Product a = ProductMap[input];
            Console.WriteLine($"Tra cứu nhanh: {a.Code} - {a.Name}");
        } 
        else
        {
            Console.WriteLine($"Không tồn tại mã {input}");
        }

        Console.WriteLine("-- Bước 7 --");

        Queue<string> alarm = new Queue<string>();
        alarm.Enqueue("Overheat");
        alarm.Enqueue("Door Open");
        alarm.Enqueue("Low Pressure");

        Console.WriteLine("Danh sách Alarm đang chờ sử lý:");

        while(alarm.Count > 0)
        {
            string alarms = alarm.Dequeue();
            Console.WriteLine(alarms);
        }


        Console.WriteLine("-- Bước 8 --");
        Stack<string> actionStack = new Stack<string>();

        actionStack.Push("Start");
        actionStack.Push("Load Recipe");
        actionStack.Push("Home Axis");
        actionStack.Push("Reset Alarm");

        Console.WriteLine("Undo thao tác: ");
        while(actionStack.Count > 0)
        {
            string action = actionStack.Pop();
            Console.WriteLine(action);
        }    

    }
}