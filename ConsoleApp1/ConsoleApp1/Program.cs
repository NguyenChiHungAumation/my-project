using System;
using System.Security.Cryptography.X509Certificates;
static class UnitConverter
{

    public static double PixelToMM(double pixel, double ratio)
    {
        return pixel * ratio;

    }

    public static double MMToPixel(double mm, double ratio)
    {
        return mm / ratio;

    }

    public static double PulseToMM (int pulse, double mm)
    {
        return pulse / mm;
    }

    public static double DegreesToRadians(double deg)
    {
        return deg * Math.PI / 180;
    }

    public static double RadiansToDegrees(double rad)
    {
        return rad * 180 / Math.PI;
    }
}

class Program
{
    static void Main()
    {
        string inputLine1 = Console.ReadLine();
        if (!int.TryParse(inputLine1, out int N) || N <= 0) return;

        string[][] arr = new string[N][]; 

        for (int i = 0; i < N; i++)
        {
            string input = Console.ReadLine();
            arr[i] = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (arr[i].Length == 0) return;
            else continue;

        }

        for (int i = 0;i < N; i++)
        {
            if (arr[i][0] == "P2M" && arr[i].Length == 3)
            {
                if (!double.TryParse(arr[i][1], out double pixel) || !double.TryParse(arr[i][2], out double ratio))
                { return; }
                else
                {
                    double pixelToMM = UnitConverter.PixelToMM(pixel, ratio);
                    Console.WriteLine(pixelToMM.ToString("F4"));
                    continue;
                }

            }
            
            else if (arr[i][0] == "M2P" && arr[i].Length == 3)
            {
                if (!double.TryParse(arr[i][1], out double mm ) || !double.TryParse(arr[i][2], out double ratio))
                {
                    return;
                }
                else
                {
                    double mmToPixel = UnitConverter.MMToPixel(mm, ratio);
                    Console.WriteLine(mmToPixel.ToString("F4"));
                    continue ;
                }    

            }    



        }    
    }
}
