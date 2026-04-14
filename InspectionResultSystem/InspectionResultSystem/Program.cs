using System;
using System.Collections.Generic;

enum JudgeResult
{
    OK,
    NG,
    Uncertain

}

struct Measurement
{
    public string Name {  get; private set; }
    public double Value { get; private set; }
    public double Min {  get; private set; }
    public double Max { get; private set; }

    public Measurement(string name, double value, double min, double max)
    {
        Name = name;
        Value = value;
        Min = min;
        Max = max;
    }
    public JudgeResult Judge()
    {
        
        double valueA = 0;
        double valueB = 0;
        bool inRange = Value >= Min && Value <= Max;

        if (inRange == true)
        {
            return JudgeResult.OK;
        }   
        else
        {
            if (Value <= Min)
            {
                valueA = (Min - Value) / (Max - Min);
                if (valueA <= 0.1) return JudgeResult.Uncertain;
                else return JudgeResult.NG;
            }
            else
            {
                valueB = (Value - Max) / (Max - Min);
                valueB = Math.Round(valueB, 2);
                if(valueB <= 0.1) return JudgeResult.Uncertain;
                else return JudgeResult.NG;
            } 
        }    
    }
}

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N)) return;

        
        List<Measurement> measurements = new List<Measurement>();

        string[][] arr = new string[N][];
        for (int i = 0; i < N; i++)
        {
            arr[i] = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (arr[i].Length != 4) return;
            string name = arr[i][0];
            if (!double.TryParse(arr[i][1], out double value)) return;
            if (!double.TryParse(arr[i][2], out double min)) return;
            if (!double.TryParse(arr[i][3], out double max) || max == min) return;
            Measurement measurement = new Measurement(name, value, min, max);
            measurements.Add(measurement);

        } 
        int ok = 0;
        int ng = 0;
        int uncertain = 0;
        for (int i = 0;i < measurements.Count;i++)
        {
            if (measurements[i].Judge() == JudgeResult.OK) ok++;
            else if (measurements[i].Judge() == JudgeResult.NG) ng++;
            else if (measurements[i].Judge() == JudgeResult.Uncertain) uncertain++;

            Console.WriteLine($"{measurements[i].Name}: {measurements[i].Value:F2} [{measurements[i].Min:F2}-{measurements[i].Max:F2}] -> {measurements[i].Judge()}");
        }

        Console.WriteLine($"Summary: {ok} OK, {ng} NG, {uncertain} Uncertain");
        

    }

}