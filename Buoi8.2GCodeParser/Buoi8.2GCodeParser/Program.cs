using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

class Command
{
    public string Type { get; set; }
    public string Code { get; set; }
    public double? X { get; set; }
    public double? Y { get; set; }
    public double? Z { get; set; }

}

class SubProgram
{
    static double currentX = 0;
    static double currentY = 0;
    static double currentZ = 0;
    static double minX = 0;
    static double minY = 0;
    static double minZ = 0;
    static double maxX = 0;
    static double maxY = 0;
    static double maxZ = 0;
    static double totalDistance = 0;
    public static void MoveTo(double? x, double? y, double? z)
    {
        double newX = x ?? currentX;
        double newY = y ?? currentY;
        double newZ = z ?? currentZ;

        double distance = Math.Sqrt(
            Math.Pow(newX -currentX, 2) +
            Math.Pow(newY - currentY, 2) +
            Math.Pow(newZ - currentZ, 2)
        );

        

        totalDistance += distance;
        currentX = newX;
        currentY = newY;
        currentZ = newZ;

        minX = Math.Min(minX, currentX);
        maxX = Math.Max(maxX, currentX);

        minY = Math.Min(minY, currentY);
        maxY = Math.Max(maxY, currentY);

        minZ = Math.Min(minZ, currentZ);
        maxZ = Math.Max(maxZ, currentZ);
        Console.WriteLine($"Moved to X={currentX:F2} Y={currentY:F2} Z={currentZ:F2}");
    }

    public static void Distance()
    {
        Console.WriteLine($"Total distance: {totalDistance:F2}");
    }
    public static void Bounds()
    {
        Console.WriteLine($"X: [{minX:F2}, {maxX:F2}] Y: [{minY:F2}, {maxY:F2}] Z: [{minZ:F2}, {maxZ:F2}]");
    }
    public static void Position()
    {
        Console.WriteLine($"Position: X={currentX:F2} Y={currentY:F2} Z={currentZ:F2}");
    }

}



class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        string movePatternInput = @"^\s*MOVE\s+(\w+)(?:\s+X(-?\d+(?:\.\d+)?))?(?:\s+Y(-?\d+(?:\.\d+)?))?(?:\s+Z(-?\d+(?:\.\d+)?))?$";
        string distancePatternInput = @"^\s*DISTANCE\s*$";
        string boundsPatternInput = @"^\s*BOUNDS\s*$";
        string positionPatternInput = @"^\s*POSITION\s*$";


        string SoLuongInput = @"^\s*[1-9]\d*\s*$";

        string s = Console.ReadLine();
        Match p = Regex.Match(s, SoLuongInput);
        int N = 0;
        if (p.Success)
        {
            N = int.Parse(s);
        }
        else
        {
            return;
        }
        List<Command> commands = new List<Command>();
        for (int i = 0; i < N; i++)
        {
            string line = Console.ReadLine() ;
            Match moveMatch = Regex.Match(line, movePatternInput);
            if (moveMatch.Success)
            {
                Command cmd = new Command
                {
                    Type = "MOVE",
                    Code = moveMatch.Groups[1].Value,
                    X = moveMatch.Groups[2].Success ? double.Parse(moveMatch.Groups[2].Value) : null,
                    Y = moveMatch.Groups[3].Success ? double.Parse(moveMatch.Groups[3].Value) : null,
                    Z = moveMatch.Groups[4].Success ? double.Parse(moveMatch.Groups[4].Value) : null

                };

                commands.Add(cmd);
                continue;
            }
            
            else if (Regex.IsMatch(line, distancePatternInput))
            {
                Command cmd = new Command { Type = "DISTANCE" };
                commands.Add(cmd);
                continue;
            }
            else if (Regex.IsMatch(line, boundsPatternInput))
            {
                Command cmd = new Command { Type = "BOUNDS" };
                commands.Add(cmd);
                continue;
            }
            else if (Regex.IsMatch(line, positionPatternInput))
            {
                Command cmd = new Command { Type = "POSITION" };
                commands.Add(cmd);
                continue;
            }

            else
            {
                return;
            }    

        }
        
        for (int i = 0; i < N; i++)
        {
            var input = commands[i];
            switch(input.Type)
            {
                case "MOVE":
                    {
                        SubProgram.MoveTo(input.X, input.Y, input.Z);
                        break;
                    }
                case "DISTANCE":
                    {
                        SubProgram.Distance();
                        break;
                    }
                case "BOUNDS":
                    {
                        SubProgram.Bounds();
                        break;
                    }
                case "POSITION":
                    {
                        SubProgram.Position();
                        break;
                    }
                    default:
                    {
                        break;
                    }
            }
        }    
            
    }
}