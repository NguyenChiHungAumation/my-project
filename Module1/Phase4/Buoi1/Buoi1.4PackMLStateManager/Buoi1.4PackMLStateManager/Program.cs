using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;


class Process
{
    public double ResetTime { get; set; }
    public double StartTime { get; set; }
    public double CompleteTime { get; set; }
    public double HoldTime { get; set; }
    public double UnholdTime { get; set; }
    public double SuspendTime { get; set; } 
    public double UnsuspendTime { get; set; }
    public double AbortTime { get; set; }
    public double ClearTime { get; set; }

    public Process (double resetTime, double startTime, double completeTime, double holdTime, double unholdTime,
        double suspendTime, double unsuspendTime, double abortTime, double clearTime)
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