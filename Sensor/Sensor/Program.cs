using System;
using System.Linq.Expressions;
using System.Text;

class Ss
{
    private int _soGiaTri;
    public int soGiaTri
    {
        get { return _soGiaTri; }
        set
        {
            if (value < 1)
                throw new ArgumentException("Không được nhỏ hơn 1");
            if (value > 10000)
                throw new ArgumentException("Không được lớn hơn 10000");
            _soGiaTri = value;
        }
    }

}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        var hung = new Ss();


        //Dòng 1 
        string input =Console.ReadLine();

        if (int.TryParse(input, out int value))
        {
            if (value >= 1 && value <= 10000)
            {
                hung.soGiaTri = value;
            }
            else
                return;
        }
        else
            return;

        //Dòng 2
        //string[] arr = new int[hung.soGiaTri];

        //Ghi chuỗi
         String input2 = Console.ReadLine();

        if (string.IsNullOrEmpty(input2))
        {
            return;
        }

        //tách thành mảng
        string[] values = input2.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (values.Length != hung.soGiaTri)
        {
            return ;
        }

        //tạo mảng int
        int[] arr = new int[hung.soGiaTri];

        //Convert từng phần tử

        for (int i =0; i < hung.soGiaTri; i++)
        {
            if (!int.TryParse(values[i], out arr[i]))
            {
                return;
            }    
        }

        //Tính giá trị Max và Min

        int max = arr[0];
        int min = arr[0];
        int range = 0;
        int sum = 0;

        for (int i =0; i < hung.soGiaTri; i++)
        {
            if (arr[i] > max)
            {
                max = arr[i];
            }    

            if (arr[i] < min)
            {
                min = arr[i];
            }    
        }  
        range = max - min;

        for (int i = 0; i < hung.soGiaTri; i++)
        {
            sum += arr[i];
        }


        double avg = Math.Round(sum * 1.0 / hung.soGiaTri, 2, MidpointRounding.AwayFromZero);


        // IN ra màn hình

        Console.WriteLine("MAX: " + max);
        Console.WriteLine("MIN: " + min);
        Console.WriteLine("AVG: " + avg.ToString("F2"));
        Console.WriteLine("RANGE: " + range);






       /* Console.WriteLine("Số là: " + hung.soGiaTri);
        for (int i = 0; i < hung.soGiaTri; i++)
        {
           Console.Write(arr[i]);
           Console.Write(" ");
        } */   



    }


}