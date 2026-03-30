using Microsoft.Extensions.Hosting;
using WinMaintenance.Views;

namespace WinMaintenance
{
    public class ApplicationHostService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public ApplicationHostService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (!System.Windows.Application.Current.Windows.OfType<MainWindow>().Any())
            {
                var mainWindow = _serviceProvider.GetService(typeof(MainWindow)) as MainWindow;
                mainWindow?.Show();
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
