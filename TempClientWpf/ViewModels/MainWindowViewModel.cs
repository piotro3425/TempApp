using OCRRequestor.Commands;
using System.Net.Http;
using System.Net.Http.Json;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using TempApp.Models;

namespace TempClientWpf.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private System.Timers.Timer timer;

        public ICommand GetTempCommand { get; set; }

        public string GetTemperatureValue { get; } =  "Get Temperature";
        public string AutoUpdateValue { get; } =  "Auto update";

        private string tempDisplayValue = "0";
        public string TempDisplayValue
        {
            get => tempDisplayValue;
            set => this.SetProperty(ref tempDisplayValue, value);
        }

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
            timer = new System.Timers.Timer(2000);
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = false;

            GetTempCommand = new RelayCommand(async p => await GetTimeFromServer());
        }

        private async void OnTimedEvent(object? source, ElapsedEventArgs e)
            => await GetTimeFromServer();

        private async Task GetTimeFromServer()
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
