using System.Diagnostics;

namespace WinMaintenanceTool.Services;

public sealed class CommandRunnerService : ICommandRunnerService
{
    public async Task RunAsync(string fileName, string arguments, Action<string> onOutput, CancellationToken cancellationToken = default)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = arguments,
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            StandardOutputEncoding = System.Text.Encoding.UTF8,
            StandardErrorEncoding = System.Text.Encoding.UTF8
        };

        using var process = new Process { StartInfo = startInfo, EnableRaisingEvents = true };
        var completion = new TaskCompletionSource<int>();

        process.OutputDataReceived += (_, e) =>
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
                onOutput(e.Data);
        };

        process.ErrorDataReceived += (_, e) =>
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
                onOutput(e.Data);
        };

        process.Exited += (_, _) => completion.TrySetResult(process.ExitCode);

        if (!process.Start())
            throw new InvalidOperationException("Could not start process.");

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        using var registration = cancellationToken.Register(() =>
        {
            try
            {
                if (!process.HasExited)
                    process.Kill(entireProcessTree: true);
            }
            catch
            {
                // Ignored.
            }
        });

        var exitCode = await completion.Task;
        onOutput($"Exit code: {exitCode}");
    }
}
