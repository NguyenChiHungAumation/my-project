using System;

class Base
{
    static void Main()
    {
        string[] arr = new string[2];
        string s = Console.ReadLine();
        //if (s.Length != 2) return;
        arr = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (arr.Length != 2) return;

        if (!int.TryParse(arr[0], out int N) || N < 1 || N > 1000) return;
        if (!int.TryParse(arr[1], out int T) || T < 0 || T > 255) return;

        string[] arr1 = new string[N];
        string b = Console.ReadLine();
        //if (b.Length != N) return;
        arr1 = b.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (arr1.Length != N) return;
        /*for (int i = 0; i < N; i++)
        {
            if (!int.TryParse(arr1[i], out int a) || a < 0 || a > 255) return;
            else continue;
        }*/

        int[] data = new int[N];

        for (int i = 0; i < N; i++)
        {
            if (!int.TryParse(arr1[i], out data[i]) || data[i] < 0 || data[i] > 255) return;
        }    

        int count = 0;

        for (int i = 0; i < N; i++)
        {
            if (data[i] > T)
            {
                count++;
                //Console.Write(i);
                //Console.Write(" ");
            }  
            else continue;
        }
        Console.WriteLine(count);

        if (count == 0)
        {
            Console.WriteLine("NONE");
        }
        else
        {


            for (int i = 0; i < N; i++)
            {
                if (data[i] > T)
                {
                    //count++;
                    Console.Write(i);
                    Console.Write(" ");
                }
                else continue;
            }
        }


    }
    

}
