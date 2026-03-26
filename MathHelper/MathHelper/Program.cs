using System;
using System.Security.Cryptography.X509Certificates;

static class MathHelper
{
    public static double Distance(double x1, double y1, double x2, double y2)
    {
        double x = x2 - x1;
        double y = y2 - y1;
        return Math.Sqrt(x * x + y * y);
    }

    public static double Angle(double x1, double y1, double x2, double y2)
    {
        double dx = x2 - x1;
        double dy = y2 - y1;
        double redians = Math.Atan2(dy, dx);
        double deg = redians * 180 / Math.PI;
        if (deg < 0)
        {
            deg += 360;
        }
        else return deg;

        return deg;

    }

    public static void MidPoint(double x1, double y1, double x2, double y2, out double mx, out double my)
    {
        mx =(x1 + x2) / 2;
        my =(y1 + y2) / 2;
    }
}



class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N) || N < 0) return;

        string[][] arr = new string[N][];
        
        /*for (int i = 0; i < N; i++)
        {
            arr[i] = new string[5];
        }    */

        for (int i = 0;i < N; i++)
        {
            string input = Console.ReadLine();
            arr[i] = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (arr[i].Length != 5) return;
            else continue;
        }  
        
        for (int i = 0; i < N; i++)
        {
            if (arr[i][0] == "DIST")
            {
                if (!double.TryParse(arr[i][1], out double x1)) return;
                if (!double.TryParse(arr[i][2], out double y1)) return;
                if (!double.TryParse(arr[i][3], out double x2)) return;
                if (!double.TryParse(arr[i][4], out double y2)) return;

                double distance = MathHelper.Distance(x1, y1, x2, y2);
                Console.WriteLine(distance.ToString("F4"));
                continue;
            } 
            
            else if (arr[i][0] == "ANGLE")
            {
                if (!double.TryParse(arr[i][1], out double x1)) return;
                if (!double.TryParse(arr[i][2], out double y1)) return;
                if (!double.TryParse(arr[i][3], out double x2)) return;
                if (!double.TryParse(arr[i][4], out double y2)) return;
                double angle = MathHelper.Angle(x1, y1, x2, y2);
                Console.WriteLine(angle.ToString("F4"));
                continue;
            }  
            

            else if (arr[i][0] == "MID")
            {
                if (!double.TryParse(arr[i][1], out double x1)) return;
                if (!double.TryParse(arr[i][2], out double y1)) return;
                if (!double.TryParse(arr[i][3], out double x2)) return;
                if (!double.TryParse(arr[i][4], out double y2)) return;


                MathHelper.MidPoint(x1, y1, x2, y2, out double mx, out double my);
                Console.WriteLine($"{mx.ToString("F4")} {my.ToString("F4")}");


            }    
        }    
    }
}
