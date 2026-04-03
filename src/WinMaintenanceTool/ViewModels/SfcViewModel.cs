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
        Actions.Add(new MaintenanceAction(Strings.SfcScanNowTitle, Strings.SfcScanNowDesc, "sfc /scannow"));
        Actions.Add(new MaintenanceAction(Strings.SfcVerifyOnlyTitle, Strings.SfcVerifyOnlyDesc, "sfc /verifyonly"));

        RefreshCommonLocalizedText();
    }
}
