using System;
using System.Collections.Generic;

class Protocol
{
    public string Name { get; set; }
    public bool IsConnected { get; set; } = false;

    public virtual void Connect()
    {

        IsConnected = true;
        Console.WriteLine($"[{Name}] Connected");
    }

    public virtual void Disconnect()
    {

        IsConnected = false;
        Console.WriteLine($"[{Name}] Disconnected");
    }

    public virtual void Send(string msg)
    {
        if (IsConnected == true)
            Console.WriteLine($"[{Name}] Sent: {msg}");
        else
            Console.WriteLine($"[{Name}] ERROR: Not connected");

    }
}

class SerialProtocol : Protocol
{
    public int BaudRate { get; set; }

    public override void Connect()
    {
        base.Connect();
        Console.WriteLine($"[{Name}] BaudRate={BaudRate}");
    }

    public override void Send(string msg)
    {
        base.Send(msg);
        if (IsConnected == true)
        Console.WriteLine($"[{Name}] Frame: STX|{msg}|ETX");
    }
}

class TcpProtocol : Protocol
{
    public string IP { set; get; }
    public int Port { get; set; }

    public override void Connect()
    {
        base.Connect();
        Console.WriteLine($"[{Name}] Endpoint={IP}:{Port}");
    }

    public override void Send(string msg)
    {
        base.Send(msg);
        if (IsConnected == true)
        Console.WriteLine($"[{Name}] Packet:[{msg.Length}] {msg}");
    }
}

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if(!int.TryParse(s, out int N) || N < 0) return;

        string[][] arr = new string[N][];

        for (int i = 0; i < N; i++)
        {
            string input = Console.ReadLine();
            arr[i] = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        List<Protocol> protocols = new List<Protocol>();
        for (int i = 0; i < N; i++)
        {
            if (arr[i][0] == "CREATE")
            {
                if (arr[i][1] == "SERIAL" && arr[i].Length == 4)
                {
                    SerialProtocol serial = new SerialProtocol();
                    serial.Name = arr[i][2];
                    if (!int.TryParse(arr[i][3], out int baudRate)) return;
                    serial.BaudRate = baudRate;
                    protocols.Add(serial);
                    Console.WriteLine($"Created Serial '{serial.Name}'");
                    continue;
                }

                else if (arr[i][1] == "TCP" && arr[i].Length == 5)
                {
                    TcpProtocol tcp = new TcpProtocol();
                    tcp.Name = arr[i][2];
                    tcp.IP = arr[i][3];
                    if (!int.TryParse(arr[i][4], out int port)) return;
                    tcp.Port = port;
                    protocols.Add(tcp);
                    Console.WriteLine($"Created TCP '{tcp.Name}'");
                    continue;
                }

            }

            else if (arr[i][0] == "CONNECT" && arr[i].Length == 2)
            {
                for (int j = 0; j < protocols.Count; j++)
                {
                    if (arr[i][1] == protocols[j].Name)
                    {
                        protocols[j].Connect();
                        break;
                    }
                    else continue;

                }    
            }

            else if (arr[i][0] == "DISCONNECT" && arr[i].Length == 2)
            {
                for (int j = 0; j < protocols.Count; j++)
                {
                    if (arr[i][1] == protocols[j].Name)
                    {
                        protocols[j].Disconnect();
                        break;
                    }
                    else continue;

                }
            }

            else if (arr[i][0] == "SEND" && arr[i].Length == 3)
            {
                for (int j = 0; j < protocols.Count; j++)
                {
                    if (arr[i][1] == protocols[j].Name)
                    {
                        protocols[j].Send(arr[i][2]);
                        break;
                    }
                    else continue;

                }
            }
        }
    }
}
