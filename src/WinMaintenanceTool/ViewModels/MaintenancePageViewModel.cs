using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using WinMaintenanceTool.Models;
using WinMaintenanceTool.Resources;
using WinMaintenanceTool.Services;

namespace WinMaintenanceTool.ViewModels;

public abstract partial class MaintenancePageViewModel : ViewModelBase
{
    private readonly ICommandRunnerService _commandRunnerService;
    private bool _isBusy;
    private string _output = string.Empty;
    private MaintenanceAction? _selectedAction;
    private double _progressValue;

    protected MaintenancePageViewModel(ICommandRunnerService commandRunnerService)
    {
        _commandRunnerService = commandRunnerService;
        RunCommand = new RelayCommand<MaintenanceAction>(async action => await ExecuteActionAsync(action), action => !_isBusy && action is not null);
    }

    [GeneratedRegex(@"(\d{1,3}(?:[\.,]\d+)?)%")]
    private static partial Regex ProgressRegex();

    public ObservableCollection<MaintenanceAction> Actions { get; } = [];

    public RelayCommand<MaintenanceAction> RunCommand { get; }

    public string Output
    {
        get => _output;
        set => SetProperty(ref _output, value);
    }

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            SetProperty(ref _isBusy, value);
            RunCommand.NotifyCanExecuteChanged();
            RaisePropertyChanged(nameof(IsProgressIndeterminate));
        }
    }

    public MaintenanceAction? SelectedAction
    {
        get => _selectedAction;
        private set
        {
            if (!SetProperty(ref _selectedAction, value))
                return;

            RaisePropertyChanged(nameof(SelectedActionTitle));
        }
    }

    public string SelectedActionTitle => SelectedAction?.Title ?? Strings.NoActionSelected;

    public double ProgressValue
    {
        get => _progressValue;
        private set
        {
            if (!SetProperty(ref _progressValue, value))
                return;

            RaisePropertyChanged(nameof(HasDeterminateProgress));
            RaisePropertyChanged(nameof(IsProgressIndeterminate));
        }
    }

    public bool HasDeterminateProgress => ProgressValue > 0;

    public bool IsProgressIndeterminate => IsBusy && !HasDeterminateProgress;

    public string RunButtonLabel => Strings.RunButton;

    public string ExecutionLogTitle => Strings.ExecutionLogTitle;

    protected void RefreshCommonLocalizedText()
    {
        RaisePropertyChanged(nameof(SelectedActionTitle));
        RaisePropertyChanged(nameof(RunButtonLabel));
        RaisePropertyChanged(nameof(ExecutionLogTitle));
    }

    private async Task ExecuteActionAsync(MaintenanceAction? action)
    {
        if (action is null)
            return;

        SelectedAction = action;
        IsBusy = true;
        ProgressValue = 0;
        Output = $"[{DateTime.Now:HH:mm:ss}] {Strings.RunningAction}: {action.Title}{Environment.NewLine}";

        try
        {
            var split = action.Command.Split(' ', 2, StringSplitOptions.TrimEntries);
            var fileName = split[0];
            var arguments = split.Length > 1 ? split[1] : string.Empty;

            await _commandRunnerService.RunAsync(fileName, arguments, line =>
            {
                Application.Current.Dispatcher.Invoke(() => AppendLogLine(line));
            });

            Output += $"[{DateTime.Now:HH:mm:ss}] {Strings.ProcessCompleted}{Environment.NewLine}";
        }
        catch (Exception ex)
        {
            Output += $"[{DateTime.Now:HH:mm:ss}] {Strings.ExecutionError}: {ex.Message}{Environment.NewLine}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void AppendLogLine(string line)
    {
        var match = ProgressRegex().Match(line);
        if (match.Success && double.TryParse(match.Groups[1].Value.Replace(',', '.'), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out var percentage))
        {
            ProgressValue = Math.Clamp(percentage, 0, 100);
        }

        Output += $"[{DateTime.Now:HH:mm:ss}] {line}{Environment.NewLine}";
    }
}
