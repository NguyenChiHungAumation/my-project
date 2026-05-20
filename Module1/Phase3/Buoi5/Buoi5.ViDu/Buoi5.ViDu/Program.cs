using System;
using System.Collections.Generic;
using System.Collections.Concurrent; // Chứa Concurrent Collections
using System.Threading;
using System.Threading.Tasks;

class Program
{
    // 1. Sử dụng BlockingCollection (Concurrent Collection) đóng vai trò Buffer
    // Nó giúp quản lý việc nạp/lấy dữ liệu an toàn giữa nhiều Thread.
    private static BlockingCollection<string> taskQueue = new BlockingCollection<string>(10);

    // 2. Sử dụng SemaphoreSlim để giới hạn tối đa 3 Task được xử lý song song
    private static SemaphoreSlim semaphore = new SemaphoreSlim(3);

    static async Task Main(string[] args)
    {
        Console.WriteLine("--- KHOI CHAY HE THONG PRODUCER-CONSUMER ---");

        // Chạy luồng Producer (Người tạo việc)
        Task producer = Task.Run(() => Producer());

        // Chạy luồng Consumer (Người xử lý việc)
        Task consumer = Task.Run(() => Consumer());

        await Task.WhenAll(producer, consumer);

        Console.WriteLine("--- TAT CA CONG VIEC DA HOAN THANH ---");
    }

    // Luồng Sản xuất (Producer)
    static void Producer()
    {
        for (int i = 1; i <= 10; i++)
        {
            string taskName = $"Task #{i}";
            Console.WriteLine($"[Producer] Dang tao: {taskName}");

            // Dua vao hang doi (Neu hang doi day 10 phan tu, no se tu block va doi)
            taskQueue.Add(taskName);

            Thread.Sleep(200); // Gia lap thoi gian tao task
        }

        // Thong bao da het task de Consumer biet duong ma dung lai
        taskQueue.CompleteAdding();
        Console.WriteLine("[Producer] Da gui het tat ca task.");
    }

    // Luồng Tiêu thụ (Consumer)
    static async Task Consumer()
    {
        // GetConsumingEnumerable() giup vong lap foreach doi cho den khi co du lieu moi
        foreach (var taskName in taskQueue.GetConsumingEnumerable())
        {
            // Cho phep consumer bat dau xu ly task nhung phai thong qua Semaphore
            await ProcessTaskAsync(taskName);
        }
    }

    static async Task ProcessTaskAsync(string taskName)
    {
        // 3. SemaphoreSlim: Doi den khi co "ghe trong" (toi da 3 ghe)
        await semaphore.WaitAsync();

        _ = Task.Run(async () =>
        {
            try
            {
                Console.WriteLine($"   [Worker] Dang xu ly: {taskName} (Ghe trong con lai: {semaphore.CurrentCount})");

                // Gia lap thoi gian xu ly task
                await Task.Delay(new Random().Next(1000, 3000));

                Console.WriteLine($"   [Worker] Hoan thanh: {taskName}");
            }
            finally
            {
                // Giai phong 1 suat cho task khac vao
                semaphore.Release();
            }
        });
    }
}