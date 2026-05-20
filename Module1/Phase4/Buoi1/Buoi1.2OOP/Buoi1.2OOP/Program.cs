using System;
using System.Collections.Generic;

class TransitionRule
{
    public string FromState {  get; }
    public string Event { get; }
    public string ToState { get; }
    public string Action { get; }

    public TransitionRule(string fromState, string @event, string toState, string action)
    {
        FromState = fromState;
        Event = @event;
        ToState = toState;
        Action = action;
    }
}

class StateMachine
{
    private string CurrentState;

    public int Total = 0;
    public int Success = 0;

    private List<TransitionRule> rules = new List<TransitionRule>();

    public StateMachine(string currentState)
    {
        CurrentState = currentState;


    }

    public void AddRule(TransitionRule current)
    {
        rules.Add(current);
    }

    public void FindRuler(string cmd)
    {
        Total++;
        bool isTrue = false;

        foreach (var item in rules)
        {
            if (CurrentState == item.FromState && cmd == item.Event)
            {
                Console.WriteLine($"{CurrentState} + {cmd} => {item.ToState} [{item.Action}]");

                CurrentState = item.ToState;
                isTrue = true;
                Success++;
                break;
            }    
        } 
        if (isTrue == false)
        {
            Console.WriteLine($"{CurrentState} + {cmd} => NO TRANSITION");
        }    
    }

    public void Transitions()
    {
        Console.WriteLine($"Transitions: {Success}/{Total}");

        var newRulers = rules
            .Select(p => p.FromState)
            .Distinct()
            .ToList()
            .OrderBy(p => p);
        string line = string.Join(", ", newRulers);
        
        Console.WriteLine($"States visited: {line}");

        Console.WriteLine($"Final state: {CurrentState}");
    }

}

class Program
{
    
    static void Main()
    {
        StateMachine stateMachine;

        string s = Console.ReadLine();
        stateMachine = new StateMachine(s);

        int T;
        try
        {
            T = int.Parse(Console.ReadLine());
            if (T < 0)
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

        for (int i = 0; i < T; i++)
        {
            string[] line = Console.ReadLine().Split(' ', 4);
            if (line.Length != 4) return;

            TransitionRule rule = new TransitionRule(line[0], line[1], line[2], line[3]);

            stateMachine.AddRule(rule);
            
        }

        int E;
        try
        {
            E = int.Parse(Console.ReadLine());
            if (E < 0)
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

        for (int i = 0; i < E; i++)
        {
            string line = Console.ReadLine();

            stateMachine.FindRuler(line);
        }

        stateMachine.Transitions();
    }
}