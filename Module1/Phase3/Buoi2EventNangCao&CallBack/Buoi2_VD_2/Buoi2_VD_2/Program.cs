using System;

class Inspect
{
    public void InspectPart(string partld, Action<bool> OnCompleted)
    {
        bool passed = true;
        OnCompleted?.Invoke(passed);
    }

    
}

class Program
{
    static void Main()
    { 
        Inspect inspect = new Inspect();
        inspect.InspectPart("P01", passed =>
        {
            Console.WriteLine(passed ? "PASS" : "FAIL");
        });
    }
}

