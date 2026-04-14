using System;

class Program
{
    static void Main()
    {
        string[] s = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (s.Length != 2) return;
        if (!int.TryParse(s[0], out int H) || H < 1 || H > 50) return;
        if (!int.TryParse(s[1], out int W) || W < 1 || W > 50) return;
    }
}

