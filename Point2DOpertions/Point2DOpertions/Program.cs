using System;
using System.Collections.Generic;

struct Point2D
{
    public double X {  get; set; }
    public double Y { get; set; }
    public Point2D(double x, double y)
    {
        X = x;
        Y = y;   
        
    }

    public double DistanceTo(Point2D other)
    {
        double dx = X - other.X;
        double dy = Y - other.Y;
        return Math.Sqrt(dx * dx + dy * dy);

    }

    public Point2D Add(Point2D other)
    {
        return new Point2D(X + other.X, Y + other.Y);
    }

    public override string ToString()
    {
        return $"({X:F2}, {Y:F2})";
    }
}

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N)) return;

        string[][] arr = new string[N][];

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        }

        Dictionary<string, Point2D> points = new Dictionary<string, Point2D>();

        for (int i = 0;i < arr.Length;i++)
        {
            if (arr[i][0] == "CREATE" && arr[i].Length == 4)
            {
                if (!double.TryParse(arr[i][2], out double x)) return;
                if (!double.TryParse(arr[i][3], out double y)) return;
                points[arr[i][1]] = new Point2D(x, y);
                Console.WriteLine($"{points[arr[i][1]]}");
                continue;
            }

            else if (arr[i][0] == "DIST" && arr[i].Length == 3)
            {
                if (!points.ContainsKey(arr[i][1]) || !points.ContainsKey(arr[i][2]) ) return;
                double distance = points[arr[i][1]].DistanceTo(points[arr[i][2]]);
                Console.WriteLine($"{distance:F4}");
                continue;
            }

            else if (arr[i][0] == "ADD" && arr[i].Length == 3)
            {
                if (!points.ContainsKey(arr[i][1]) || !points.ContainsKey(arr[i][2])) return;
                var add = points[arr[i][1]].Add(points[arr[i][2]]);
                Console.WriteLine($"{add}");
                continue;
            }

            else if (arr[i][0] == "PRINT" && arr[i].Length == 2)
            {
                if (!points.ContainsKey(arr[i][1])) return;
                Console.WriteLine($"{points[arr[i][1]]}");
                continue;
            }
        } 
        


    }
}