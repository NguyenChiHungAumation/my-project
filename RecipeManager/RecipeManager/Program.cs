using System;
using System.Collections.Generic;


class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N)) return;
        string[][] arr = new string[N][];
        for (int i = 0; i < N; i++)
        {
            arr[i] = Console.ReadLine().Split(' ', 2);

        }
        
        List<string> StepHienTai = new List<string>();
        Stack<string> StepDaXoa = new Stack<string>();
        Dictionary<string, List<string>> re = new Dictionary<string, List<string>>();

        for (int i = 0;i < N;i++)
        {
            if (arr[i][0] == "ADD" && arr[i].Length == 2)
            {
                string frist = arr[i][1];
                StepHienTai.Add(frist);
                Console.WriteLine($"Added: {StepHienTai[StepHienTai.Count - 1]}");
                continue;
            }    

            else if (arr[i][0] == "UNDO" && arr[i].Length == 1)
            {
                if (StepHienTai.Count != 0)
                {
                    string last = StepHienTai[StepHienTai.Count - 1];
                    StepDaXoa.Push(last);
                    StepHienTai.RemoveAt(StepHienTai.Count - 1);
                    Console.WriteLine($"Undone: {last}");
                    continue;
                }
                else Console.WriteLine($"Nothing to undo");
            }

            else if (arr[i][0] == "LIST" && arr[i].Length == 1)
            {
                if (StepHienTai.Count != 0)
                {
                    for (int j = 0; j < StepHienTai.Count; j++)
                    {
                        Console.WriteLine($"{j + 1}. {StepHienTai[j]}");
                    }
                    continue;

                }
                else
                {
                    Console.WriteLine($"Recipe is empty");
                    continue ;
                }
            }

            else if (arr[i][0] == "SAVE" && arr[i].Length == 2)
            {
                if (!re.ContainsKey(arr[i][1]))
                {
                    re.Add(arr[i][1], new List<string>(StepHienTai));
                    Console.WriteLine($"Saved recipe '{arr[i][1]}' ({re[arr[i][1]].Count} steps)");
                }
                else
                {
                    re[arr[i][1]] = new List<string>(StepHienTai);
                    Console.WriteLine($"Saved recipe '{arr[i][1]}' ({re[arr[i][1]].Count} steps)");
                    continue;
                }
            }

            else if (arr[i][0] == "LOAD" && arr[i].Length == 2)
            {
                if (re.ContainsKey(arr[i][1]))
                {
                    StepHienTai = new List<string>(re[arr[i][1]]);
                    StepDaXoa.Clear();
                    Console.WriteLine($"Loaded recipe '{arr[i][1]}' ({re[arr[i][1]].Count} steps)");
                }
                else
                {
                    Console.WriteLine($"Recipe '{arr[i][1]}' not found");
                    continue;
                }
            }

            else if (arr[i][0] == "HISTORY" && arr[i].Length == 1)
            {
                if (StepDaXoa.Count != 0)
                {
                    foreach (var item in StepDaXoa)
                    {
                        Console.WriteLine(item);
                    }
                    continue;
                }
                else
                {
                    Console.WriteLine($"No undo history");
                    continue;
                }
            }    
        }    
    }
}