using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int N;
        try
        {
            N = int.Parse(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Invalid format");
            return;
        }
        catch (OverflowException)
        {
            Console.WriteLine("Error: Over float");
            return;
        }

        string[] line = new string[N];

        for (int i = 0; i < N; i++)
        {
            line[i] = Console.ReadLine();
        }

        string[][] parts = new string[N][];
        try
        {
            for (int i = 0; i < N; i++)
            {

                parts[i] = line[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts[i].Length != 1 && parts[i].Length != 2)
                {
                    throw new FormatException();
                }

            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Invalid format");
        }

        Dictionary<string, string> keys = new Dictionary<string, string>();
        for (int i = 0; i < N; i++)
        {
            try
            {
                switch (parts[i][0])
                {
                    case "LOAD":
                        {
                            if (parts[i].Length == 2)
                            {
                                string lines = parts[i][1];
                                string[] load = lines.Split('=', 2);
                                if (load.Length != 2)
                                    throw new FormatException($"Invalid format '{lines}'");
                                string key = load[0];
                                string value = load[1];
                                if (key == "")
                                    throw new FormatException("Emty key");
                                
                                if (keys.ContainsKey(key))
                                    throw new FormatException($"Duplicate key '{key}'");

                                keys.Add(key, value);
                                Console.WriteLine($"Loaded: {key} = {value}");

                                break;

                            }
                            else
                                throw new FormatException($"Invalid format");
                        }
                    case "GET":
                        {
                            if (parts[i].Length == 2)
                            {
                                if (keys.ContainsKey(parts[i][1]))
                                {
                                    string key = parts[i][1];
                                    string value = keys[parts[i][1]];

                                    Console.WriteLine($"{key} = {value}");
                                    break;
                                }
                                else
                                    throw new FormatException($"Key '{parts[i][1]}' not found");

                            }
                            else
                                throw new FormatException("Invalid format");
                        }
                    case "LIST":
                        {
                            if (parts[i].Length == 1)
                            {
                                if (keys.Count != 0)
                                {
                                    var result = keys.OrderBy(p => p.Key);
                                    foreach (var item in result)
                                    {
                                        Console.WriteLine($"{item.Key} = {item.Value}");
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("No config");
                                    break;
                                }
                            }
                            else
                                throw new FormatException("Invalid format");
                        }
                    case "VALIDATE":
                        {
                            if (parts[i].Length == 1)
                            {
                                bool host = false;
                                bool port = false;
                                bool timeout = false;
                                if (keys.ContainsKey("host")) host = true;
                                if (keys.ContainsKey("port")) port = true;
                                if (keys.ContainsKey("timeout")) timeout = true;
                                if (host == true && port == true && timeout == true)
                                {
                                    Console.WriteLine("OK");
                                    break;
                                }
                                else
                                {
                                    Console.Write("Missing: ");
                                    if (host == false && port == false && timeout == false)
                                    {
                                        Console.WriteLine("host, port, timeout");
                                        break;
                                    }
                                    else if (port == false && host == false)
                                    {
                                        Console.WriteLine("host, port");
                                        break;
                                    }
                                    else if (timeout == false && port == false)
                                    {
                                        Console.WriteLine("port, timeout");
                                        break;
                                    }
                                    else if (timeout == false && host == false)
                                    {
                                        Console.WriteLine("host,timeout");
                                        break;
                                    }
                                    else if (host == false)
                                    {
                                        Console.WriteLine("host");
                                        break;
                                    }
                                    else if (port == false)
                                    {
                                        Console.WriteLine("port");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("timeout");
                                        break;
                                    }

                                }

                            }
                            else
                                throw new FormatException("Invalid format");
                        }
                    default:
                        {
                            throw new FormatException("Invalid format");
                        }

                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"ConfigException: {ex.Message}");
            }
        }


    }
}