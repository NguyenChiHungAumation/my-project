using System;
using System.ComponentModel.Design;
using System.Runtime.Serialization.Formatters;

class Sensor
{
    public int Id { get; private set; }
    public string Name {  get; private set; }
    public string Unit { get; private set; }
    public  double Value { get; private set; } = 0;

    public Sensor(int id, string name, string unit)
    {
        Id = id;
        Name = name;
        Unit = unit;
    }

    public void SetValue(double value)
    {
        Value = value;
    }

    public string Display()
    {
        return $"{Name} {Value} {Unit}";
    }

    public string CheckRange(double min, double max)
    {
        if (min <= Value && Value <= max)
        {
            return "OK";
        }
        else
            return "ALARM";

    }

   
    
}



class Program
{
    static void Main()
    {

        string input = Console.ReadLine(); // nhập số N vào màn hình

        if (!int.TryParse(input, out int soN) || soN <= 0) // kiểm tra số N là số nguyên và lớn hơn 0
        {
            return; // kết thúc chương trình 

        }
        string[][] part = new string[soN][]; // tạo N mảng 2 chiều 

        for (int i = 0; i < soN; i++) // tạo các mảng 2 chiều gồm 10 phần tử
        {
            part[i] = new string[10];
        }

        for (int i = 0; i < soN; i++)
        {
            string s = Console.ReadLine(); // Nhập chuỗi vào từng dòng
            part[i] = s.Split(' ', StringSplitOptions.RemoveEmptyEntries); // tách chuỗi thành mảng
        }

        var sensor = new Sensor[soN];
        int count = 0;

        for (int i = 0; i < soN; i++)
        {
            if (part[i][0] == "ADD") // lọc điều kiện ADD
            {
                int countPhanTuAdd = part[i].Length;
                if (countPhanTuAdd == 4)
                {
                    int id = int.Parse(part[i][1]);
                    string name = part[i][2];
                    string unit = part[i][3];

                    sensor[count] = new Sensor(id, name, unit);
                    Console.WriteLine($"Sensor #{sensor[count].Id} '{sensor[count].Name}' added");
                    count++;
                }
                else
                    return;

            }
            

            else if (part[i][0] == "SET") // lọc điều kiện SET
            {
                int countPhanTuSet = part[i].Length;
                if (countPhanTuSet == 3)
                {
                    
                    int id = int.Parse(part[i][1]);
                    double value = double.Parse(part[i][2]);
                    for (int b = 0; b < count; b++)
                    {
                        if (id == sensor[b].Id)
                        {
                            sensor[b].SetValue(value);
                            //sensor[b].SetValue(double.Parse(part[i][2]));
                            Console.WriteLine($"Sensor #{sensor[b].Id}: {(sensor[b].Value).ToString("F2")}");
                            break;
                        }
                        else
                            continue;
                    }

                }
                else
                    return ;
                
            }
   

            else if (part[i][0] == "DISPLAY")
            {
                int countPhanTuDisplay = part[i].Length;
                if (countPhanTuDisplay == 2)
                {
                    int id = int.Parse(part[i][1]);
                    for (int b = 0; b < count; b++)
                    {
                        if (id == sensor[b].Id)
                        {

                            Console.WriteLine($"[{sensor[b].Name}] {(sensor[b].Value).ToString("F2")} {sensor[b].Unit}");
                            break;
                        
                        }
                        else
                            continue;
                    }
                }

                else
                    return;
                
            }


            else if (part[i][0] == "CHECK")
            {
                int countPhanTuCheck = part[i].Length;
                if (countPhanTuCheck == 4)
                {
                    int id = int.Parse (part[i][1]);
                    double min = double.Parse(part[i][2]);
                    double max = double.Parse(part[i][3]);
                    for (int b = 0; b < count; b++)
                    {
                        if (id == sensor[b].Id)
                        {
                            sensor[b].CheckRange(min, max);
                            Console.WriteLine($"Sensor #{sensor[b].Id}: {(sensor[b].Value).ToString("F2")} {sensor[b].Unit} - {sensor[b].CheckRange(min, max)}");
                            break;
                        }
                        else
                            continue;
                   
                    }


                }
            }
            


        }
    }
}

