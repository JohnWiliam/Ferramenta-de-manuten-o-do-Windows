using System.Globalization;
using System.Resources;

namespace WinMaintenanceTool.Resources;

public static class Strings
{
    private static readonly ResourceManager ResourceManager = new("WinMaintenanceTool.Resources.Strings", typeof(Strings).Assembly);

    private static string Get(string name) => ResourceManager.GetString(name, CultureInfo.CurrentUICulture) ?? name;

    public static string AppTitle => Get(nameof(AppTitle));
    public static string HomeTitle => Get(nameof(HomeTitle));
    public static string HomeSubtitle => Get(nameof(HomeSubtitle));
    public static string HomeSfcDesc => Get(nameof(HomeSfcDesc));
    public static string HomeDismDesc => Get(nameof(HomeDismDesc));
    public static string SfcTitle => Get(nameof(SfcTitle));
    public static string SfcDescription => Get(nameof(SfcDescription));
    public static string SfcScanNowDesc => Get(nameof(SfcScanNowDesc));
    public static string SfcVerifyOnlyDesc => Get(nameof(SfcVerifyOnlyDesc));
    public static string DismTitle => Get(nameof(DismTitle));
    public static string DismDescription => Get(nameof(DismDescription));
    public static string DismCheckHealthDesc => Get(nameof(DismCheckHealthDesc));
    public static string DismScanHealthDesc => Get(nameof(DismScanHealthDesc));
    public static string DismRestoreHealthDesc => Get(nameof(DismRestoreHealthDesc));
    public static string DismAnalyzeDesc => Get(nameof(DismAnalyzeDesc));
    public static string DismCleanupDesc => Get(nameof(DismCleanupDesc));
    public static string DismResetBaseDesc => Get(nameof(DismResetBaseDesc));
    public static string SettingsTitle => Get(nameof(SettingsTitle));
    public static string ProcessCompleted => Get(nameof(ProcessCompleted));
    public static string ExecutionError => Get(nameof(ExecutionError));
}
