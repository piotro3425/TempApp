using System.Net.Http.Json;
using System.Timers;
using TempApp.Models;

namespace TempClientMaui
{
    public partial class MainPage : ContentPage
    {
        System.Timers.Timer timer1;

        public MainPage()
        {
            InitializeComponent();
            this.timer1 = new System.Timers.Timer(2000);
            this.timer1.Elapsed += TOnTimedEvent;
            this.timer1.Enabled = false;
        }

        private async void TOnTimedEvent(object? source, ElapsedEventArgs e)
            => await GetTimeFromServer();



        private async Task GetTimeFromServer()
        {
            try
            {
                HttpClient http = new HttpClient();
                HttpResponseMessage res = await http.GetAsync("http://192.168.100.194:5100/api/Temp");
                res.EnsureSuccessStatusCode();
                Temp? temp = await res.Content.ReadFromJsonAsync<Temp>();

                if (temp is not null)
                    this.TempDisplay.Dispatcher.Dispatch(() => this.TempDisplay.Text = $"{temp.Temperature} °C");
            }
            catch (Exception ex)
            {
                this.TempDisplay.Dispatcher.Dispatch(() =>
                {
                    this.TempDisplay.Text = ex.Message;
                    this.TempDisplay.FontSize = 12;
                });
            }
        }

        private async void OnCounterClicked(object sender, EventArgs e)
            => await GetTimeFromServer();

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox? checkBox = sender as CheckBox;
            if(checkBox is not null)
            {
                if (checkBox.IsChecked)
                    this.timer1.Enabled = true;
                else
                    this.timer1.Enabled = false;
            }
        }
    }
}
