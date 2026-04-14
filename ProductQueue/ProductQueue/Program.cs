using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static void Main()
    {
        Queue<string> sp = new Queue<string>();

        string s = Console.ReadLine();
        if(!int.TryParse(s, out int N)) return;

        string[][] arr = new string[N][];

        for (int i = 0; i < N; i++)
        {
            arr[i] = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);


        }   
        
        for (int i = 0;i < N;i++)
        {
            if (arr[i][0] == "ENQUEUE" && arr[i].Length == 2)
            {
                sp.Enqueue(arr[i][1]);
                Console.WriteLine($"Enqueued: {arr[i][1]}");
                continue;
            }

            else if (arr[i][0] == "DEQUEUE" && arr[i].Length == 1)
            {
                if (sp.Count != 0)
                {
                    string takeOut = sp.Dequeue();
                    Console.WriteLine($"Dequeued: {takeOut}");
                    continue;
                }
                else Console.WriteLine($"Queue is empty");

            }

            else if (arr[i][0] == "PEEK" && arr[i].Length == 1)
            {
                if (sp.Count != 0)
                {
                    string takeOut = sp.Peek();
                    Console.WriteLine($"Front: {takeOut}");
                    continue;
                }
                else Console.WriteLine($"Queue is empty");

            }

            else if (arr[i][0] == "COUNT" && arr[i].Length == 1)
            {

                int sl = sp.Count;
                Console.WriteLine($"Count: {sl}");
                continue;
                

            }

            else if (arr[i][0] == "LIST" && arr[i].Length == 1)
            {

                int sl = sp.Count;
                if (sl != 0)
                {
                    for (int j = 0; j < sl; j++)
                    {
                        string name = sp.ElementAt(j);
                        Console.WriteLine($"{name}");
                        continue;
                    }
                }
                else Console.WriteLine($"Queue is empty");

            }
        }    
    }
}