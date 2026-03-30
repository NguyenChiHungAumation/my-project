using System;

class Production
{

    public static int CountPass {  get; set; } = 0;


    public static int CountFail { get; set; } = 0;

    public static int Count { get; set; } = 0;
    public static int Pass(string pass)
    {
        
        if (pass == "PASS")
        {
            CountPass++;
            Count++;
        }
        
        return CountPass;
    }

    public static int Fail(string fail)
    {

        if (fail == "FAIL")
        {
            CountFail++;
            Count++;
        }

        return CountFail;
    }

}


class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N) || N < 1 || N > 500) return;

        string[][] arr = new string[N][];

        for (int i = 0; i < N; i++)
        {
            string a = Console.ReadLine();
            arr[i] = a.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (arr[i].Length != 1 || double.TryParse(arr[i][0], out double M)) return;
        }

        double phanTramPass = 0.0;

        double phanTramFail = 0.0;

        for (int i = 0;i < N; i++)
        {
            if (arr[i][0] == "PASS")
            {
                Production.Pass(arr[i][0]);
                continue;
            }

            else if (arr[i][0] == "FAIL")
            {
                Production.Fail(arr[i][0]);
                continue;
            }

            else if (arr[i][0] == "RESET")
            {
                Production.CountFail = 0;
                Production.CountPass = 0;
                Production.Count = 0;
                continue;
            }

            else if (arr[i][0] == "REPORT")
            {
                Console.WriteLine($"Total: {Production.Count}");
                if (Production.Count == 0)
                {
                    Console.WriteLine($"Pass: {Production.CountPass} (0.0%)");
                    Console.WriteLine($"Fail: {Production.CountFail} (0.0%)");
                    continue;
                }  
                else
                {
                    phanTramPass = ((double)Production.CountPass / Production.Count) * 100;
                    phanTramFail = ((double)Production.CountFail / Production.Count) * 100;

                    Console.WriteLine($"Pass: {Production.CountPass} ({phanTramPass.ToString("F1")}%)");
                    Console.WriteLine($"Fail: {Production.CountFail} ({phanTramFail.ToString("F1")}%)");

                }    

                continue;
            }


        }    
    }   
}

