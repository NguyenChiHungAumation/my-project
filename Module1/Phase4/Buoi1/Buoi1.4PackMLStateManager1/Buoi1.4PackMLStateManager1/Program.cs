using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

class TimeProcess
{
    public double ResetTime { get; }
    public double StartTime { get; }
    public double CompleteTime { get; }
    public double HoldTime { get; }
    public double UnholdTime { get; }
    public double SuspendTime { get; }
    public double UnsuspendTime { get; }
    public double AbortTime { get; }
    public double ClearTime { get; }

    public TimeProcess (double resetTime, double startTime, double completeTime, double holdTime, double unholdTime, double suspendTime, double unsuspendTime, double abortTime, double clearTime)
    {
        ResetTime = resetTime;
        StartTime = startTime;
        CompleteTime = completeTime;
        HoldTime = holdTime;
        UnholdTime = unholdTime;
        SuspendTime = suspendTime;
        UnsuspendTime = unsuspendTime;
        AbortTime = abortTime;
        ClearTime = clearTime;
    }


}

enum StateMachine {STOPPED, RESETTING, IDLE,
    STARTING, EXECUTE, COMPLETING, COMPLETE,
    HOLDING, HELD,UNHOLDING, SUSPENDING,
    SUSPENED, UNSUSPENDING, ABORTING, ABORTED, CLEARING}

class Process
{
    public StateMachine CurrentState { get; private set; }

    public TimeProcess TimeProcess { get; private set; }

    public double TimeLine { get; private set; } = 0;
    public Process(TimeProcess time)
    {
        TimeProcess = time;

        CurrentState = StateMachine.STOPPED;
    }

    public void SC(string cmd)
    {
        if (CurrentState == StateMachine.STOPPED)
        {
            Console.WriteLine($"[{CurrentState}] {cmd} -> {StateMachine.RESETTING}");
            Console.WriteLine($"...acting {TimeProcess.ResetTime}ms -> {StateMachine.IDLE}");

            CurrentState = StateMachine.IDLE;
            TimeLine += TimeProcess.ResetTime;
        }
        else if (CurrentState == StateMachine.IDLE)
        {
            Console.WriteLine($"[{CurrentState}] {cmd} -> {StateMachine.STARTING}");
            Console.WriteLine($"...acting {TimeProcess.StartTime}ms -> {StateMachine.EXECUTE}");

            CurrentState = StateMachine.EXECUTE;
            TimeLine += TimeProcess.StartTime;
        }    
        else if (CurrentState == StateMachine.EXECUTE)
        {
            Console.WriteLine($"[{CurrentState}] {cmd} -> {StateMachine.COMPLETING}");
            Console.WriteLine($"...acting {TimeProcess.CompleteTime}ms -> {StateMachine.COMPLETE}");

            CurrentState = StateMachine.COMPLETE;
            TimeLine += TimeProcess.CompleteTime;
        }
        else if (CurrentState == StateMachine.COMPLETE)
        {
            Console.WriteLine($"[{CurrentState}] {cmd} -> {StateMachine.RESETTING}");
            Console.WriteLine($"...acting {TimeProcess.ResetTime}ms -> {StateMachine.IDLE}");

            CurrentState = StateMachine.IDLE;
            TimeLine += TimeProcess.ResetTime;
        }
        else
        {
            Invalid(cmd);
        }    
    }

    public void HoldCmd(string cmd)
    {
        if (CurrentState == StateMachine.EXECUTE)
        {
            Console.WriteLine($"[{CurrentState}] {cmd} -> {StateMachine.HOLDING}");
            Console.WriteLine($"...acting {TimeProcess.HoldTime}ms -> {StateMachine.HELD}");

            CurrentState = StateMachine.HELD;
            TimeLine += TimeProcess.HoldTime;
        }    
        else
        {
            Invalid(cmd);
        }    
    }
    public void UnholdCmd(string cmd)
    {
        if (CurrentState == StateMachine.HELD)
        {
            Console.WriteLine($"[{CurrentState}] {cmd} -> {StateMachine.UNHOLDING}");
            Console.WriteLine($"...acting {TimeProcess.UnholdTime}ms -> {StateMachine.EXECUTE}");

            CurrentState = StateMachine.EXECUTE;
            TimeLine += TimeProcess.UnholdTime;
        }
        else
        {
            Invalid(cmd);
        }
    }
    public void SuspenCmd(string cmd)
    {
        if (CurrentState == StateMachine.EXECUTE)
        {
            Console.WriteLine($"[{CurrentState}] {cmd} -> {StateMachine.SUSPENDING}");
            Console.WriteLine($"...acting {TimeProcess.SuspendTime}ms -> {StateMachine.SUSPENED}");

            CurrentState = StateMachine.SUSPENED;
            TimeLine += TimeProcess.SuspendTime;
        }
        else
        {
            Invalid(cmd);
        }
    }
    public void UnsuspendCmd(string cmd)
    {
        if (CurrentState == StateMachine.SUSPENED)
        {
            Console.WriteLine($"[{CurrentState}] {cmd} -> {StateMachine.UNSUSPENDING}");
            Console.WriteLine($"...acting {TimeProcess.UnsuspendTime}ms -> {StateMachine.EXECUTE}");

            CurrentState = StateMachine.EXECUTE;
            TimeLine += TimeProcess.UnsuspendTime;
        }
        else
        {
            Invalid(cmd);
        }
    }
    public void Abort(string cmd)
    {
        Console.WriteLine($"[{CurrentState}] {cmd} -> {StateMachine.ABORTING}");
        Console.WriteLine($"...acting {TimeProcess.AbortTime}ms -> {StateMachine.ABORTED}");

        CurrentState = StateMachine.ABORTED;
        TimeLine += TimeProcess.AbortTime;
    }
    public void ClearCmd(string cmd)
    {
        if (CurrentState == StateMachine.ABORTED)
        {
            Console.WriteLine($"[{CurrentState}] {cmd} -> {StateMachine.CLEARING}");
            Console.WriteLine($"...acting {TimeProcess.ClearTime}ms -> {StateMachine.STOPPED}");

            CurrentState = StateMachine.STOPPED;
            TimeLine += TimeProcess.ClearTime;
        }
        else
        {
            Invalid(cmd);
        }
    }

    public void Invalid(string cmd)
    {
        Console.WriteLine($"[{CurrentState}] {cmd} -> INVALID");
    }

    public void Final()
    {
        Console.WriteLine($"Final: {CurrentState}");
        Console.WriteLine($"TimeLine: {TimeLine}ms");
    }

}

class Program
{
    static void Main()
    {
        string[] line1 = Console.ReadLine().Split(' ', 9);
        if (line1.Length != 9) return;

        double resetTime = double.Parse(line1[0]);
        double startTime = double.Parse(line1[1]);
        double completeTime = double.Parse(line1[2]);
        double holdTime = double.Parse(line1[3]);
        double unholdTime = double.Parse(line1[4]);
        double suspendTime = double.Parse(line1[5]);
        double unsuspendTime = double.Parse(line1[6]);
        double abortTime = double.Parse(line1[7]);
        double clearTime = double.Parse(line1[8]);
        TimeProcess timeProcess = new TimeProcess
            (
            resetTime,
            startTime,
            completeTime,
            holdTime,
            unholdTime,
            suspendTime,
            unsuspendTime,
            abortTime,
            clearTime
            );
        Process process = new Process(timeProcess);

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
            string line2 = Console.ReadLine();
            if (line2 == null) return;

            switch (line2)
            {
                case "SC":
                    {
                        process.SC(line2); 
                        break;
                    }
                case "HoldCmd":
                    {
                        process.HoldCmd(line2);
                        break;
                    }
                case "UnholdCmd":
                    {
                        process.UnholdCmd(line2);
                        break;
                    }
                case "SuspendCmd":
                    {
                        process.SuspenCmd(line2);
                        break;
                    }
                case "UnsuspendCmd":
                    {
                        process.UnsuspendCmd(line2);
                        break;
                    }
                case "Abort":
                    {
                        process.Abort(line2);
                        break;
                    }
                case "ClearCmd":
                    {
                        process.ClearCmd(line2);
                        break;
                    }
            }    
        }

        process.Final();

    }
}


