using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WinMaintenance.Services
{
    public interface IMaintenanceService
    {
        Task RunCommandAsync(string command, string arguments, Action<string> onOutputDataReceived);
    }

    public class MaintenanceService : IMaintenanceService
    {
        public async Task RunCommandAsync(string command, string arguments, Action<string> onOutputDataReceived)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = System.Text.Encoding.UTF8,
                StandardErrorEncoding = System.Text.Encoding.UTF8
            };

            using var process = new Process { StartInfo = processStartInfo };

            process.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    onOutputDataReceived?.Invoke(e.Data);
                }
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    onOutputDataReceived?.Invoke($"ERROR: {e.Data}");
                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await process.WaitForExitAsync();
        }
    }
}
