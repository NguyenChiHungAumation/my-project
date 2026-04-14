using System;

class Program
{
    static void Main()
    {
        string[] arr = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (arr.Length != 2 || !int.TryParse(arr[0], out int H) || !int.TryParse(arr[1], out int W)) return;
        if (H < 1 || H > 50) return;
        if (W < 1 || W > 50) return ;

        int[,] ans = new int[H, W];
        int r = H / 2;
        int c = W / 2;
        ans[r, c] = 1;
        int num = 2;
        int len = 1;

        while(num <= H * W)
        {
            //sang phải
            for (int i = 0; i < len && num <= H * W; i++)
            {
                c++;
                if (r >= 0 && r < H && c >= 0 && c < W)
                {
                    ans[r, c] = num;
                    num++;
                }
                else continue;

            } 
            // xuống
            for (int i = 0; i < len && num <= H * W; i++)
            {
                r++;
                if (r >= 0 && r < H && c >= 0 && c < W)
                {
                    ans[r, c] = num;
                    num++;
                }
                else continue;
               
            }
            len++;

            // Sang trái

            for(int i = 0; i < len && num <= H * W; i++)
            {
                c--;
                if (r >= 0 && r < H && c >= 0 && c < W)
                {
                    ans[r, c] = num;
                    num++;
                }
                else continue;
                
            }

            for (int i = 0; i < len && num <= H * W; i++)
            {
                r--;
                if (r >= 0 && r < H && c >= 0 && c < W)
                {
                    ans[r, c] = num;
                    num++;
                }
                else continue;

            }
            len++;
        }
        int width = (H * W).ToString().Length;
        
        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < W; j++)
            {
                if (j > 0) Console.Write(" ");
                Console.Write(ans[i, j].ToString().PadLeft(width));
                
            }  
            Console.WriteLine();
        }    
    }
}