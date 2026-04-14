using System;
using System.Collections.Generic;
 


class Program
{
    static void Main()
    {


        void VungSang(int r, int c, int H, int W, int[,] arr, bool[,] daXet, int threshold
            , out int minr, out int minc, out int maxr, out int maxc, out int size, out int sum)
        {
            
            int[] sr = new int[] { -1, 1, 0, 0 };
            int[] sc = new int[] { 0, 0, -1, 1 };
            size = 0;
            sum = 0;
            minr = r;
            minc = c;
            maxr = r;
            maxc = c;
            List<(int r, int c)> q = new List<(int r, int c)>();
            q.Add((r, c));
            while (q.Count != 0)
            {
                var cur =q[0];
                q.RemoveAt(0);


                sum += arr[cur.r, cur.c];
                size += 1;

                if (minr > cur.r) { minr = cur.r; }
                if (minc > cur.c) { minc = cur.c; }
                if (maxr < cur.r) { maxr = cur.r; }
                if (maxc < cur.c) { maxc = cur.c; }

                for (int k = 0; k < 4; k++)
                {
                    int nr = cur.r + sr[k];
                    int nc = cur.c + sc[k];
                    if (nr < 0 || nr >= H || nc < 0 || nc >= W) continue;
                    if (daXet[nr, nc] == true) continue;
                    if (arr[nr, nc] >= threshold)
                    {
                        daXet[nr, nc] = true;
                        q.Add((nr, nc));
                        continue;
                    }
                    else
                    {
                        daXet[nr, nc] = true;
                        continue;
                    }
                }
            }

        }

    
        
            

        string[] s = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (!int.TryParse(s[0], out int H) || H < 1 || H > 100) return;
        if (!int.TryParse(s[1], out int W) || W < 1 || W > 100) return;
        string a = Console.ReadLine();
        if (!int.TryParse(a, out int threshold) || threshold < 0 || threshold > 255) return;



        int[,] arr = new int[H, W];
        bool[,] daXet = new bool[H, W];
        for (int i = 0; i < H; i++)
        {
            string[] row = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (row.Length != W) return;
            for (int j = 0; j < W; j++)
            {
                if (!int.TryParse(row[j], out arr[i, j])) return;
            }
            
        }
        int Regions = 0;
        int minR = 0;
        int minC = 0;
        int maxR = 0;
        int maxC = 0;
        int size = 0;
        int sum = 0;
        int Sum = 0;
        int Largest = 0;
        int MinR = 0;
        int MinC = 0;
        int MaxR = 0;
        int MaxC = 0;
        double Mean = 0.0;




        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < W; j++)
            {
                if (daXet[i, j] == true) continue;
                if (arr[i, j] >= threshold)
                {
                    daXet[i, j] = true;
                    Regions++;
                    VungSang(i, j, H, W, arr, daXet, threshold, out minR,
                        out minC, out maxR, out maxC, out size, out sum);
                    //Largest = Math.Max(Largest, Size);

                    if (size > Largest)
                    {
                        Largest = size;
                        MinR = minR;
                        MinC = minC;
                        MaxR = maxR;
                        MaxC = maxC;
                        Sum = sum;
                        Mean = (double)Sum / Largest; 
                    }
                    

                }
                
            }    
        }
        if (Regions != 0)
        {
            Console.WriteLine($"Regions: {Regions}");
            Console.WriteLine($"Largest: {Largest} pixels");
            Console.WriteLine($"BoundingBox: ({MinR},{MinC})-({MaxR},{MaxC})");
            Console.WriteLine($"Mean: {Mean.ToString("F1")}");
        }
        else Console.WriteLine($"No bright region found");



    }

    
}
