using System;

class Digital
{
    public string Status { get; private set; } = "X";

    public void ON()
    {
        
        Status = "O";
    }

    public void OFF()
    {
        
        Status = "X";
    }


    public void TOG()
    {
        if (Status == "O") Status = "X";
        else Status = "O";
    }
}


class Program
{
    static void Main()
    {
        Digital[] digital = new Digital[8];

        for (int i = 0; i < 8; i++)
        {
             digital[i] = new Digital();
        }    

 

        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N) || N < 1 || N > 500) return;

        string[][] arr = new string[N][];

        for (int i = 0; i < N; i++)
        {
            string a = Console.ReadLine();
            arr[i] = a.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        }
        

        for (int i = 0; i < N; i++)
        {
            if (arr[i][0] == "SET" && arr[i].Length == 2)
            {
                for (int j = 0; j < digital.Length; j++)
                {
                    if (!int.TryParse(arr[i][1],  out int M) || M < 0 || M > 7) return;
                    if (M == j)
                    {
                        digital[j].ON();
                        break;
                    }
                    else continue;
                }
                continue;
            }

            else if (arr[i][0] == "CLR" && arr[i].Length == 2)
            {
                for (int j = 0; j < digital.Length; j++)
                {
                    if (!int.TryParse(arr[i][1], out int M) || M < 0 || M > 7) return;
                    if (M == j)
                    {
                        digital[j].OFF();
                        break;
                    }
                    else continue;
                }
                continue;
            }

            else if (arr[i][0] == "TOG" && arr[i].Length == 2)
            {
                for (int j = 0; j < digital.Length; j++)
                {
                    if (!int.TryParse(arr[i][1], out int M) || M < 0 || M > 7) return;
                    if (M == j)
                    {
                        digital[j].TOG();
                        break;
                    }
                    else continue;
                }
                continue;
            }

            else if (arr[i][0] == "STATUS" && arr[i].Length == 1)
            {
                for (int j = 0; j < digital.Length; j++)
                {
                    Console.Write($"[{digital[j].Status}]");
                }
                Console.WriteLine();
                continue;
            }

            else if (arr[i][0] == "COUNT" && arr[i].Length == 1)
            {
                int countOn = 0;
                for (int j = 0; j < digital.Length; j++)
                {
                    if (digital[j].Status == "O")
                    {
                        countOn++;
                        continue;
                    }
                    else continue;
                }
                Console.WriteLine($"{countOn}");
                continue;
            }

        }    


    }
}