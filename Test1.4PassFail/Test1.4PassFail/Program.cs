using System;
using System.Globalization;
class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N) || N < 1 || N > 100) return;

        string[][] arr = new string[N][];
        decimal[][] data = new decimal[N][];

        for (int i = 0; i < N; i++)
        {
            string a = Console.ReadLine();
            arr[i] = a.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (arr[i].Length != 3) return;

        }   
        

        for (int i = 0; i < N; i++)
        {
            data[i] = new decimal[3];

            for (int j = 0; j < arr[i].Length; j++)
            {
                
                if (!decimal.TryParse(arr[i][j], NumberStyles.Any, CultureInfo.InvariantCulture , out data[i][j])) return;
            }    
        }
        int countFail = 0;
        
        for (int i = 0; i < N; i++)
        {
            

            if (Math.Abs(data[i][1] - data[i][0]) <= data[i][2])
            {
                Console.WriteLine("PASS");
                continue;
            }
            
            else if (Math.Abs(data[i][1] - data[i][0]) > data[i][2])
            {
                countFail ++;
                Console.WriteLine("FAIL");
                continue;
            }    
        }

        if (countFail != 0)
        {
            Console.WriteLine("OVERALL: FAIL");
        }
        else
        {
            Console.WriteLine("OVERALL: PASS");
        }    
    }
}
