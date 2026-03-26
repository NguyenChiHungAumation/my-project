using System;
using System.Globalization;

class Frame
{
    public static int TransactionID(string index0, string index1)
    {
        string Id = index0 + index1;
        if (!int.TryParse(Id, NumberStyles.HexNumber, null, out int value)) return -1;
        return value;
    }

    public static int UnitID (string index6)
    {
        if (!int.TryParse(index6, NumberStyles.HexNumber, null, out int value)) return -1;
        return value;
    }

    public static int FunctionCode(string index7)
    {
        if (!int.TryParse(index7, NumberStyles.HexNumber, null, out int value)) return -1;
        return value;
    }

    public static int RegisterAddress(string index8, string index9)
    {
        string Add = index8 + index9;
        if (!int.TryParse(Add, NumberStyles.HexNumber, null, out int value)) return -1;
        return value;
    }

    public static int Quantily(string index10, string index11)
    {
        string Quan = index10 + index11;
        if (!int.TryParse(Quan, NumberStyles.HexNumber, null, out int value)) return -1;
        return value;
    }

}

class Program
{
    static void Main()
    {
        //string[] arr = new string[12];
        string input = Console.ReadLine();
        string[] arr = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (arr.Length != 12) return;
        for (int i =0; i < 12; i++)
        {
            if (arr[i].Length != 2) return;
            else continue;
        }    
        
        int Transaction = Frame.TransactionID(arr[0], arr[1]);
        if (Transaction < 0) return;
        else Console.WriteLine($"Transaction ID: {Transaction}");

        int UnitID = Frame.UnitID(arr[6]);
        if (UnitID < 0) return;
        else Console.WriteLine($"Unit ID: {UnitID}");

        int function = Frame.FunctionCode(arr[7]);
        if (function < 0) return;
        else Console.WriteLine($"Function Code: {function}");

        int register = Frame.RegisterAddress(arr[8], arr[9]);
        if (register < 0) return;
        else Console.WriteLine($"Register Address: {register}");

        int quantity = Frame.Quantily(arr[10], arr[11]);
        if (quantity < 0) return;
        else Console.WriteLine($"Quantity: {quantity}");


    }
}
