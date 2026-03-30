using System.Windows.Controls;
using Wpf.Ui.Abstractions.Controls;

namespace WinMaintenance.Views
{
    public partial class SettingsView : INavigableView<ViewModels.SettingsViewModel>
    {
        public ViewModels.SettingsViewModel ViewModel { get; }

        public SettingsView(ViewModels.SettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
