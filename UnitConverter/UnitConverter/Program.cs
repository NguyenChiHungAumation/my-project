using System;
using System.ComponentModel.Design;


class UnitConverter
{
    public static double PixelToMM(double pixel, double ratio)
    {
        return pixel * ratio;
    }

    public static double MMToPixel(double mm, double ratio)
    {
        return mm / ratio;
    }

    public static double PulseToMM(int pulse, double pulsessPerMM)
    {
        return pulse / pulsessPerMM;
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
        // Nhập dòng 1
        string input = Console.ReadLine();
        if (!int.TryParse(input, out int N) || N <= 0 )
            return;

        // Tạo mảng 2 chiều
        string[][] lenhNhap = new string[N][];

        for (int i = 0; i < N; i++)
        {
            lenhNhap[i] = new string[10];
        }

        for (int i = 0; i < N; i++)
        {
            string input1 = Console.ReadLine();
            lenhNhap[i] = input1.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
        }

        for(int i = 0; i < N; i++)
        {
            // Pixel to MM
            if (lenhNhap[i][0] == "P2M")
            {
                if (lenhNhap[i].Length == 3)
                {
                    double pixel = double.Parse(lenhNhap[i][1]);
                    double ratio = double.Parse(lenhNhap[i][2]);

                    double pixelToRatio = UnitConverter.PixelToMM(pixel, ratio);
                    Console.WriteLine(pixelToRatio.ToString("F4"));

                }
                else
                    return;
                continue;

            }
            else if (lenhNhap[i][0] == "M2P")
            {
                if (lenhNhap[i].Length == 3)
                {
                    double mm = double.Parse(lenhNhap[i][1]);
                    double pixel = double.Parse(lenhNhap[i][2]);
                    double MnToPixel = UnitConverter.MMToPixel(mm, pixel);
                    Console.WriteLine(MnToPixel.ToString("F4"));
                }
                else
                    return;
                continue;
            }
            

            else if (lenhNhap[i][0] == "PUL" && lenhNhap[i].Length == 3)
            {
                int pluse = int.Parse(lenhNhap[i][1]);
                double mm = double.Parse(lenhNhap[i][2]);
                double pluseToMm = UnitConverter.PulseToMM(pluse, mm);
                Console.WriteLine(pluseToMm.ToString("F4"));
                continue;
            }    

            else if (lenhNhap[i][0] == "D2R" && lenhNhap[i].Length == 2)
            {
                double deg = double.Parse(lenhNhap[i][1]);
                double degToRadians = UnitConverter.DegreesToRadians(deg);
                Console.WriteLine(degToRadians.ToString("F4"));
                continue;
            }    


            else if (lenhNhap[i][0] == "R2D" && lenhNhap[i].Length == 2)
            {
                double radians = double.Parse(lenhNhap[i][1]);
                double radiansToDeg = UnitConverter.RadiansToDegrees(radians);
                Console.WriteLine(radiansToDeg.ToString("F4"));
                continue;

            }    



        }    






    }
}
