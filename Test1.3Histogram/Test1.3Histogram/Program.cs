using System;

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N) || N < 1 || N > 1000) return;

        string[] arr = new string[N];
        int[] data = new int[N];

        string a = Console.ReadLine();
        arr = a.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (arr.Length != N) return;

        for (int i = 0; i < N; i++)
        {
            if (!int.TryParse(arr[i], out int M) || M < 0 || M > 255) return;
            data[i] = Convert.ToInt32(arr[i]);
            
        }
                 
        if (data.Length != N) return;

        int countBin0 = 0;
        int countBin1 = 0;
        int countBin2 = 0;
        int countBin3 = 0;


        for (int i = 0; i < N; i++)
        {
            if (data[i] >= 0 && data[i] <= 63)
            {
                countBin0++;
                continue;
            }


            else if (data[i] >= 64 && data[i] <= 127)
            {
                countBin1++;
                continue;
            }

            else if (data[i] >= 128 && data[i] <= 191)
            {
                countBin2++;
                continue;
            }

            else if (data[i] >= 192 && data[i] <= 255)
            {
                countBin3++;
                continue;
            }


        }

        Console.WriteLine($"Bin 0: {countBin0}");
        Console.WriteLine($"Bin 1: {countBin1}");
        Console.WriteLine($"Bin 2: {countBin2}");
        Console.WriteLine($"Bin 3: {countBin3}");




    }
}
