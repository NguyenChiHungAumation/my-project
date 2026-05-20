using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

class Atomic
{
    public string Name { get; set; }
    public int InitValue { get; set; }
    public Atomic(string name, int initValue)
    {
        Name = name;
        InitValue = initValue;
    }
}

class Process
{
    private ConcurrentDictionary<string, Atomic> atomics = new ConcurrentDictionary<string, Atomic>();

    public void Create(string name, int initValue)
    {
        bool isAdded = atomics.TryAdd(name, new Atomic(name, initValue));
        if (isAdded == true)
        {
            Console.WriteLine($"Counter '{name}' = {initValue}");
        }   
        else
        {
            Console.WriteLine($"Counter '{name} already exists'");
            
        }    
    }


    public void Increment(string name)
    {
        bool success = false;

        if (atomics.TryGetValue(name, out Atomic existingAtomic))
        {
            var updateAtomic = new Atomic(name, existingAtomic.InitValue + 1);

            success = atomics.TryUpdate(name, updateAtomic, existingAtomic);
            if (success)
            {
                Console.WriteLine($"{name}: {existingAtomic.InitValue} -> {updateAtomic.InitValue}");

            }    
        }   
        else
        {
            Console.WriteLine($"Counter '{name}' not found");
        }    
    }

    public void Decrement(string name)
    {
        bool success = false;

        if (atomics.TryGetValue(name, out Atomic existingAtomic))
        {
            var updateAtomic = new Atomic(name, existingAtomic.InitValue - 1);

            success = atomics.TryUpdate(name, updateAtomic, existingAtomic);
            if (success)
            {
                Console.WriteLine($"{name}: {existingAtomic.InitValue} -> {updateAtomic.InitValue}");

            }
        }
        else
        {
            Console.WriteLine($"Counter '{name}' not found");
        }
    }

    public void Add(string name, int value)
    {
        bool success = false;

        if (atomics.TryGetValue(name, out Atomic existingAtomic))
        {
            var updateAtomic = new Atomic(name, existingAtomic.InitValue + value);

            success = atomics.TryUpdate(name, updateAtomic, existingAtomic);
            if (success)
            {
                Console.WriteLine($"{name}: {existingAtomic.InitValue} -> {updateAtomic.InitValue}");

            }
        }
        else
        {
            Console.WriteLine($"Counter '{name}' not found");
        }
    }

    public void CompareExchange(string name, int expected, int newValue)
    {
        bool success = false;

        if (atomics.TryGetValue(name, out Atomic existingAtomic))
        {            
            var updateAtomic = new Atomic(name, newValue);
            if (existingAtomic.InitValue == expected)
            {
                success = atomics.TryUpdate(name, updateAtomic, existingAtomic);
            }
            if (success)
            {
                Console.WriteLine($"{name}: {existingAtomic.InitValue} -> {updateAtomic.InitValue} (exchanged)");

            }   
            else
            {
                Console.WriteLine($"{name}: {existingAtomic.InitValue} ‡ {expected} (not exchanged)");
            }    
        }   
        else
        {
            Console.WriteLine($"Counter '{name}' not found");
        }
    }

    public void List()
    {
        if (atomics.Count == 0)
        {
            Console.WriteLine("No counter");
        }    
        else
        {
            foreach (var item in atomics)
            {
                Console.WriteLine($"{item.Key} = {item.Value.InitValue}");
            }    
        }    
    }

    public void Get(string name)
    {
        if (atomics.TryGetValue(name, out Atomic existingAtomic))
        {
            Console.WriteLine($"{existingAtomic.Name} = {existingAtomic.InitValue}");
        }   
        else
        {
            Console.WriteLine($"Counter '{name}' not found");
        }    
    }

}

class Program
{
    static void Main()
    {
        Process process = new Process();

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

        for (int i = 0; i < N; i++)
        {
            string[] line = Console.ReadLine().Split(' ', 4, StringSplitOptions.RemoveEmptyEntries);
            if (line.Length == 0) return;

            switch (line[0])
            {
                case "CREATE":
                    {
                        if (line.Length == 3)
                        {
                            string name = line[1];
                            if (!int.TryParse(line[2], out int initValue)) return;

                            process.Create(name, initValue);
                            break;

                        }
                        else
                        {
                            return;
                        }
                    }
                case "INCREMENT":
                    {
                        if (line.Length == 2)
                        {
                            string name = line[1];
                            
                            process.Increment(name);
                            break;

                        }
                        else
                        {
                            return;
                        }
                    }
                case "DECREMENT":
                    {
                        if (line.Length == 2)
                        {
                            string name = line[1];

                            process.Decrement(name);
                            break;

                        }
                        else
                        {
                            return;
                        }
                    }
                case "ADD":
                    {
                        if (line.Length == 3)
                        {
                            string name = line[1];
                            if (!int.TryParse(line[2], out int value)) return;

                            process.Add(name, value);
                            break;

                        }
                        else
                        {
                            return;
                        }
                    }
                case "COMPARE_EXCHANGE":
                    {
                        if (line.Length == 4)
                        {
                            string name = line[1];
                            if (!int.TryParse(line[2], out int expected)) return;
                            if (!int.TryParse(line[3], out int newValue)) return;

                            process.CompareExchange(name, expected, newValue);
                            break;

                        }
                        else
                        {
                            return;
                        }
                    }
                case "LIST":
                    {
                        if (line.Length == 1)
                        {
                            process.List();
                            break;

                        }
                        else
                        {
                            return;
                        }
                    }
                case "GET":
                    {
                        if (line.Length == 2)
                        {
                            string name = line[1];
                            process.Get(name);
                            break;

                        }
                        else
                        {
                            return;
                        }
                    }

            }
        }    
    }
}