using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System.Windows.Input;
using TempClientAvalonia.Commands;

namespace TempClientAvalonia.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public string TempDisplayValue { get; } = "0";
        public string GetTempText { get; } = "Get Temp";
        public ICommand ExitCommand { get; set; }

        public MainWindowViewModel()
        {
            ExitCommand = new RelayCommand(p =>
            {
                if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
                    desktopLifetime.Shutdown();
            }, p => true);
        }
    }
}
