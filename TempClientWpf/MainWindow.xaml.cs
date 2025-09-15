using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using TempApp.Models;

namespace TempClientWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpClient http = new HttpClient();
                HttpResponseMessage res = await http.GetAsync("https://localhost:7182/api/Temp");
                res.EnsureSuccessStatusCode();
                Temp? temp = await res.Content.ReadFromJsonAsync<Temp>();

                if (temp is not null)
                    this.TempDisplay.Content = $"{temp.Temperature} °C";
            }
            catch(HttpRequestException ex)
            {
                this.TempDisplay.Content = "XXX";
            }
        }
    }
}