namespace WinMaintenanceTool.Services;

public interface ILocalizationService
{
    event EventHandler? LanguageChanged;
    void SetLanguage(string languageKey);
}
