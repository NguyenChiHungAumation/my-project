using System;
using System.Security.Cryptography.X509Certificates;

class Axis
{
    public static double Pos {  get; private set; }
    public static double Dist { get; private set; }
    public static double MoveX { get; private set; } = 0;
    public static double MoveY { get; private set; } = 0;
    public static double MoveZ { get; private set; } = 0;
    

    public static void MOVEX(double moveX)
    {
        
        MoveX += moveX;
        Dist += Math.Abs(moveX);
    }

    public static void MOVEY(double moveY)
    {
        MoveY += moveY;
        Dist += Math.Abs(moveY);
    }

    public static void MOVEZ(double moveZ)
    {
        MoveZ += moveZ;
        Dist += Math.Abs(moveZ);
    }

    public static void HOME()
    {
        MoveX = 0;
        MoveY = 0;
        MoveZ = 0;
        //Console.WriteLine($"X={MoveX.ToString("F1")} Y={MoveY.ToString("F1")} Z={MoveZ.ToString("F1")}");
    }


    public static void POS()
    {
        Console.WriteLine($"X={MoveX.ToString("F1")} Y={MoveY.ToString("F1")} Z={MoveZ.ToString("F1")}");
    }

    
}

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N) || N < 1 || N > 500) return;

        string[][] arr = new string[N][];

        for (int i = 0; i < N; i++)
        {
            string a = Console.ReadLine();
            arr[i] = a.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }   
        
        for (int i = 0; i < N; i++)
        {
            if (arr[i][0] == "MOVE" && arr[i].Length == 3)
            {
                if ((arr[i][1] != "X" && arr[i][1] != "Y" && arr[i][1] != "Z") || !double.TryParse(arr[i][2], out double p)) return;
                if (arr[i][1] == "X")
                {
                    Axis.MOVEX(p);
                    
                    continue;
                }

                else if (arr[i][1] == "Y")
                {
                    Axis.MOVEY(p);
                    
                    continue;
                }
                else if (arr[i][1] == "Z")
                {
                    Axis.MOVEZ(p);
                    
                    continue;
                }
                continue;
            } 
            
            else if (arr[i][0] == "HOME" && arr[i].Length == 1)
            {
                Axis.HOME();
                continue;
            }

            else if (arr[i][0] == "POS" && arr[i].Length == 1)
            {
                Axis.POS();
                continue;
            }

            else if (arr[i][0] == "DIST" && arr[i].Length == 1)
            {
                
                Console.WriteLine($"DIST={Axis.Dist.ToString("F1")}");
                continue;
            }
        }    
    }
}