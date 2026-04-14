using System;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static void Main()
    {
        if (!int.TryParse(Console.ReadLine(), out int N) || N < 1) return;

        List<(string id, string type, string result, double score)> sp = new List<(string, string, string, double)>();

        for (int i = 0; i < N; i++)
        {
            string[] data = Console.ReadLine().Split(' ', 4, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length != 4) return;
            if (data[2] != "OK" && data[2] != "NG") return;
            if (!double.TryParse(data[3], out double score)) return;
            string id = data[0];
            string type = data[1];
            string result = data[2];
            sp.Add((id, type, result, score));
        }

        Console.WriteLine($"Total: {N}");

        var ok = sp.Where(p => p.result == "OK").ToList();
        var ng = sp.Where(p => p.result == "NG").ToList();

        int numOk = ok.Count;
        int numNg = ng.Count;
        Console.WriteLine($"OK: {numOk}, NG: {numNg}");

        double yield = ((double)numOk / N) * 100;
        Console.WriteLine($"Yield: {yield:F2}%");

        Console.WriteLine("--- By Type ---");

        var byType = sp.GroupBy(p => p.type)
                       .OrderBy(g => g.Key);

        foreach (var group in byType)
        {
            int okCount = group.Count(x => x.result == "OK");
            double typeYield = (double)okCount / group.Count() * 100;

            Console.WriteLine($"{group.Key}: {group.Count()} items, {okCount} OK, yield {typeYield:F2}%");
        }

        Console.WriteLine("--- Scores ---");

        
        var top = sp.OrderByDescending(p => p.score).First();
        Console.WriteLine($"Top: {top.id} ({top.score:F2})");

        
        var bottom = sp.OrderBy(p => p.score).First();
        Console.WriteLine($"Bottom: {bottom.id} ({bottom.score:F2})");
       

        var sum = sp.Sum(p => p.score);
        double avg = sum / sp.Count;
        Console.WriteLine($"Average: {avg:F2}");

    }
}