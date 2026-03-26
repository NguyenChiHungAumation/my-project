using System;
class Progarm
{
    static void Main()
    {
        
        string inputLine1 = Console.ReadLine();
        string[] arr = inputLine1.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (arr.Length != 2) return;
        if (!int.TryParse(arr[0], out int N) || N < 1) return;
        if (!int.TryParse(arr[1], out int K) || K < 1) return;
        //if (!int.TryParse(arr[0], out int N) || N < K || N > 10000) return;
        if (N > 10000 || N < K || K < 1) return;

        //string[][] arr1 = new string[N][];
        string inputLine2 = Console.ReadLine();
        string[] arr1 = inputLine2.Split(" ",StringSplitOptions.RemoveEmptyEntries);

        if (arr1.Length != N) return;


        int[] data = new int[N];
        for (int i = 0; i < N; i++)
        {
            if (!int.TryParse(arr1[i], out data[i]) || data[i] < 0) return;
            else continue;

        }   
        
        for (int i = 0; i < N; i++)
        {
            int strart = Math.Max(0, i - K + 1);
            int sum = 0;
            for (int j = strart; j <= i; j++)
            {
                sum += data[j];
            }
            int count = i - strart + 1;
            int result = sum / count;
            Console.Write(result);
            Console.Write(" ");
            

        }    
    }
}