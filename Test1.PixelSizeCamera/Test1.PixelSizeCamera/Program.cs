using System;

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!double.TryParse(s, out double FOV) || FOV < 0.001 || FOV > 1000) return;
        string a = Console.ReadLine();
        if (!int.TryParse(a, out int resolution) || resolution < 1 || resolution > 10000) return;
        double kq = FOV / resolution;
        Console.WriteLine(kq.ToString("F4"));
    }
}
