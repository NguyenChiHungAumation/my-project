using System;
using System.Data;

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int R) || R < 1 || R > 10) return;

        string[][] arr = new string[R][];
        //List<int> rules = new List<int>();

        for (int i = 0; i < R; i++)
        {
            string s2 = Console.ReadLine();
            arr[i] = s2.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (arr[i].Length != 8) return;
        }

        for (int i = 0; i < R; i++)
        {
            if (arr[i][0] != "RULE") return;
            if (!double.TryParse(arr[i][2], out double P)) return;
            if (!double.TryParse(arr[i][3], out P)) return;
            if (!double.TryParse(arr[i][4], out P) || P < 0 || P >255) return;
            if (!double.TryParse(arr[i][5], out P) || P < 0 || P > 255) return;
            if (!double.TryParse(arr[i][6], out P) || P < 0.0 || P > 1.0) return;
            if (!double.TryParse(arr[i][7], out P) || P < 0.0 || P > 1.0) return;
        }

        
        string z = Console.ReadLine();
        if (!int.TryParse(z, out int N) || N < 1 || N > 200) return;

        string[][] arr2 = new string[N][];
        for (int i = 0; i < N; i++)
        {
            string x = Console.ReadLine();
            arr2[i] = x.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (arr2[i].Length != 3) return;
            if (!double.TryParse(arr2[i][0], out double P)) return;
            if (!double.TryParse(arr2[i][1], out P)) return;
            if (!double.TryParse(arr2[i][2], out P)) return;

        }
        int[] count = new int[R];
        int count1 = 0;

          

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < R; j++)
            {
                if (!double.TryParse(arr2[i][0], out double size)) return;
                if (!double.TryParse(arr2[i][1], out double brightness)) return;
                if (!double.TryParse(arr2[i][2], out double circularity)) return;
                if (!double.TryParse(arr[j][2], out double sizeMin)) return;
                if (!double.TryParse(arr[j][3], out double sizeMax)) return;
                if (!double.TryParse(arr[j][4], out double brightMin) || brightMin < 0 || brightMin > 255) return;
                if (!double.TryParse(arr[j][5], out double brightMax) || brightMax < 0 || brightMax > 255) return;
                if (!double.TryParse(arr[j][6], out double circMin) || circMin < 0.0 || circMin > 1.0) return;
                if (!double.TryParse(arr[j][7], out double circMax) || circMax < 0.0 || circMax > 1.0) return;

                if (size >= sizeMin && size <= sizeMax &&
                    brightness >= brightMin && brightness <= brightMax &&
                    circularity >= circMin && circularity <= circMax)
                {
                    Console.WriteLine($"Product {i + 1}: {arr[j][1]}");
                    count[j]++;
                    break;
                }
                else
                {
                    if (j == R - 1)
                    {
                        Console.WriteLine($"Product {i + 1}: REJECT");
                        count1++;
                        break;
                    }
                    else continue;
                }    
            }

            

        }

        for (int i = 0; i < R; i++)
        {
            Console.WriteLine($"{arr[i][1]}: {count[i]}");
            
        }
        if (count1 != 0)
        {
            Console.WriteLine($"REJECT: {count1}");
        }
    }
}