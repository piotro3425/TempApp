using System.Net.Http;
using System.Net.Http.Json;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using TempApp.Models;
using TempClientWpf.ViewModels;

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
            DataContext = new MainWindowViewModel();
        }
    }
}