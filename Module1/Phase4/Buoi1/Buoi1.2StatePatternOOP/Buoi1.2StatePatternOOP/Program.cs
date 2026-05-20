using System;
using System.Collections.Generic;

namespace ConveyorStatePattern
{
    // 1. Định nghĩa các trạng thái hệ thống
    enum MachineState { IDLE, LOADING, RUNNING, FAULT }

    // 2. Interface cho State Pattern
    interface IConveyorState
    {
        void HandleStart(Process context);
        void HandleLoad(Process context, string id);
        void HandleProcess(Process context);
        void HandleClear(Process context);
        MachineState StateName { get; }
    }

    // 3. Lớp Context quản lý trạng thái hiện tại và dữ liệu
    class Process
    {
        public IConveyorState CurrentState { get; set; }
        public int Quota { get; private set; }
        public List<string> Ids { get; private set; } = new List<string>();
        public int OkCount { get; set; } = 0;
        public int FaultCount { get; set; } = 0;

        public Process(int quota)
        {
            Quota = quota;
            CurrentState = new IdleState();
        }

        public void Start() => CurrentState.HandleStart(this);
        public void Load(string id) => CurrentState.HandleLoad(this, id);
        public void ProcessItem() => CurrentState.HandleProcess(this);
        public void Clear() => CurrentState.HandleClear(this);

        public void LogIgnored(string cmd)
        {
            Console.WriteLine($"[{CurrentState.StateName}] {cmd} -> IGNORED");
        }

        public void PrintFinal()
        {
            Console.WriteLine($"Final: {CurrentState.StateName}, processed: {OkCount}, faults: {FaultCount}");
        }
    }

    // 4. Các lớp trạng thái cụ thể
    class IdleState : IConveyorState
    {
        public MachineState StateName => MachineState.IDLE;
        public void HandleStart(Process context)
        {
            Console.WriteLine($"[IDLE] START -> LOADING (quota: {context.Quota})");
            context.CurrentState = new LoadingState();
        }
        public void HandleLoad(Process context, string id) => context.LogIgnored($"LOAD {id}");
        public void HandleProcess(Process context) => context.LogIgnored("PROCESS");
        public void HandleClear(Process context) => context.LogIgnored("CLEAR");
    }

    class LoadingState : IConveyorState
    {
        public MachineState StateName => MachineState.LOADING;
        public void HandleLoad(Process context, string id)
        {
            context.Ids.Add(id);
            if (context.Ids.Count == context.Quota)
            {
                context.CurrentState = new RunningState();
                Console.WriteLine($"[LOADING] LOAD {id} -> full, switch RUNNING");
            }
            else
            {
                Console.WriteLine($"[LOADING] LOAD {id} -> buffered ({context.Ids.Count}/{context.Quota})");
            }
        }
        public void HandleStart(Process context) => context.LogIgnored("START");
        public void HandleProcess(Process context) => context.LogIgnored("PROCESS");
        public void HandleClear(Process context) => context.LogIgnored("CLEAR");
    }

    class RunningState : IConveyorState
    {
        public MachineState StateName => MachineState.RUNNING;
        public void HandleProcess(Process context)
        {
            if (context.Ids.Count == 0)
            {
                context.CurrentState = new IdleState();
                Console.WriteLine($"[RUNNING] PROCESS -> empty, switch IDLE");
                return;
            }

            string id = context.Ids[0];
            if (id.Contains("X"))
            {
                context.FaultCount++;
                context.CurrentState = new FaultState();
                Console.WriteLine($"[RUNNING] PROCESS {id} -> FAULT (defective: {id})");
            }
            else
            {
                context.OkCount++;
                context.Ids.RemoveAt(0);
                Console.WriteLine($"[RUNNING] PROCESS {id} -> OK");
            }
        }
        public void HandleStart(Process context) => context.LogIgnored("START");
        public void HandleLoad(Process context, string id) => context.LogIgnored($"LOAD {id}");
        public void HandleClear(Process context) => context.LogIgnored("CLEAR");
    }

    class FaultState : IConveyorState
    {
        public MachineState StateName => MachineState.FAULT;
        public void HandleClear(Process context)
        {
            string id = context.Ids[0];
            context.Ids.RemoveAt(0);
            context.CurrentState = new RunningState();
            Console.WriteLine($"[FAULT] CLEAR {id} -> RUNNING");
        }
        public void HandleStart(Process context) => context.LogIgnored("START");
        public void HandleLoad(Process context, string id) => context.LogIgnored($"LOAD {id}");
        public void HandleProcess(Process context) => context.LogIgnored("PROCESS");
    }

    // 5. Hàm Main xử lý luồng nhập xuất
    class Program
    {
        static void Main()
        {
            try
            {
                string line1 = Console.ReadLine();
                if (string.IsNullOrEmpty(line1)) return;
                int quota = int.Parse(line1);

                string line2 = Console.ReadLine();
                if (string.IsNullOrEmpty(line2)) return;
                int nCommands = int.Parse(line2);

                Process process = new Process(quota);

                for (int i = 0; i < nCommands; i++)
                {
                    string input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input)) continue;

                    string[] parts = input.Split(' ', 2);
                    string cmd = parts[0];

                    switch (cmd)
                    {
                        case "START":
                            process.Start();
                            break;
                        case "LOAD":
                            if (parts.Length > 1) process.Load(parts[1]);
                            break;
                        case "PROCESS":
                            process.ProcessItem();
                            break;
                        case "CLEAR":
                            process.Clear();
                            break;
                    }
                }
                process.PrintFinal();
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi nhập liệu cơ bản
                return;
            }
        }
    }
}