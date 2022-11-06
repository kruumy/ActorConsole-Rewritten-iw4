using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace ActorConsole.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Classes.Startup.Execute();
        }
        private void LaunchGithubSiteButton_Click(object sender, RoutedEventArgs e)
        {
            var uri = "https://github.com/kruumy/ActorConsole-Rewritten-iw4";
            var p = new ProcessStartInfo();
            p.UseShellExecute = true;
            p.FileName = uri;
            Process.Start(p);
        }
    }
}
