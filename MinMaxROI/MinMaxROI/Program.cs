using System;

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        string[] arr = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (arr.Length != 2) return;
        if (!int.TryParse(arr[0], out int H) || H < 1 || H > 100) return;
        if (!int.TryParse(arr[1], out int W) || W < 1 || W > 100) return;

        string[][] arr1 = new string[H][];
        int[][] data = new int[H][]; 


        for (int i = 0; i < H; i++)
        {
            string a = Console.ReadLine();
            arr1[i] = a.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (arr1[i].Length != W) return;
        }

        for (int i = 0; i < H; i++)
        {
            data[i] = new int[W];
        }    

        for (int i = 0; i < H; i++)
        {

            for (int j = 0; j < W; j++)
            {
                if (!int.TryParse(arr1[i][j], out data[i][j]) || data[i][j] < 0 || data[i][j] > 255) return;
            }    
        }

        string z = Console.ReadLine();
    
        string[] arr2 = z.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (arr2.Length != 4) return;
        if (!int.TryParse(arr2[0], out int startRow) || startRow < 0 || startRow >= H) return;
        if (!int.TryParse(arr2[1], out int startCol) || startCol < 0 || startCol >= W) return;
        if (!int.TryParse(arr2[2], out int roiHeight) || roiHeight < 1 || roiHeight > 100) return;
        if (!int.TryParse(arr2[3], out int roiWidth) || roiWidth < 1 || roiWidth > 100) return;

        if (startRow + roiHeight > H || startCol + roiWidth > W) return;
        int min = data[startRow][startCol];
        int max = data[startRow][startCol];

        int posMinHeight = startRow;
        int posMinWidth = startCol;
        int posMaxHeight = startRow;
        int posMaxWidth = startCol;


        for (int i = startRow; i < roiHeight + startRow; i++)
        {
            for (int j = startCol; j < roiWidth + startCol; j++)
            {
                if (max < data[i][j])
                {
                    max = data[i][j];
                    posMaxHeight = i;
                    posMaxWidth = j;
                }  
                
                if (min > data[i][j])
                {
                    min = data[i][j];
                    posMinHeight = i;
                    posMinWidth = j;

                }    
            }    
        }


        Console.WriteLine($"Min: {min} at ({posMinHeight}, {posMinWidth})");
        Console.WriteLine($"Max: {max} at ({posMaxHeight}, {posMaxWidth})");
    }
}
