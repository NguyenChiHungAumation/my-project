using System;
using System.Collections.Generic;

enum MachineState
{
    Idle = 0,
    Initializing = 1,
    Running = 2,
    Paused = 3,
    Error = 4,
    Stopped = 5,

}

class Machine
{
    public string Name { get; set; }
    public MachineState State {  get; set; }
}

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N)) return;

        string[][] arr = new string[N][];
        for (int i = 0; i < N; i++)
        {
            arr[i] = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        }   
        List<Machine> machines = new List<Machine>();
        
        for (int i = 0;i < arr.Length;i++)
        {
            if (arr[i][0] == "CREATE" && arr[i].Length == 2)
            {
                var device = new Machine();
                device.Name = arr[i][1];
                MachineState state = MachineState.Idle;
                device.State = state;
                machines.Add(device);
                Console.WriteLine($"Created machine '{device.Name}'");
            }

            else if (arr[i][0] == "INIT" && arr[i].Length == 2)
            {
                for (int j = 0; j < machines.Count; j++)
                {
                    if (arr[i][1] == machines[j].Name)
                    {
                        if (machines[j].State == MachineState.Idle)
                            
                        {


                            MachineState oldstate = machines[j].State;
                            MachineState state = MachineState.Initializing;
                            machines[j].State = state;
                            Console.WriteLine($"{machines[j].Name}: {oldstate} -> {machines[j].State}");
                            break;
                        }

                        else
                        {
                            Console.WriteLine($"ERROR: Cannot {arr[i][0]} from {machines[j].State}");
                            break ;
                        }    
                    }
                    else continue;
                }
               
            }


            else if (arr[i][0] == "STOP" && arr[i].Length == 2)
            {
                for (int j = 0; j < machines.Count; j++)
                {
                    if (arr[i][1] == machines[j].Name)
                    {
                        if (machines[j].State == MachineState.Idle ||
                            machines[j].State == MachineState.Running ||
                            machines[j].State == MachineState.Paused)
                        {

                            MachineState oldstate = machines[j].State;
                            MachineState state = MachineState.Stopped;
                            machines [j].State = state;
                            Console.WriteLine($"{machines[j].Name}: {oldstate} -> {machines[j].State}");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"ERROR: Cannot {arr[i][0]} from {machines[j].State}");
                            break ;
                        }
                    }
                    else continue;
                }

            }

            else if (arr[i][0] == "RUN" && arr[i].Length == 2)
            {
                for (int j = 0; j < machines.Count; j++)
                {
                    if (arr[i][1] == machines[j].Name)
                    {
                        if (machines[j].State == MachineState.Initializing)
                        {

                            MachineState oldstate = machines[j].State;
                            MachineState state = MachineState.Running;
                            machines[j].State = state;
                            Console.WriteLine($"{machines[j].Name}: {oldstate} -> {machines[j].State}");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"ERROR: Cannot {arr[i][0]} from {machines[j].State}");
                            break;
                        }
                    }
                    else continue;
                }

            }

            else if (arr[i][0] == "PAUSE" && arr[i].Length == 2)
            {
                for (int j = 0; j < machines.Count; j++)
                {
                    if (arr[i][1] == machines[j].Name)
                    {
                        if (machines[j].State == MachineState.Running)
                        {

                            MachineState oldstate = machines[j].State;
                            MachineState state = MachineState.Paused;
                            machines[j].State = state;
                            Console.WriteLine($"{machines[j].Name}: {oldstate} -> {machines[j].State}");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"ERROR: Cannot {arr[i][0]} from {machines[j].State}");
                            break;
                        }
                    }
                    else continue;
                }

            }


            else if (arr[i][0] == "ERROR" && arr[i].Length == 2)
            {
                for (int j = 0; j < machines.Count; j++)
                {
                    if (arr[i][1] == machines[j].Name)
                    {
                        if (machines[j].State == MachineState.Initializing ||
                            machines[j].State == MachineState.Running)
                        {

                            MachineState oldstate = machines[j].State;
                            MachineState state = MachineState.Error;
                            machines[j].State = state;
                            Console.WriteLine($"{machines[j].Name}: {oldstate} -> {machines[j].State}");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"ERROR: Cannot {arr[i][0]} from {machines[j].State}");
                            break;
                        }
                    }
                    else continue;
                }

            }


            else if (arr[i][0] == "RESET" && arr[i].Length == 2)
            {
                for (int j = 0; j < machines.Count; j++)
                {
                    if (arr[i][1] == machines[j].Name)
                    {
                        if (machines[j].State == MachineState.Error||
                            machines[j].State == MachineState.Stopped)
                        {

                            MachineState oldstate = machines[j].State;
                            MachineState state = MachineState.Idle;
                            machines[j].State = state;
                            Console.WriteLine($"{machines[j].Name}: {oldstate} -> {machines[j].State}");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"ERROR: Cannot {arr[i][0]} from {machines[j].State}");
                            break;
                        }
                    }
                    else continue;
                }

            }



            else if (arr[i][0] == "RESUME" && arr[i].Length == 2)
            {
                for (int j = 0; j < machines.Count; j++)
                {
                    if (arr[i][1] == machines[j].Name)
                    {
                        if (machines[j].State == MachineState.Paused)
                        {

                            MachineState oldstate = machines[j].State;
                            MachineState state = MachineState.Running;
                            machines[j].State = state;
                            Console.WriteLine($"{machines[j].Name}: {oldstate} -> {machines[j].State}");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"ERROR: Cannot {arr[i][0]} from {machines[j].State}");
                            break;
                        }
                    }
                    else continue;
                }



            }


            else if (arr[i][0] == "STATE" && arr[i].Length == 2)
            {
                for (int j = 0; j < machines.Count; j++)
                {
                    if (arr[i][1] == machines[j].Name)
                    {
                        Console.WriteLine($"{machines[j].Name}: {machines[j].State}");
                        break;
                    }
                    else continue;
                }    
            }    

        }    
    }
}