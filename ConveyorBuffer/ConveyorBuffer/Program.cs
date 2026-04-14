using System;

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N) || N < 1 || N > 20) return;

        string[] viTri = new string[N];

        string a = Console.ReadLine();
        if (!int.TryParse(a, out int M) || M < 1 || M > 500) return;

        string[][] arr = new string[M][];

        for (int i = 0; i < M; i++)
        {
            string z = Console.ReadLine();
            arr[i] = z.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        }

        

        string[] spOut = new string[M];
        

        for (int i = 0; i < M; i++)
        {
            if (arr[i][0] == "IN" && arr[i].Length ==2)
            {
                if (viTri[0] != null)
                {
                    Console.WriteLine($"BLOCKED {arr[i][1]}");
                    continue;
                } 
                else
                {
                    viTri[0] = arr[i][1];
                    continue;
                }    
            }

            else if (arr[i][0] == "TICK" && arr[i].Length == 1)
            {
                spOut[i] = viTri[N - 1];
                for (int j = N - 1; j > 0; j--)
                {
                    viTri[j] = viTri[j - 1];
                }
                viTri[0] = null;
                
            }

            else if (arr[i][0] == "OUTPUT" && arr[i].Length == 1)
            {
                bool tatCaDeuNull = true;

                for (int v = 0; v < M; v++)
                {
                    if (spOut[v] != null)
                    {
                        tatCaDeuNull = false;
                        break;
                    }
                    else continue;
                }

                if (tatCaDeuNull == false)
                {
                    Console.Write("Produced: ");
                    bool first = true;

                    for (int j = 0; j < M; j++)
                    {
                        if (spOut[j] != null)
                        {
                            if (!first) Console.Write(" ");
                            Console.Write(spOut[j]);
                            first = false;
                            continue;
                        }
                        else continue;
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"Produced: none");
                }    

            }


            else if (arr[i][0] == "STATUS" && arr[i].Length == 1)
            {
                for (int v = 0;v < N; v++)
                {
                    if (viTri[v] != null)
                    {
                        Console.Write($"[{viTri[v]}]");
                    }
                    else Console.Write($"[.]");
                } 
                Console.WriteLine();
            }    


        }
    }
}