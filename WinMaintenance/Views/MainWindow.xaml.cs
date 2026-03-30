using System.Windows;
using System.Windows.Controls;
using Wpf.Ui;
using Wpf.Ui.Abstractions;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using WinMaintenance.ViewModels;

namespace WinMaintenance.Views
{
    public partial class MainWindow : FluentWindow, INavigationWindow
    {
        public MainWindowViewModel ViewModel { get; }

        public MainWindow(
            MainWindowViewModel viewModel,
            INavigationService navigationService,
            IServiceProvider serviceProvider,
            ISnackbarService snackbarService,
            IContentDialogService contentDialogService)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            SystemThemeWatcher.Watch(this);
            ApplicationThemeManager.ApplySystemTheme();

            navigationService.SetNavigationControl(RootNavigation);
            snackbarService.SetSnackbarPresenter(SnackbarPresenter);
            contentDialogService.SetContentPresenter(ContentDialogPresenter);

            RootNavigation.SetServiceProvider(serviceProvider);
            Loaded += (_, _) => RootNavigation.Navigate(typeof(HomeViewModel));
        }

        public INavigationView GetNavigation() => RootNavigation;
        public bool Navigate(Type pageType) => RootNavigation.Navigate(pageType);
        public void SetServiceProvider(IServiceProvider serviceProvider) => RootNavigation.SetServiceProvider(serviceProvider);
        public void SetPageService(INavigationViewPageProvider pageService) { }
        public void ShowWindow() => Show();
        public void CloseWindow() => Close();
    }
}
