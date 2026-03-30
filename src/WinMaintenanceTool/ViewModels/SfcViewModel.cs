using WinMaintenanceTool.Models;
using WinMaintenanceTool.Resources;
using WinMaintenanceTool.Services;

namespace WinMaintenanceTool.ViewModels;

public sealed class SfcViewModel(ICommandRunnerService commandRunnerService) : MaintenancePageViewModel(commandRunnerService)
{
    public string Title => Strings.SfcTitle;
    public string Description => Strings.SfcDescription;

    public void Reload()
    {
        Actions.Clear();
        Actions.Add(new MaintenanceAction("SFC /scannow", Strings.SfcScanNowDesc, "sfc /scannow"));
        Actions.Add(new MaintenanceAction("SFC /verifyonly", Strings.SfcVerifyOnlyDesc, "sfc /verifyonly"));
    }
}
