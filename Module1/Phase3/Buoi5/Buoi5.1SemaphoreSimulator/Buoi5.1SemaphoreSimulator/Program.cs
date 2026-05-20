using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

class Process
{
    int MaxSlots { get; set; }
    private SemaphoreSlim semaphore; 
    private ConcurrentQueue<string> waitingtasks = new ConcurrentQueue<string>();
    private List<string> activeUsers = new List<string>();
    public Process(int maxSlots)
    {
        if (maxSlots <= 0) return;
        MaxSlots = maxSlots;

        semaphore = new SemaphoreSlim(maxSlots);
    }

    public async Task Acquire(string name)
    {          

        if (semaphore.CurrentCount > 0)
        {
            await semaphore.WaitAsync();
            activeUsers.Add(name);
            
            Console.WriteLine($"{name} acquired (slots: {MaxSlots - semaphore.CurrentCount}/{MaxSlots})");

        }   
        else
        {
            waitingtasks.Enqueue(name);
            Console.WriteLine($"{name} waiting (queue: {waitingtasks.Count})");
        }
        
    }

    public async Task Release(string name)
    {
        if (activeUsers.Contains(name))
        {
            activeUsers.Remove(name);
            semaphore.Release();

            Console.WriteLine($"{name} released (slots: {MaxSlots - semaphore.CurrentCount}/{MaxSlots})");

            if (waitingtasks.Count > 0)
            {
                waitingtasks.TryDequeue(out string waiter);
                await semaphore.WaitAsync();
                activeUsers.Add(waiter);

                Console.WriteLine($"    {waiter} acquired (slots: {MaxSlots - semaphore.CurrentCount}/{MaxSlots})");
            }    
        }
        else
        {
            Console.WriteLine($"{name} has no slot");
        }    
        
    }

    public void Status()
    {

        string result = string.Join(", ", activeUsers);
        Console.WriteLine($"Active: [{result}]");

        string result1 = string.Join(", ", waitingtasks);
        Console.WriteLine($"Waiting: [{result1}]");

        Console.WriteLine($"Slots: {MaxSlots - semaphore.CurrentCount}/{MaxSlots}");
            
    }
}

class Program
{
    static void Main()
    {
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

        Process process = new Process(N);

        int M;
        try
        {
            M = int.Parse(Console.ReadLine());
            if (M < 0)
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


        for (int i = 0; i < M; i++)
        {
            string[] line = Console.ReadLine().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

            if (line.Length == 0) return;

            switch (line[0])
            {
                case "ACQUIRE":
                    {
                        if (line.Length == 2)
                        {
                            string name = line[1];

                            process.Acquire(name);
                            break;
                            
                        }
                        else
                        {
                            break;
                        }
                    }
                case "RELEASE":
                    {
                        if (line.Length == 2)
                        {
                            string name = line[1];

                            process.Release(name);
                            break;

                        }
                        else
                        {
                            break;
                        }
                    }
                case "STATUS":
                    {
                        if (line.Length == 1)
                        {
                            process.Status();
                            break;

                        }
                        else
                        {
                            break;
                        }
                    }
                default:
                    {
                        return;
                    }
            }
        }    
    }
}