using System.Diagnostics;
using System.IO;

namespace App.WindowsService;

public sealed class WindowsBackgroundService(ILogger<WindowsBackgroundService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                //using (StreamWriter streamWriter = new StreamWriter(@"C:\Users\121101\Desktop\test.txt",true))
                //{

                //    streamWriter.WriteLine("服務測試:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                string exePath = @"C:\Users\Administrator\Desktop\AIOT\slnKanbanUpdateData\KanbanUpdateData\bin\Debug\net8.0\KanbanUpdateData.exe";
                if (!File.Exists(exePath))
                {
                    logger.LogError("查詢不到該路徑: {ExePath}", exePath);
                    Environment.Exit(1);
                }

                ProcessStartInfo startInfo = new ProcessStartInfo(exePath);
                Process process = Process.Start(startInfo);

                await Task.Delay(15000, stoppingToken);
                }
            //}
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            Environment.Exit(1);
        }
    }
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        CancellationTokenSource _stoppingCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        Task _executeTask = ExecuteAsync(_stoppingCts.Token);
    }
}
