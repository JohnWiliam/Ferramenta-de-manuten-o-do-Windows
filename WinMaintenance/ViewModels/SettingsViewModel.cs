using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;
using System.Threading;
using Wpf.Ui.Appearance;

namespace WinMaintenance.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _currentTheme;

        [ObservableProperty]
        private string _currentLanguage;

        public SettingsViewModel()
        {
            _currentTheme = ApplicationThemeManager.GetAppTheme().ToString();
            _currentLanguage = Thread.CurrentThread.CurrentUICulture.Name;
        }

        [RelayCommand]
        private void ChangeTheme(string theme)
        {
            if (theme == "SystemDefault")
            {
                ApplicationThemeManager.ApplySystemTheme();
                CurrentTheme = ApplicationThemeManager.GetAppTheme().ToString();
            }
            else if (theme == "Light")
            {
                ApplicationThemeManager.Apply(ApplicationTheme.Light);
                CurrentTheme = "Light";
            }
            else if (theme == "Dark")
            {
                ApplicationThemeManager.Apply(ApplicationTheme.Dark);
                CurrentTheme = "Dark";
            }
        }

        [RelayCommand]
        private void ChangeLanguage(string cultureCode)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
            CurrentLanguage = cultureCode;
        }
    }
}
