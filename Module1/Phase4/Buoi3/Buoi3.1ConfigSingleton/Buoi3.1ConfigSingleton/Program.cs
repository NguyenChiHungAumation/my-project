using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
    private string Key;
    private int Value;

    public Product(string key, int value)
    {
        Key = key;
        Value = value;
    }
        
}

class ManagerProduct
{
    private Dictionary<string, Product> products = new Dictionary<string, Product>();

    public void SetProduct(string key, int vaiue)
    {
        Product product = new Product(key, vaiue);

        if (products.ContainsKey(key))
        {
            Console.WriteLine($"");
        }   
        else
        {

        }    
    }
    public void GetProduct(string key)
    {

    }
}

