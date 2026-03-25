using System;

class ProductionStatus
{
    public static double Yield(int total,int passed)
    {
        return passed / total * 100;
    }

    public static void Analyze(int[] data, out int min, out int max, out double avg)
    {
        min = data[0];
        max = data[0];
        int sum = 0;
        for (int i = 0; i < data.Length; i++)
        {
            if (min > data[i])
            {
                min = data[i];
            }

            else if (data[i] > max)
            {
                max = data[i];

            }
            sum += data[i];

            
        }
        avg = (double)sum / data.Length;
    }

    public static string Grade(double yield)
    {
        if (yield >= 98)
        {
            return "A";
        }

        else if (yield >= 95 && yield < 98)
        {
            return "B";
        }

        else if (yield >= 90 && yield < 95)
        {
            return "C";
        }
        else
            return "F";

        
    }
}

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N) && N <= 0) return;

        string[] input = new string[N];

        for (int i = 0; i < N; i++)
        {
            input[i] = new string[10];
        }    

        for (int i = 0; i < N; i++)
        {
            string a = Console.Read();
            input[i] = a.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
