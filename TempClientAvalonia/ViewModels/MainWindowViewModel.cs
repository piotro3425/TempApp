using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using TempApp.Models;
using TempClientAvalonia.Commands;

namespace TempClientAvalonia.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private string tempDisplayValue = "0";

        public string TempDisplayValue
        {
            get => tempDisplayValue;
            set => this.SetProperty(ref tempDisplayValue, value);
        }
        public string GetTempText { get; } = "Get Temp";
        public ICommand ExitCommand { get; set; }
        public ICommand GetTempCommand { get; set; }
        private System.Timers.Timer timer;
        private bool isTimerOn;

        public bool IsTimerOn
        {
            get => isTimerOn;
            set
            {
                this.SetProperty(ref isTimerOn, value);
                this.timer.Enabled = value;
            }
        }
        public MainWindowViewModel()
        {
            ExitCommand = new RelayCommand(p =>
            {
                if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
                    desktopLifetime.Shutdown();
            });

            GetTempCommand = new RelayCommand( async p => await GetTemp(p));

            timer = new System.Timers.Timer(2000);
            timer.Elapsed += OnTimedEvent;
            IsTimerOn = false;
        }

        private async void OnTimedEvent(object? sender, ElapsedEventArgs e)=> await GetTempFromServer();

        private async Task GetTemp(object? obj) => await GetTempFromServer();

        private async Task GetTempFromServer()
        {
            try
            {
                HttpClient http = new HttpClient();
                HttpResponseMessage res = await http.GetAsync("https://localhost:7182/api/Temp");
                res.EnsureSuccessStatusCode();
                Temp? temp = await res.Content.ReadFromJsonAsync<Temp>();

                if (temp is not null)
                    this.TempDisplayValue = $"{temp.Temperature} °C";
            }
            catch (HttpRequestException)
            {
                this.TempDisplayValue = "XXX";
            }
        }
    }
}
