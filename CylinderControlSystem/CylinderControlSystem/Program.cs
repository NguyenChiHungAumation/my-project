using System;

enum CylinderState { Retracted, Extending, Extended, Retracting}
class Cylinder
{
    public string Name { get; private set; }
    public CylinderState State { get; private set; } = CylinderState.Retracted;
    public Cylinder(string name)
    {
        Name = name;
    }
    public void Extend()
    {
        if (State == CylinderState.Retracted)
        {
            State = CylinderState.Extending;
            
        }
        else
            Console.WriteLine($"ERROR: {Name} cannot extend from {State}");
    }

    public void Retract()
    {
        if (State == CylinderState.Extended)
        {
            State = CylinderState.Retracting;
        }
        else
            Console.WriteLine($"ERROR: {Name} cannot retract from {State}");
    }
    
    public void Update()
    {
        if (State == CylinderState.Extending)
        {
            State = CylinderState.Extended;
        }
        else if (State == CylinderState.Retracting)
        {
            State = CylinderState.Retracted;
        }
        else
            Console.WriteLine($"{Name}: no transition");
    }






}

class Program
{
    static void Main()
    {
        //Nhập dòng 1
        string input = Console.ReadLine();
        if (!int.TryParse(input, out int N) || N <= 0)
            return;

        //Nhập dòng 2
        string[][] part = new string[N][];
        for (int i = 0; i < N; i++)
        {
            part[i] = new string[10];
        }

        for (int i = 0; i < N; i++)
        {
            string s = Console.ReadLine(); //Nhập chuỗi s
            part[i] = s.Split(' ', StringSplitOptions.RemoveEmptyEntries); //tách chuỗi s thành mảng part
        }

        var cylinder = new Cylinder[N];
        int count = 0;

        for(int i = 0;i < N; i++)
        {
            if (part[i][0] == "CREATE")
            {
                int countCreate = part[i].Length;
                if (countCreate == 2)
                {
                    cylinder[count] = new Cylinder(part[i][1]);
                    Console.WriteLine($"Cylinder '{cylinder[count].Name}' created");
                    count++;
                }
                else
                    return;
                continue;
            }
            
            
            else if (part[i][0] == "EXTEND")
            {
                int countExtend = part[i].Length;
                if (countExtend == 2)
                {
                    for (int j = 0; j < count; j++)
                    {
                        if (part[i][1] == cylinder[j].Name)
                        {
                            CylinderState old = cylinder[j].State;
                            cylinder[j].Extend();
                           if (old != cylinder[j].State && cylinder[j].State == CylinderState.Extending)
                           {
                                Console.WriteLine($"{cylinder[j].Name}: extending");
                           }
                        }
                        else
                            continue;
                        break;
                    }

                }
                else
                    return;
                continue;
            }
            
            else if (part[i][0] == "RETRACT")
            {
                int countRetract = part[i].Length;
                if (countRetract == 2)
                {
                    for (int j = 0; j < count; j++)
                    {
                        if (part[i][1] == cylinder[j].Name)
                        {
                            cylinder[j].Retract();
                            if (cylinder[j].State == CylinderState.Retracting)
                            {
                                Console.WriteLine($"{cylinder[j].Name}: retracting");
                            }
                        }
                        else
                            continue;
                        break;
                    }
                }
                else
                    return;
                continue ;
            }
            
            else if (part[i][0] == "UPDATE")
            {
                int countUpdate = part[i].Length;
                if (countUpdate == 2)
                {
                    for (int j = 0; j < count; j++)
                    {
                        if (part[i][1] == cylinder[j].Name)
                        {
                            CylinderState oldState = cylinder[j].State;
                            cylinder[j].Update();
                            if (oldState == CylinderState.Extending && cylinder[j].State == CylinderState.Extended)
                            {
                                Console.WriteLine($"{cylinder[j].Name}: extended");
                            }
                            else if (oldState == CylinderState.Retracting && cylinder[j].State == CylinderState.Retracted)
                            {
                                Console.WriteLine($"{cylinder[j].Name}: retracted");
                            }    
                        }
                        else
                            continue;
                        break;
                    }
                }
                else
                    return;
                continue;

            }
            
            else if (part[i][0] == "STATUS")
            {
                int countStatus = part[i].Length;
                if (countStatus == 2)
                {
                    for (int j = 0; j < count; j++)
                    {
                        if (part[i][1] == cylinder[j].Name)
                        {
                            Console.WriteLine($"{cylinder[j].Name}: {cylinder[j].State}");
                        }
                        else
                            continue;
                        break ;
                    }

                }
                else
                    return;
                continue;
            }    







        }    














    }

}