using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantily {  get; set; }
}
class ProductManager
{
    private static List<Product> products = new List<Product>();

    private static Dictionary<string, Product> keyProduct = new Dictionary<string, Product>();


    public static void AddProduct(Product product)
    {
        if (keyProduct.ContainsKey(product.Code))
        {
            Console.WriteLine($"{product.Code} đã tồn tại!");
        }
        else
        {
            products.Add(product);
            foreach (var item in products)
            {
                keyProduct[item.Code] = item;
            }
        }
    }

    public static void ShowAll()
    {
        
        foreach (var item in products)
        {
            Console.WriteLine($"{item.Code} - {item.Name} - {item.Price} - {item.Quantily}");


        }    
    }

    public static void FinByCode(string code)
    {

        if (keyProduct.ContainsKey(code))
        {
            Product item = keyProduct[code];
            Console.WriteLine($"Sản phẩm tìm được: {item.Code} - {item.Name} - {item.Price} - {item.Quantily}");
        }
        else
        {
            Console.WriteLine($"Không tìm thấy mã: {code}");
        }    
            

    }

    public static void UpdateQuantily(string code, int Sl)
    {
               
        if (keyProduct.TryGetValue(code, out Product product))
        {
            if (Sl > product.Quantily) return;
            
            product.Quantily = product.Quantily - Sl;

            Console.Write($"Đã lấy: {Sl} - Còn lại: {product.Quantily}");
            Console.WriteLine();

        }
        else
        {
            Console.WriteLine($"Không tìm thấy mã: {code}");
        }


    }


    public static void DeleteByCode(string code)
    {
        if (keyProduct.ContainsKey(code))
        {
            Product product = keyProduct[code];
            keyProduct.Remove(code);
            products.Remove(product);
        }
        else
        {
            Console.WriteLine($"Không tìm thấy mã: {code}");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        Console.Write("Nhập số sản phẩm cần nhập: ");
        string q = Console.ReadLine();

        if (!int.TryParse(q, out int N)) return;

        string[] arr = new string[4];

        Console.WriteLine("Nhập sản phẩm: ");
        for (int i = 0; i < N; i++)
        {
            arr = Console.ReadLine().Split(' ', 4, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length != 4) return;
            string code = arr[0];
            string name = arr[1];
            if (!double.TryParse(arr[2], out double price)) return;
            if (!int.TryParse(arr[3], out int quantily)) return;
            Product product = new Product();

            product.Code = code;
            product.Name = name;
            product.Price = price;
            product.Quantily = quantily;

            ProductManager.AddProduct(product);
            
        }
        Console.WriteLine("--Danh sách đã nhập--");
        ProductManager.ShowAll();

        Console.WriteLine("--Tìm kiếm sản phẩm--");
        Console.Write("Nhập Code cần tìm: ");
        string findCode = Console.ReadLine();

        ProductManager.FinByCode(findCode);

        Console.WriteLine("--Số lượng cần lấy--");
        Console.Write("Nhập số lượng: ");
        if (!int.TryParse(Console.ReadLine(), out int findSl) || findSl < 0) return;
        ProductManager.UpdateQuantily(findCode, findSl);
        Console.WriteLine("--Update danh sách--");
        ProductManager.ShowAll();

        Console.WriteLine("--Remove Code--");

        Console.Write($"Nhập sản phẩn cần xóa: ");
        string deleteSp = Console.ReadLine();

        ProductManager.DeleteByCode(deleteSp);
        Console.WriteLine("--Update danh sách--");
        ProductManager.ShowAll();

    }
}