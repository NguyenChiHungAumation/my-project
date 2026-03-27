using System;
using System.Collections.Generic;

abstract class Shape
{
    public string Name { get; set;  }
    public abstract double Area();

    public abstract double Perimeter();       
}

class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }
    public Rectangle(double width, double height)
    {
        Name = "Rectangle";
        Width = width;
        Height = height;
        
    }

    public override double Area()
    {
        return Width * Height;
    }

    public override double Perimeter()
    {
        return (Width + Height) * 2;
    }
}

class Circle : Shape
{
    double Radius { get; set; }
    public Circle(double radius)
    {
        Name = "Cirle";
        Radius = radius;
    }

    public override double Area()
    {
        return Math.PI * Radius * Radius;
    }

    public override double Perimeter()
    {
        return 2 * Math.PI * Radius;
    }
}

class Triangle : Shape
{
    double A { get; set; }
    double B { get; set; }
    double C { get; set; }
    public Triangle(double a, double b, double c)
    {
        Name = "Triangle";
        A = a;
        B = b;
        C = c;
    }

    public override double Area()
    {
        double s = (A + B + C) / 2;
        return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
    }

    public override double Perimeter()
    {
        return A + B + C;
    }

}

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N) || N < 0) return;

        string[][] arr1 = new string[N][];

        for (int i = 0; i < N; i++)
        {
            string a = Console.ReadLine();
            arr1[i] = a.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        string d = Console.ReadLine();
        if (!int.TryParse(d, out int M) || M < 0) return;

        string[][] arr2 = new string[M][];

        for (int i = 0; i < M; i++)
        {
            string a = Console.ReadLine();
            arr2[i] = a.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        string[][] arr = new string[N + M][];

        for (int i = 0; i < N; i++)
        {
            arr[i] = arr1[i];
        }    

        for (int i = 0; i < M; i++)
        {
            arr[i + N] = arr2[i];
        } 
        
        List<Shape> shapes = new List<Shape>();

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i][0] == "RECT" && arr[i].Length == 3)
            {
                if (!double.TryParse(arr[i][1], out double width)) return;
                if (!double.TryParse(arr[i][2], out double height)) return;
                Rectangle rectangle = new Rectangle(width, height);
                shapes.Add(rectangle);
                continue;
            }
            
            else if (arr[i][0] == "CIRCLE" && arr[i].Length == 2)
            {
                if (!double.TryParse(arr[i][1], out double radius)) return;
                Circle circle = new Circle(radius);
                shapes.Add(circle);
                continue;
            }

            else if (arr[i][0] == "TRI" && arr[i].Length == 4)
            {
                if (!double.TryParse(arr[i][1], out double a)) return;
                if (!double.TryParse(arr[i][2], out double b)) return;
                if (!double.TryParse(arr[i][3], out double c)) return;
                Triangle triangle = new Triangle(a, b, c);
                shapes.Add(triangle);
                continue;
            }

            else if (arr[i][0] == "AREA" && arr[i].Length == 2)
            {
                if (!int.TryParse(arr[i][1], out int index)) return;
                double area = shapes[index - 1].Area();
                Console.WriteLine($"{area.ToString("F4")}");     
                continue;
            }

            else if (arr[i][0] == "PERIMETER" && arr[i].Length == 2)
            {
                if (!int.TryParse(arr[i][1], out int index)) return;
                double perimeter = shapes[index - 1].Perimeter();
                Console.WriteLine($"{perimeter.ToString("F4")}");
                continue;
            }

            else if (arr[i][0] == "TOTAL_AREA" && arr[i].Length == 1)
            {
                double sum = 0;
                for (int j = 0; j < shapes.Count; j++)
                {
                    sum += shapes[j].Area();
                }    
                Console.WriteLine($"{sum.ToString("F4")}");
                continue;
            }
        }    
    }
}


