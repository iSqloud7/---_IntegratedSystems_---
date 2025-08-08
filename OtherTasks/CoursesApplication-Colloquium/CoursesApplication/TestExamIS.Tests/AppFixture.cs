using System.Diagnostics;

namespace TestExamIS.Tests;

public class AppFixture : IAsyncLifetime
{
    private Process? _appProcess;
    private const string AppUrl = "http://localhost:5164";

    public async Task InitializeAsync()
    {
        if (!await IsAppRunning())
        {
            var a = Directory.GetCurrentDirectory();
            var projectPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../"));

            var startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                WorkingDirectory = projectPath,
                Arguments = $"run --project CoursesApplication.Web --environment Test",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            
            _appProcess = Process.Start(startInfo);

            for (int i = 0; i < 30; i++)
            {
                if (await IsAppRunning()) break;
                await Task.Delay(1000);
            }
            
            if (!await IsAppRunning())
                throw new Exception("App did not start in time.");
        }
    }

    public Task DisposeAsync()
    {
        if (_appProcess is { HasExited: false })
        {
            _appProcess.Kill(true);
            _appProcess.Dispose();
        }

        return Task.CompletedTask;
    }

    private static async Task<bool> IsAppRunning()
    {
        try
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(AppUrl);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}