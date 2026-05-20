using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

class Process
{
    public int BufferSize { get; private set; }
    public bool ProduceComplete = false;

    private SemaphoreSlim semaphore;
    private ConcurrentQueue<string> waitingItem = new ConcurrentQueue<string>();
    private ConcurrentQueue<string> activeUsers = new ConcurrentQueue<string>();

    public Process(int bufferSize)
    {
        if (bufferSize <= 0) return;
        BufferSize = bufferSize;

        semaphore = new SemaphoreSlim(bufferSize);
    }

    public async Task Produce(string item)
    {
        ProduceComplete = true;

        if (semaphore.CurrentCount > 0 && semaphore.CurrentCount <= BufferSize)
        {
            await semaphore.WaitAsync();
            activeUsers.Enqueue(item);

            Console.WriteLine($"Produced: {item} (buffer: {activeUsers.Count}/{BufferSize})");
        }   
        else
        {


            Console.WriteLine($"BUFFER FULL - '{item}' dropper");
        }    
    }

    public void Consume()
    {
        bool fristQueue = activeUsers.TryDequeue(out var frist);

        if (fristQueue)
        {
            semaphore.Release();

            Console.WriteLine($"Consumed: {frist} (buffer: {activeUsers.Count}/{BufferSize})");
        }    
        else
        {
            Console.WriteLine($"BUFFER EMPTY");
        }    
    }

    public void Peek()
    {
        bool exis = activeUsers.TryPeek(out string result);

        if (exis)
        {
            Console.WriteLine($"Next: {result}");
        }    
        else
        {
            Console.WriteLine($"BUFFER EMPTY");
        }    
    }

    public void Status()
    {
        string status = string.Join(", ", activeUsers);

        Console.WriteLine($"Buffer: [{status}] ({activeUsers.Count}/{BufferSize})");
    }

    public void Complete()
    {
        if (ProduceComplete)
        {
            Console.WriteLine("Channel completed");
        }
        else
        {
            Console.WriteLine("Cannot produce - channel completed");
        }
    }
    
    public void ConsumeAll()
    {
        int count = 0;
        int numActive = activeUsers.Count;
        for (int i = 0; i <= numActive; i++)
        {
            bool result = activeUsers.TryDequeue(out var result1);

            if (result)
            {
                count++;

                Console.WriteLine($"Consumed: {result1}");
            }    
        }

        Console.WriteLine($"Drained: {count} items");
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
                case "PRODUCE":
                    {
                        if (line.Length == 2)
                        {
                            string item = line[1];

                            process.Produce(item);
                            break;

                        }
                        else
                        {
                            break;
                        }
                    }
                case "CONSUME":
                    {
                        if (line.Length == 1)
                        {

                            process.Consume();
                            break;

                        }
                        else
                        {
                            break;
                        }
                    }
                case "PEEK":
                    {
                        if (line.Length == 1)
                        {
                            process.Peek();
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
                case "COMPLETE":
                    {
                        if (line.Length == 1)
                        {
                            process.Complete();
                            break;

                        }
                        else
                        {
                            break;
                        }
                    }
                case "CONSUME_ALL":
                    {
                        if (line.Length == 1)
                        {
                            process.ConsumeAll();
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