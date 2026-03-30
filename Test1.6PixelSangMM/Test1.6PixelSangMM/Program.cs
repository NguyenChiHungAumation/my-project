using System;
using System.Globalization;

class Pixel
{
    public static void XPoint(long X, long CenterX, double Size, out double MmX)
    {
        MmX = (X - CenterX) * Size;
    }

    public static void YPoint(long Y, long CenterY, double Size, out double MmY)
    {
        MmY = (CenterY - Y) * Size;
    }
}

class Program
{
    static void Main()
    {
        string a = Console.ReadLine();
        string[] arr = a.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
        if (arr.Length != 3) return;
        if (!long.TryParse(arr[0], out long centerX)) return;
        if (!long.TryParse(arr[1], out long centerY)) return;
        //if (!arr[2].Contains('.')) return;
        if (!double.TryParse(arr[2], out double s)) return;

        string d = Console.ReadLine();
        if (!int.TryParse(d, out int N) || N < 1 || N > 200) return;

        string[][] arr1 = new string[N][];

        int[][] data = new int[N][];

        for (int i = 0; i < N; i++)
        {
            string z = Console.ReadLine();
            arr1[i] = z.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            if (arr1[i].Length != 2) return;
        }

        for (int i = 0; i < N; i++)
        {
            data[i] = new int[2];
            if (!long.TryParse(arr1[i][0], out long x)) return;
            if (!long.TryParse(arr1[i][1], out long y)) return;

            Pixel.XPoint(x, centerX, s, out double mmX);
            Pixel.YPoint(y, centerY, s, out double mmY);

            string sx = mmX.ToString("F3", CultureInfo.InvariantCulture); 
            string sy = mmY.ToString("F3", CultureInfo.InvariantCulture);

            if (sx == "-0.000") sx = "0.000";
            if (sy == "-0.000") sy = "0.000";
            Console.WriteLine($"{sx} {sy}");
        }
        



    }
}