using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WinMaintenance.Services;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace WinMaintenance.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly IMaintenanceService _maintenanceService;

        [ObservableProperty]
        private string _terminalOutput = string.Empty;

        [ObservableProperty]
        private bool _isBusy = false;

        public HomeViewModel(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        private void AppendOutput(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                TerminalOutput += $"{message}\n";
            });
        }

        [RelayCommand]
        private async Task RunSfcScannow()
        {
            await RunMaintenanceCommandAsync("sfc", "/scannow");
        }

        [RelayCommand]
        private async Task RunDismCheckHealth()
        {
            await RunMaintenanceCommandAsync("DISM", "/Online /Cleanup-Image /CheckHealth");
        }

        [RelayCommand]
        private async Task RunDismScanHealth()
        {
            await RunMaintenanceCommandAsync("DISM", "/Online /Cleanup-Image /ScanHealth");
        }

        [RelayCommand]
        private async Task RunDismRestoreHealth()
        {
            await RunMaintenanceCommandAsync("DISM", "/Online /Cleanup-Image /RestoreHealth");
        }

        [RelayCommand]
        private async Task RunDismAnalyzeComponentStore()
        {
            await RunMaintenanceCommandAsync("DISM", "/Online /Cleanup-Image /AnalyzeComponentStore");
        }

        [RelayCommand]
        private async Task RunDismStartComponentCleanup()
        {
            await RunMaintenanceCommandAsync("DISM", "/Online /Cleanup-Image /StartComponentCleanup");
        }

        [RelayCommand]
        private async Task RunDismResetBase()
        {
            await RunMaintenanceCommandAsync("DISM", "/Online /Cleanup-Image /StartComponentCleanup /ResetBase");
        }

        private async Task RunMaintenanceCommandAsync(string command, string arguments)
        {
            if (IsBusy) return;

            IsBusy = true;
            TerminalOutput = $"Running {command} {arguments}...\n\n";

            try
            {
                await _maintenanceService.RunCommandAsync(command, arguments, AppendOutput);
                AppendOutput("\nCommand completed successfully.");
            }
            catch (Exception ex)
            {
                AppendOutput($"\nAn error occurred: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
