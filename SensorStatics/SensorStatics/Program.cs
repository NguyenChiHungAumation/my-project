using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N)) return;
        

        double[] data = new double[N];
        for (int i = 0; i < N; i++)
        {
            string[] a = Console.ReadLine().Split(' ', 1, StringSplitOptions.RemoveEmptyEntries);
            if (a.Length != 1) return;
            if (!double.TryParse(a[0], out data[i])) return;

        }

        if(!double.TryParse(Console.ReadLine(), out double threshold));

        Console.WriteLine($"Count: {N}");
        double sum = data.Sum();
        double avg = data.Average();
        double min = data.Min();
        double max = data.Max();
        var above = data.Where(a => a > threshold).ToList();
        var below = data.Where(a => a < threshold).ToList();
        int numabove = above.Count;
        int numbelow = below.Count;
        Console.WriteLine($"Sum: {sum:F2}");
        Console.WriteLine($"Average: {avg:F2}");
        Console.WriteLine($"Min: {min:F2}");
        Console.WriteLine($"Max: {max:F2}");
        Console.WriteLine($"Above {threshold:F2}: {numabove}");
        Console.WriteLine($"Below {threshold:F2}: {numbelow}");
    }
}