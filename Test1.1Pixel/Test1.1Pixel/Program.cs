using System;

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N) || N < 1 || N > 1000) return;

        string[][] arr = new string[N][];
        int[] data = new int[N];

        for (int i = 0; i < N; i++)
        {
            string a = Console.ReadLine();
            arr[i] = a.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (!int.TryParse(arr[i][0], out int M) || M < 0 || M > 255 || arr[i].Length != 1) return;

            //data[i] = int.Parse(arr[i]);
            data[i] = Convert.ToInt32((arr[i][0]));
        }

     
        for (int i = 0; i < N; i++)
        {
            if (data[i] >= 0 && data[i] <= 50)
            {
                Console.WriteLine("BLACK");
                continue;
            }

            else if (data[i] >= 51 && data[i] <= 100)
            {
                Console.WriteLine("DARK");
                continue;
            }

            else if (data[i] >= 101 && data[i] <= 170)
            {
                Console.WriteLine("GRAY");
                continue;
            }

            else if (data[i] >= 171 && data[i] <= 220)
            {
                Console.WriteLine("LIGHT");
                continue;
            }

            else if (data[i] >= 221 && data[i] <= 255)
            {
                Console.WriteLine("WHITE");
                continue;
            }

        }    
    }
            
                
                



}
