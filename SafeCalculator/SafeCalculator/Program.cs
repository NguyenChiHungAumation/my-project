using System;
using System.Text;

class SafeCalculator
{
    public static double Calculate(int a, char op, int b)
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
                    return checked((double)a * b);
                }
            case '/':
                {
                    if (b == 0)
                    {
                        throw new DivideByZeroException("Không thể chia cho không");
                    } 
                    else
                    {
                        return checked((double)a / b);
                    } 
                    
                }
            default:
                {
                    throw new InvalidOperationException("toán tử không hợp lệ");
                }


        }    
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        try
        {
            Console.Write("Nhập a: ");
            int a = int.Parse(Console.ReadLine());

            Console.Write("Nhập op: ");
            string opInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(opInput) || opInput.Length != 1)
            {
                throw new InvalidOperationException("Toán tử không hợp lệ");
            }

            char op = opInput[0];

            Console.Write("Nhập b: ");
            int b = int.Parse(Console.ReadLine());

            double result = SafeCalculator.Calculate(a, op, b);
            Console.WriteLine($"Kết quả: {result}");
                
        }

        catch (FormatException ex)
        {
            Console.WriteLine("Lỗi: nhập sai số ");
        }
        catch (OverflowException ex)
        {
            Console.WriteLine("Lỗi: số quá lớn gây Overload");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Lỗi: {ex.Message}");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"Lỗi: {ex.Message}");
        }
    }
}