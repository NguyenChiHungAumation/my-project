using System;

class EdgeDetection
{
    public static void Gradient(int[] arr, int N, int E, out int soEdge)
    {
        int n = N - 2;
        int count = 0;
        for (int i = 0; i < N; i++)
        {
            if (i <= n && i >= 1)
            {
                int result = Math.Abs(arr[i + 1] - arr[i - 1]);
                if (result >= E)
                {
                    count++;
                }
                else continue;

            }
        }
        soEdge = count;
    }

    public static void Position(int[] arr, int N, int E)
    {
        int n = N - 2;
        int count = 0;
        for (int i = 0; i < N; i++)
        {
            if (i <= n && i >= 1)
            {
                int result = Math.Abs(arr[i + 1] - arr[i - 1]);
                if (result >= E)
                {
                    count++;
                    Console.Write(i);
                    Console.Write(" ");
                }
                else continue;

                

            }
        }
        if (count == 0)
        {
            Console.WriteLine("NONE");
        }    
        
    }



}

class Program
{
    static void Main()
    {
        string inputDong1 = Console.ReadLine();
        string[] arr = inputDong1.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (arr.Length != 2) return;

        int[] data = new int[arr.Length];
        if (!int.TryParse(arr[0], out data[0]) || data[0] < 2 || data[0] > 1000) return;
        if (!int.TryParse(arr[1], out data[1]) || data[1] < 1 || data[1] > 255) return;


        string inputDong2 = Console.ReadLine();
        string[] arr1 = inputDong2.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (arr1.Length != data[0]) return;

        int[] data2 = new int[arr1.Length];

        for (int i = 0; i < arr1.Length; i++)
        {
            if (!int.TryParse(arr1[i], out data2[i]) || data2[i] < 0 || data2[i] > 255) return;
            else continue;
        }    

        

    }
}