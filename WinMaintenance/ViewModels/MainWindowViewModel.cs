using CommunityToolkit.Mvvm.ComponentModel;

namespace WinMaintenance.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "Windows Maintenance Tool";
    }
}
