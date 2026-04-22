using System;
using System.Collections.Generic;

class SafeCalculator
{
    public static double Calculator(int a, char op, int b)
    {
        switch (op)
        {
            case '+':
                {
                    return checked(a + b);
                }
            case '-':
                {
                    return checked(a - b);
                }
            case '*':
                {
                    return checked(a * b);
                }
            case '/':
                {
                    if (b == 0)
                    {
                        throw new DivideByZeroException("Division by zero");
                    }    
                    return checked((double)a / b);

                }
            default:
                {
                    throw new InvalidOperationException($"Unknown operator '{op}'");
                }

        }    
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
        }
        catch (OverflowException)
        {
            Console.WriteLine("Error: Overflow");
            return;
        }
        catch(FormatException)
        {
            Console.WriteLine("Error: Invalid number format");
            return;
        }
        
        string[] arr = new string[N];
        
        for (int i = 0; i < N; i++)
        {
            arr[i] = Console.ReadLine();
        }

        for (int i = 0; i < N; i++)
        {
            try
            {
                string[] parts = arr[i].Split(' ');
                if (parts.Length != 3)
                    throw new FormatException();


                int a = int.Parse(parts[0]);
                char op = char.Parse(parts[1]);
                int b = int.Parse(parts[2]);

                double result = SafeCalculator.Calculator(a, op, b);
                if (op != '/')
                Console.WriteLine($"{a} {op} {b} = {result}");
                else
                    Console.WriteLine($"{a} {op} {b} = {result:F2}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            catch (OverflowException)
            {
                Console.WriteLine("Error: Overflow");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Invalid number format");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }    

    }
}