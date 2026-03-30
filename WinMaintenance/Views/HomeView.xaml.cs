using System.Windows.Controls;
using Wpf.Ui.Abstractions.Controls;

namespace WinMaintenance.Views
{
    public partial class HomeView : INavigableView<ViewModels.HomeViewModel>
    {
        public ViewModels.HomeViewModel ViewModel { get; }

        public HomeView(ViewModels.HomeViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
