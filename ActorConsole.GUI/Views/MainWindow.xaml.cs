using ControlzEx.Theming;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            ThemeManager.Current.ChangeTheme(this, $"Dark.{Classes.MetroColorTheme.GetRandomColorTheme()}");

            Classes.Startup.Execute();
            Task.Run(StatusBarUpdatingLoop);
        }

        private void StatusBarUpdatingLoop()
        {
            while (true)
            {
                try
                {
                    if (Core.Memory.IW4.IsRunning)
                    {
                        string? map = Core.Memory.IW4.Map;
                        Dispatcher.Invoke(() =>
                        {

                            DvarQueueLabel.Content = $"Dvar Queue = {Core.Memory.SendDvarQueue.Count}";
                            if (map != null)
                                MapLabel.Content = $"Map = {map.Split('_')[1]}";
                            else
                                MapLabel.Content = $"Map = null";
                        });
                    }

                    Classes.Startup.StatusBar_Update();
                }
                catch { }
                System.Threading.Thread.Sleep(1000);
            }
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
