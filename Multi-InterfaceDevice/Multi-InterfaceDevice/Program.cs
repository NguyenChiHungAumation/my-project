using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public interface IConnectable
{
    public bool IsConnected { get; set; }

    public void Connect();
    public void Disconnect();


}


public interface IInspectable
{
    public string LastResult { get; set; }
    public string Inspect();
}

class SmartCamera : IConnectable, IInspectable
{
    public int soLanGoiInspect {  get; set; } = 0;
    public string LastResult {  set; get; }
    public bool IsConnected { set; get; } = false;
    public string Name {  get; private set; }
    public SmartCamera(string name)
    {
        Name = name;
    }

    public void Connect()
    {
        if (IsConnected == false)
        {
            IsConnected = true;
            Console.WriteLine($"{Name}: connected");
        }
        else
            Console.WriteLine($"{Name}: already connected");
    }

    public void Disconnect()
    {
        if (IsConnected == true)
        {
            IsConnected = false;
            Console.WriteLine($"{Name}: disconnected");
        }
        else
            Console.WriteLine($"{Name}: already disconnected");
    }
    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public string Inspect()
    {
        if (IsConnected == false)
        {
            Console.WriteLine($"{Name}: ERROR not connected");
            return LastResult;
        }
        else if (IsConnected == true)
        {
            if (soLanGoiInspect % 2 == 0 && soLanGoiInspect > 0)
            {
                LastResult = "NG";
                Console.WriteLine($"{Name}: {LastResult}");
            }
            else if (soLanGoiInspect % 2 == 1 && soLanGoiInspect > 0)
            {
                LastResult = "OK";
                Console.WriteLine($"{Name}: {LastResult}");
            }
            return LastResult;

        }

        return LastResult;
    }

}

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        if (!int.TryParse(s, out int N) || N < 0) return;

        string[][] mang = new string[N][];  

        for (int i = 0; i < N; i++)
        {
            string input = Console.ReadLine();
            mang[i] = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }
        
        List<SmartCamera> cameras = new List<SmartCamera>();

        for (int i = 0;i < N; i++)
        {
            if (mang[i][0] == "CREATE" && mang[i].Length == 2)
            {
                SmartCamera camera = new SmartCamera(mang[i][1]);
                cameras.Add(camera);
                Console.WriteLine($"Created SmartCamera '{camera.Name}'");
                continue;

            }

            else if (mang[i][0] == "CONNECT" && mang[i].Length == 2)
            {
                for (int j = 0; j < cameras.Count; j++)
                {
                    if (mang[i][1] == cameras[j].Name)
                    {
                        cameras[j].Connect();
                        break;
                    }
                    else continue;
                }    
            }

            else if (mang[i][0] == "DISCONNECT" && mang[i].Length == 2)
            {
                for (int j = 0; j < cameras.Count; j++)
                {
                    if (mang[i][1] == cameras[j].Name)
                    {
                        cameras[j].Disconnect();
                        break;
                    }
                    else continue;
                }
            }

            else if (mang[i][0] == "INSPECT" && mang[i].Length == 2)
            {
                for (int j = 0; j < cameras.Count; j++)
                {
                    if (mang[i][1] == cameras[j].Name)
                    {
                        if (cameras[j].IsConnected == true)
                        {
                            cameras[j].soLanGoiInspect++;
                        }
                        cameras[j].Inspect();
                        break;
                    }
                    else continue;
                }
            }

            else if (mang[i][0] == "RESULT" && mang[i].Length == 2)
            {
                for (int j = 0; j < cameras.Count; j++)
                {
                    if (mang[i][1] == cameras[j].Name)
                    {
                        if (cameras[j].soLanGoiInspect == 0)
                        {
                            Console.WriteLine($"{cameras[j].Name}: LastResult=None");
                        }
                        else
                        {
                            Console.WriteLine($"{cameras[j].Name}: LastResult={cameras[j].LastResult}");
                        }    
                        break;
                    }
                    else continue;
                }
            }



        }    
    }
}