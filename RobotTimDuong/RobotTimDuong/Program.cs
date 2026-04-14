using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Đọc H và W, xử lý các dấu cách thừa
        string inputSize = Console.ReadLine();
        if (string.IsNullOrEmpty(inputSize)) return;

        string[] s = inputSize.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        if (s.Length < 2) return;

        int H = int.Parse(s[0]);
        int W = int.Parse(s[1]);

        char[,] grid = new char[H, W];
        (int r, int c) posS = (-1, -1);
        (int r, int c) posE = (-1, -1);

        // Đọc ma trận
        for (int i = 0; i < H; i++)
        {
            string line = Console.ReadLine();
            // Lấy tất cả các ký tự không phải khoảng trắng
            string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            string rowStr = string.Join("", parts);

            for (int j = 0; j < W; j++)
            {
                grid[i, j] = rowStr[j];
                if (grid[i, j] == 'S') posS = (i, j);
                else if (grid[i, j] == 'E') posE = (i, j);
            }
        }

        // Kiểm tra nếu không tìm thấy S hoặc E
        if (posS.Item1 == -1 || posE.Item1 == -1)
        {
            Console.WriteLine("No path");
            return;
        }

        // BFS
        Queue<(int r, int c)> queue = new Queue<(int r, int c)>();
        int[,] dist = new int[H, W];
        (int r, int c)[,] parent = new (int r, int c)[H, W];
        bool[,] visited = new bool[H, W];

        // Khởi tạo mảng dist
        for (int i = 0; i < H; i++)
            for (int j = 0; j < W; j++)
                dist[i, j] = -1;

        queue.Enqueue(posS);
        visited[posS.r, posS.c] = true;
        dist[posS.r, posS.c] = 0;

        int[] dr = { -1, 1, 0, 0 }; // Lên, Xuống, Trái, Phải
        int[] dc = { 0, 0, -1, 1 };
        bool found = false;

        while (queue.Count > 0)
        {
            var cur = queue.Dequeue();

            if (cur.r == posE.r && cur.c == posE.c)
            {
                found = true;
                break;
            }

            for (int i = 0; i < 4; i++)
            {
                int nr = cur.r + dr[i];
                int nc = cur.c + dc[i];

                if (nr >= 0 && nr < H && nc >= 0 && nc < W && !visited[nr, nc] && grid[nr, nc] != '#')
                {
                    visited[nr, nc] = true;
                    dist[nr, nc] = dist[cur.r, cur.c] + 1;
                    parent[nr, nc] = cur;
                    queue.Enqueue((nr, nc));
                }
            }
        }

        // Xuất kết quả
        if (!found)
        {
            Console.WriteLine("No path");
        }
        else
        {
            Console.WriteLine($"Distance: {dist[posE.r, posE.c]}");

            // Truy vết đường đi
            List<string> path = new List<string>();
            (int r, int c) temp = posE;
            while (true)
            {
                path.Add($"({temp.r},{temp.c})");
                if (temp.r == posS.r && temp.c == posS.c) break;
                temp = parent[temp.r, temp.c];
            }
            path.Reverse();
            Console.WriteLine("Path: " + string.Join(" -> ", path));
        }
    }
}