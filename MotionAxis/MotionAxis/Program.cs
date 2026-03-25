using System;
using System.Linq;

class MotionAxis
{
    public string Name { get; private set; }
    public double Position { get; private set; } = 0;
    public double Speed { get; private set; } = 100;
    public bool IsHome { get; private set; } = false;

    public MotionAxis(string name)
    {
        Name = name;
    }

    public void MoveAbsolute(double position)
    {
        Position = position;
        IsHome = false;
    }

    public void HomeAxis()
    {
        Position = 0;
        IsHome = true;
    }

    public void SetSpeed(double speed)
    {
        Speed = speed;
    }
}

class Program
{
    static void Main()
    {
        string input = Console.ReadLine();
        if (!int.TryParse(input, out int soN) || soN <= 0)
            return;

        MotionAxis[] axis = new MotionAxis[1000];
        int count = 0;

        for (int i = 0; i < soN; i++)
        {
            string s = Console.ReadLine();
            string[] part = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (part[0] == "CREATE")
            {
                if (part.Length != 2) return;

                string name = part[1];
                axis[count] = new MotionAxis(name);
                Console.WriteLine($"Created axis '{axis[count].Name}'");
                count++;
            }
            else if (part[0] == "MOVE")
            {
                if (part.Length != 3) return;

                for (int a = 0; a < count; a++)
                {
                    if (part[1] == axis[a].Name)
                    {
                        axis[a].MoveAbsolute(double.Parse(part[2]));
                        Console.WriteLine($"{axis[a].Name}: moved to {axis[a].Position:F2}");
                        break;
                    }
                }
            }
            else if (part[0] == "SPEED")
            {
                if (part.Length != 3) return;

                for (int a = 0; a < count; a++)
                {
                    if (part[1] == axis[a].Name)
                    {
                        axis[a].SetSpeed(double.Parse(part[2]));
                        Console.WriteLine($"{axis[a].Name}: speed set to {axis[a].Speed:F2}");
                        break;
                    }
                }
            }
            else if (part[0] == "STATUS")
            {
                if (part.Length != 2) return;

                for (int a = 0; a < count; a++)
                {
                    if (part[1] == axis[a].Name)
                    {
                        Console.WriteLine($"{axis[a].Name}: Position={axis[a].Position:F2} Speed={axis[a].Speed:F2} IsHome={axis[a].IsHome}");
                        break;
                    }
                }
            }
            else if (part[0] == "HOME")
            {
                if (part.Length != 2) return;

                for (int a = 0; a < count; a++)
                {
                    if (part[1] == axis[a].Name)
                    {
                        axis[a].HomeAxis();
                        Console.WriteLine($"{axis[a].Name}: homed");
                        break;
                    }
                }
            }
        }
    }
}