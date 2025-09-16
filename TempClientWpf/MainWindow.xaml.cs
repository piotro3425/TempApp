using System.Net.Http;
using System.Net.Http.Json;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using TempApp.Models;

namespace TempClientWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Timers.Timer timer;

        public MainWindow()
        {
            InitializeComponent();
            timer = new System.Timers.Timer(5000);
            timer.Elapsed += TOnTimedEvent;
            timer.Enabled = false;
        }

        private async void TOnTimedEvent(object? source, ElapsedEventArgs e)
            => await GetTimeFromServer();


        private async void Button_Click(object sender, RoutedEventArgs e)
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
                    await this.TempDisplay.Dispatcher.BeginInvoke(() =>this.TempDisplay.Content = $"{temp.Temperature} °C");
            }
            catch (HttpRequestException)
            {
                this.TempDisplay.Content = "XXX";
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
            => timer.Enabled = true;

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
            => timer.Enabled = false;
    }
}