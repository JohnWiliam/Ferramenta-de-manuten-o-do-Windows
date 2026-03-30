using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wpf.Ui;
using WinMaintenance.ViewModels;
using WinMaintenance.Views;
using WinMaintenance.Services;

namespace WinMaintenance
{
    public partial class App : Application
    {
        private static readonly IHost _host = Host
            .CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddHostedService<ApplicationHostService>();

                // Theme and navigation
                services.AddSingleton<IThemeService, ThemeService>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<ISnackbarService, SnackbarService>();
                services.AddSingleton<IContentDialogService, ContentDialogService>();

                // Maintenance Service
                services.AddSingleton<IMaintenanceService, MaintenanceService>();

                // Views and ViewModels
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainWindowViewModel>();

                services.AddTransient<HomeView>();
                services.AddTransient<HomeViewModel>();

                services.AddTransient<SettingsView>();
                services.AddTransient<SettingsViewModel>();
            })
            .Build();

        public static T GetService<T>() where T : class
        {
            return _host.Services.GetService(typeof(T)) as T ?? throw new InvalidOperationException($"Service {typeof(T)} not found.");
        }

        private async void OnStartup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();
        }

        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
        }
    }
}
