using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}

class ProgramProduct
{
    private static List<Product> products = new List<Product>();
    public static void AddProduct(Product product)
    {
        Product product2 = products.FirstOrDefault(p => p.Code == product.Code);
        if (product2 != null)
        {
            product2.Name = product.Name;
            product2.Price = product.Price;
            product2.Quantity = product.Quantity;
            Console.WriteLine($"Update: {product.Code}-{product.Name}-{product.Price}-{product.Quantity}");
        }
        else
        {
            products.Add(product);
            Console.WriteLine($"Added: {product.Code}-{product.Name}-{product.Price}-{product.Quantity}");
        }    
    }

    public static void PriceProduct()
    {
        var result = products.Where(p => p.Price > 100);
        if (result != null)
        {
            foreach(var item in result)
            {
                Console.WriteLine($"{item.Code}-{item.Name}-{item.Price}");
            }    
        }
        else
        {
            Console.WriteLine($"Không có sản phẩm có giá lớn hơn 100");
        }    
    }
    
    public static void DecreasingProduct()
    {
        var result1 = products.OrderByDescending(p => p.Quantity);
        foreach (var item in result1)
        {
            Console.WriteLine($"{item.Code}-{item.Name}-{item.Quantity}");
        }    
    }

    public static void ExamProduct(string name)
    {
        var result2 = products.FirstOrDefault(p => p.Name == name);
        if (result2 != null)
        {
            Console.WriteLine($"Có sản phẩm có tên {name}");
        }
        else
        {
            Console.WriteLine($"Không có sản phẩm có tên {name}");
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
            arr[i] = Console.ReadLine().Split(' ', 5, StringSplitOptions.RemoveEmptyEntries);
            if (arr[i].Length == 0) return;
        }   
        
        for (int i = 0;i < N; i++)
        {
            switch(arr[i][0].ToUpper())
            {
                case "ADD":
                    {
                        if (arr[i].Length == 5)
                        {
                            Product product = new Product();
                            product.Code = arr[i][1];
                            product.Name = arr[i][2];
                            if (!double.TryParse(arr[i][3], out double price)) return;
                            if (!int.TryParse(arr[i][4], out int quantity)) return;
                            product.Price = price;
                            product.Quantity = quantity;
                            ProgramProduct.AddProduct(product);
                            break;
                        }   
                        else
                        {
                            Console.WriteLine($"Sai cú pháp");
                            break;
                        }    
                    }
                case "PRICE":
                    {
                        if (arr[i].Length == 1)
                        {
                            ProgramProduct.PriceProduct();
                            break;
                        }   
                        else
                        {
                            Console.WriteLine($"Sai cú pháp");
                            break;
                        }    
                    }
                case "DEC":
                    {
                        if (arr[i].Length == 1)
                        {
                            ProgramProduct.DecreasingProduct();
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Sai cú pháp");
                            break;
                        }
                    }
                case "EXAM":
                    {
                        if (arr[i].Length == 2)
                        {
                            ProgramProduct.ExamProduct(arr[i][1]);
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Sai cú pháp");
                            break;
                        }
                    }
                    default:
                    {
                        Console.WriteLine("Sai cú pháp");
                        break;
                    }
            }

        }    
    }
}