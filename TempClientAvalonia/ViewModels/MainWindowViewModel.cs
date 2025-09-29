using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
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


        public MainWindowViewModel()
        {
            ExitCommand = new RelayCommand(p =>
            {
                if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
                    desktopLifetime.Shutdown();
            });

            GetTempCommand = new RelayCommand( async p => await GetTemp(p));
        }

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
