using System;

class LockOrder
{
    public string ThreadName { get; set; }
    public List<string> Locks { get; set; } = new List<string>();

    public LockOrder(string threadName)
    {
        ThreadName = threadName;
    }
}

class ProcessLockOrder
{
    private List<LockOrder> lockOrders = new List<LockOrder>();
    public void AddLockOrder(LockOrder lockOrder)
    {
        lockOrders.Add(lockOrder);

        string result = string.Join(" -> ", lockOrder.Locks);

        Console.WriteLine($"{lockOrder.ThreadName}: {result}");
    }

    public void DeadLock()
    {
        List<(int, int, int)> values = new List<(int, int, int)> ();
        int countMin = 0;

        for (int i = 0; i < lockOrders.Count; i++)
        {
            for(int j = 1; j < lockOrders.Count; j++)
            {
                bool hasFrist = lockOrders[i].Locks[0] == lockOrders[j].Locks[lockOrders[j].Locks.Count - 1];

                bool hasLast = lockOrders[i].Locks[lockOrders[i].Locks.Count - 1] == lockOrders[j].Locks[0];

                if (hasFrist && hasLast)
                {
                    int count = lockOrders[i].Locks.Count + lockOrders[j].Locks.Count - 1;
                    values.Add((i, j, count));
                    continue;
                }
                else continue;
                   
            }
            continue;
        }

        if (values.Count == 0)
        {
            Console.WriteLine("No dead potential");
        }
        else
        {
            var filter = values.OrderBy(p => p.Item3).FirstOrDefault();
            int iFilter = filter.Item1;
            int jFilter = filter.Item2;
            int countFilter = filter.Item3;

            string result1 = string.Join(" -> ", lockOrders[iFilter].Locks);
            string result2 = string.Join(" -> ", lockOrders[jFilter].Locks.Skip(1));

            Console.WriteLine($"DEADLOCK POTENTIAL: {result1} -> {result2}");
        }
    }
}

class Program
{
    static void Main()
    {
        ProcessLockOrder process = new ProcessLockOrder();

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

        for (int i = 0; i < N; i++)
        {
            string[] line = data[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (line.Length > 0)
            {
                LockOrder lockOrder = new LockOrder(line[0]);

                for (int j = 1; j < line.Length; j++)
                {
                    lockOrder.Locks.Add(line[j]);
                }

                process.AddLockOrder(lockOrder);

                //Console.WriteLine($"ThreadName: {lockOrder.ThreadName}");
                //foreach (var item in lockOrder.Locks)
                //{
                //    Console.WriteLine($"{item} ");
                //}    
            }

            else return;
        }


        process.DeadLock();
    }
}