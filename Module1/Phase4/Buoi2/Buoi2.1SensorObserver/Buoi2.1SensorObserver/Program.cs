using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;



class Process
{
    public int Count { get; private set; } = 0;
    
    Dictionary<string, List<string>> Storage = new Dictionary<string, List<string>>();    
    public void Subscribe(string observerName, string sensorName)
    {       
                
        if (!Storage.ContainsKey(sensorName))
        {
            Storage[sensorName] = new List<string>(); 
        }
        
        if (!Storage[sensorName].Contains(observerName))
        {
            Storage[sensorName].Add(observerName);

            Console.WriteLine($"{observerName} subscribed to {sensorName}");
        }   
        
                   
    }

    public void Unsubscribe(string observerName, string sensorName)
    {
        

        if (Storage.ContainsKey(sensorName))
        {
            if (Storage[sensorName].Contains(observerName))
            {
                Console.WriteLine($"{observerName} unsubscribed from {sensorName}");
                Storage[sensorName].Remove(observerName);
            }  
            else
            {
                Console.WriteLine($"{observerName} not subscribed to {sensorName}");
            }    
        } 
        else
        {
            Console.WriteLine($"{observerName} not subscribed to {sensorName}");
        }    

          
    }

    public void Publish(string sensorName, double value)
    {
        
        
        Console.WriteLine($"{sensorName} published: {value:F2}");

        if (Storage.ContainsKey(sensorName))
        {
            foreach (var item in Storage[sensorName])
            {
                Console.WriteLine($"-> {item} received {value} from {sensorName}");
                Count++;
            }    
        }
        else
        {
            Console.WriteLine($"-> no observers");
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
            string[] line2 = Console.ReadLine().Split(' ', 3);
            if (line2.Length != 3) return;

            switch (line2[0])
            {
                case "SUBSCRIBE":
                    {
                        process.Subscribe(line2[1], line2[2]);
                        break;
                    }
                case "UNSUBSCRIBE":
                    {
                        process.Unsubscribe(line2[1], line2[2]);
                        break;
                    }
                case "PUBLISH":
                    {
                        process.Publish(line2[1], double.Parse(line2[2]));
                        break;
                    }
                
            }
        }
        Console.WriteLine($"Total notifications: {process.Count}");
    }
}