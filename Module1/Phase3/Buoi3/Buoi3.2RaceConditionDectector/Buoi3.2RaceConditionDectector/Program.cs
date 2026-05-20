using System;
using System.Collections.Generic;

class DataIn
{
    public string Thread {  get; set; }
    public string Operation { get; set; }
    public string Variable { get; set; }
    public bool Locked { get; set; }
    public int Line { get; set; }

}

class ProcessDadaa
{
    private List<DataIn> dataIns = new List<DataIn>();

    public void AddData(string thread, string operation, string variable, bool locked, int line)
    {
        dataIns.Add(new DataIn
        {
            Thread = thread,
            Operation = operation,
            Variable = variable,
            Locked = locked,
            Line = line
        });
    }

    public void FilterData()
    {
        int count = 0;

        for (int i = 0; i < dataIns.Count; i++)
        {
            for (int j = i + 1; j < dataIns.Count; j++)
            {
                DataIn a = dataIns[i];
                DataIn b = dataIns[j];

                bool sameVariable = a.Variable == b.Variable;
                bool differentThread = a.Thread != b.Thread;
                bool hasWrite = a.Operation == "WRITE" || b.Operation == "WRITE";
                bool bothNoLock = a.Locked == false && b.Locked == false;

                if (sameVariable && differentThread && hasWrite && bothNoLock)
                {
                    Console.WriteLine($"RACE: {a.Thread} {a.Operation} vs {b.Thread} {b.Operation} on {a.Variable} (line {a.Line} vs {b.Line})");

                    count++;
                    break;
                }    
            }    
        } 
        
        if (count == 0)
        {
            Console.WriteLine("No race condition detected");
        }    
        else
        {
            Console.WriteLine($"Total races: {count}");
        }    
    }
} 

class Program
{
    static void Main()
    {
        ProcessDadaa processDadaa = new ProcessDadaa();

        int N;
        try
        {
            N = int.Parse(Console.ReadLine());
            if (N < 0)
                throw new Exception();
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Lỗi: không được để trống");
            return;
        }
        catch (FormatException)
        {
            Console.WriteLine("Lỗi: nhập vào phải là số nguyên");
            return;
        }
        catch (OverflowException)
        {
            Console.WriteLine("Lỗi: số quá lớn");
            return;
        }
        catch (Exception)
        {
            Console.WriteLine("Lỗi: số nhập vào phải >= 0");
            return;
        }

        string[] data = new string[N];

        try
        {
            for (int i = 0; i < N; i++)
            {

                data[i] = Console.ReadLine();
                if (data[i].Length == 0)
                    throw new Exception();

            }
        }
        catch (Exception)
        {
            Console.WriteLine("Lỗi :không được để rỗng");
        }

        bool Nolocked = false;

        for (int i = 0; i < N; i++)
        {
            
            string[] line = data[i].Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
            if (line.Length == 0) return;

            if (line[1] == "LOCK")
            {
                Nolocked = true;
                //Console.WriteLine($"locked: {Nolocked}");

                continue;
            }
            else if (line[1] == "UNLOCK")
            {
                Nolocked = false;
                //Console.WriteLine($"Unlocked: {Nolocked}");

                continue;
            }    

            else if (line[1] == "WRITE" || line[1] == "READ")
            {
                string thread = line[0];
                string operation = line[1];
                string variable = line[2];
                bool locked = Nolocked;
                int lines = i +1;
              
                processDadaa.AddData(thread, operation, variable, locked, lines);

                //Console.WriteLine($"{thread} {operation} {variable} {locked} {lines}");
                continue;
            } 
            
                
        }

        processDadaa.FilterData();
    }
}