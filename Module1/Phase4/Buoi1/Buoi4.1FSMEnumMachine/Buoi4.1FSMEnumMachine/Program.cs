using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

enum MachineState
{
    IDLE,
    RUNNING,
    PAUSED,
    ERROR,
    STOPPED
}

class Process
{
    public MachineState currentState = MachineState.IDLE;

    public void StartCommand(string commant)
    {
        if (currentState == MachineState.IDLE)
        {
            MachineState oldState = currentState;
            currentState = MachineState.RUNNING;

            Console.WriteLine($"{oldState} --{commant}--> {currentState}");
        }   
        else
        {
            Console.WriteLine($"{currentState} --{commant}--> INVALID (stay {currentState})");
        }    
    }

    public void PauseCommand(string commant)
    {
        if (currentState == MachineState.RUNNING)
        {
            MachineState oldState = currentState;
            currentState = MachineState.PAUSED;

            Console.WriteLine($"{oldState} --{commant}--> {currentState}");
        }
        else
        {
            Console.WriteLine($"{currentState} --{commant}--> INVALID (stay {currentState})");
        }
    }

    public void ResumeCommand(string commant)
    {
        if (currentState == MachineState.PAUSED)
        {
            MachineState oldState = currentState;
            currentState = MachineState.RUNNING;

            Console.WriteLine($"{oldState} --{commant}--> {currentState}");
        }
        else
        {
            Console.WriteLine($"{currentState} --{commant}--> INVALID (stay {currentState})");
        }
    }

    public void StopCommand(string commant)
    {
        if (currentState == MachineState.RUNNING)
        {
            MachineState oldState = currentState;
            currentState = MachineState.STOPPED;

            Console.WriteLine($"{oldState} --{commant}--> {currentState}");
        }
        else if (currentState == MachineState.PAUSED)
        {
            MachineState oldState = currentState;
            currentState = MachineState.STOPPED;

            Console.WriteLine($"{oldState} --{commant}--> {currentState}");
        }    
        else
        {
            Console.WriteLine($"{currentState} --{commant}--> INVALID (stay {currentState})");
        }
    }

    public void ErrorCommand(string commant)
    {
        if (currentState == MachineState.RUNNING)
        {
            MachineState oldState = currentState;
            currentState = MachineState.ERROR;

            Console.WriteLine($"{oldState} --{commant}--> {currentState}");
        }        
        else
        {
            Console.WriteLine($"{currentState} --{commant}--> INVALID (stay {currentState})");
        }
    }

    public void ResetCommand(string commant)
    {
        if (currentState == MachineState.ERROR)
        {
            MachineState oldState = currentState;
            currentState = MachineState.IDLE;

            Console.WriteLine($"{oldState} --{commant}--> {currentState}");
        }
        else if (currentState == MachineState.STOPPED)
        {
            MachineState oldState = currentState;
            currentState = MachineState.IDLE;

            Console.WriteLine($"{oldState} --{commant}--> {currentState}");
        }
        else
        {
            Console.WriteLine($"{currentState} --{commant}--> INVALID (stay {currentState})");
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
            N = int.Parse(Console.ReadLine()); // Nhập số Frame 
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
            string[] line = Console.ReadLine().Split(' ', 1, StringSplitOptions.RemoveEmptyEntries);

            if (line.Length != 1) return;

            switch (line[0])
            {
                case "START":
                    {

                        process.StartCommand(line[0]);
                        break;
                    }
                case "PAUSE":
                    {
                        process.PauseCommand(line[0]);
                        break;
                    }
                case "RESUME":
                    {
                        process.ResumeCommand(line[0]);
                        break;
                    }
                case "STOP":
                    {
                        process.StopCommand(line[0]);
                        break;
                    }
                case "ERROR":
                    {
                        process.ErrorCommand(line[0]);
                        break;
                    }
                case "RESET":
                    {
                        process.ResetCommand(line[0]);
                        break;
                    }

            }
                        
        }

        Console.WriteLine($"Final state: {process.currentState}");
    }

    
}

