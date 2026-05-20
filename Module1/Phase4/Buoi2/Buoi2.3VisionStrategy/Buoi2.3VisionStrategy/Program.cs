using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

enum Strategies { THRESHOLD, EDGE, HISTOGRAM };

interface IProduct
{
    string ProductId { get; }
    int Param {  get; }
    Strategies strategies { get; }
    List<int> Value { get; }
    bool Result { get; }

    void Calculator();
}

class Threshold : IProduct
{
    public string ProductId { get; set; }
    public int Param { get; set; }
    public Strategies strategies { get; set; } = Strategies.THRESHOLD;
    public List<int> Value { get; set; }
    public bool Result { get; private set; } = false;
    
    public Threshold(string productId, int param, List<int> value)
    {
        ProductId = productId;
        Param = param;
        Value = value;
    }

    public void Calculator()
    {
        int OverThreshold = Value.Count(p => p >= Param);
        double score =  ((double)OverThreshold / Value.Count) * 100;
        string result = (score >= 80) ? "PASS" : "FAIL";
        Console.WriteLine($"{ProductId} [{strategies}]: score={score:F1}% -> {result}");

        Result = (result == "PASS") ? true : false;
    }
}

class Edge : IProduct
{
    public string ProductId { get; set; }
    public int Param { get; set; }
    public Strategies strategies { get; set; } = Strategies.EDGE;
    public List<int> Value { get; set; }
    public bool Result { get; private set; } = false;

    public Edge(string productId, int param, List<int> value)
    {
        ProductId = productId;
        Param = param;
        Value = value;
    }

    public void Calculator()
    {
        int total = Value.Count - 1;
        int bigParam = 0;
        for (int i = 0; i < Value.Count - 1; i++)
        {
            int dev = Math.Abs(Value[i] - Value[i + 1]);

            if (dev > Param)
            {
                bigParam++;
            }    
        }  
        
        double score = ((double)bigParam / total) * 100;

        string result = (score >= 50) ? "PASS" : "FAIL";

        Console.WriteLine($"{ProductId} [{strategies}]: score={score:F1}% -> {result}");

        Result = (result == "PASS") ? true : false;
    }
}

class Histogram : IProduct
{
    public string ProductId { get; set; }
    public int Param { get; set; }
    public Strategies strategies { get; set; } = Strategies.HISTOGRAM;
    public List<int> Value { get; set; }
    public bool Result { get; set; } = false;

    public Histogram(string productId, int param, List<int> value)
    {
        ProductId = productId;
        Param = param;
        Value = value;
    }

    public void Calculator()
    {
        int max = Value.Max(p => p);
        int min = Value.Min(p => p);

        double score = 100 - ((double)(max - min) / max * 100);

        string result = (score >= 60) ? "PASS" : "FAIL";

        Console.WriteLine($"{ProductId} [{strategies}]: score={score:F1}% -> {result}");

        Result = (result == "PASS") ? true : false;
    }
}

class Process
{
    int Total = 0;
    int NumPass = 0;

    public void Final()
    {
        double percentPass = ((double)NumPass / Total) * 100; 
        Console.WriteLine($"Summary: {NumPass}/{Total} passed ({percentPass:F1})");
    }
    public void Command(string line1, string line2)
    {
        Total++;

        string[] data1 = line1.Split(' ', 3);

        string productId = data1[0];
        string kind = data1[1];
        int param = int.Parse(data1[2]);

        string[] data2 = line2.Split(' ');
        List<int> value = new List<int>();

        for (int i = 0; i < data2.Length; i++)
        {
            value.Add(int.Parse(data2[i]));
        }
        
        switch(kind)
        {
            case "THRESHOLD":
                {
                    IProduct product = new Threshold(productId, param, value);
                    product.Calculator();
               
                    if (product.Result)
                    {
                        NumPass++;
                    }    
                    break;
                }
            case "EDGE":
                {
                    IProduct product = new Edge(productId, param, value);
                    product.Calculator();

                    if (product.Result)
                    {
                        NumPass++;
                    }
                    break;
                }
            case "HISTOGRAM":
                {
                    IProduct product = new Histogram(productId, param, value);
                    product.Calculator();

                    if (product.Result)
                    {
                        NumPass++;
                    }
                    break;
                }
        } 
        

    }
}

class Program
{
    static void Main()
    {
        Process process = new Process();
        int N;
        try
        {
            N = int.Parse(Console.ReadLine()); // Nhập số Frame 
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

        for (int i = 0; i < N; i++)
        {
            string line1 = Console.ReadLine();
            if (line1 == null) return;

            string line2 = Console.ReadLine();
            if (line2 == null) return;

            process.Command(line1, line2);
        }

        process.Final();
    }
}
