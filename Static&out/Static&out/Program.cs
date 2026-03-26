using System;

class ProductionStatus
{
    public static double Yield(int total,int passed)
    {
        return (double)passed / total * 100;
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
        
        string s = Console.ReadLine(); // Nhập số N
        if (!int.TryParse(s, out int N) || N <= 0) return; //Kiểm tra là số nguyên và lớn hơn 0

        

        string a = Console.ReadLine(); // Nhập chuỗi ở dòng thứ 2 
        string[] input = a.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (input.Length != N) return;

        int[] data = new int[N];

        for (int i = 0; i < N; i++)
        {
            if (!int.TryParse(input[i], out data[i]) || data[i] < 0) return ;
            //data[i] = int.Parse(input[i]);
        }    




        /*if (data.Length == N)
        {
            for (int i = 0; i < N; i++)
            {
                if (data[i] <= 0)
                {
                    return;
                }
                else
                    continue;

            }
        }
        else
            return;*/


        //int min = 0;
       // int max = 0;
        //double avg = 0;

        //ProductionStatus.Analyze(data,out min, out max, out avg);
        //Console.WriteLine($"Min={min} Max={max} Avg={avg.ToString("F2")}");
        

        //Nhập dòng 3
        string inputDong3 = Console.ReadLine();
        if (!int.TryParse(inputDong3, out int M) || M <= 0)
        {
            return;
        }
        
        string[][] arr = new string[M][];

        for (int i = 0; i < M; i++)
        {
            string input1 = Console.ReadLine();
            arr[i] = input1.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (arr[i].Length == 0) return;
            else continue;
        }


        for (int i = 0; i < M; i++)
        {
            if (arr[i][0] == "ANALYZE" && arr[i].Length == 1)
            {

                int min, max;
                double avg;
                ProductionStatus.Analyze(data, out min, out max, out avg);
                Console.WriteLine($"Min={min} Max={max} Avg={avg.ToString("F2")}");
                continue;

            }
            else if (arr[i][0] == "YIELD" && arr[i].Length == 3)
            {
                if (!int.TryParse(arr[i][1], out int total)) return;
                if (!int.TryParse(arr[i][2], out int passed)) return;
                //int total = int.Parse(arr[i][1]);
                //int passed = int.Parse(arr[i][2]);
                if (total > 0 && passed >= 0 && passed <= total)
                {
                    double Yield = ProductionStatus.Yield(total, passed);
                    Console.WriteLine($"{Yield.ToString("F2")}%");
                }
                else return;
                //Console.WriteLine($"{Yield.ToString("F2")}%");
                continue;

            }

            else if (arr[i][0] == "GRADE" && arr[i].Length == 2)
            {
                if (!double.TryParse(arr[i][1], out double yield)) return;
                //double yield = double.TryParse(arr[i][1]);
                string Grade = ProductionStatus.Grade(yield);
                Console.WriteLine($"Grade: {Grade}");
                continue;
            }
            else
                return;

            

            


            

        }
            
       

        
    }
}
