using ControlzEx.Theming;
using MahApps.Metro.Controls;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
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
            StatusBarUpdatingLoop();
        }

        private Task StatusBarUpdatingLoop()
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        if (Core.Memory.IW4.IsRunning)
                        {
                            string map = Core.Memory.IW4.Map;
                            Dispatcher.Invoke(() =>
                            {

                                DvarQueueLabel.Content = $"Dvar Queue = {Core.Memory.SendDvarQueue.Count}";
                                if (map != null)
                                    MapLabel.Content = $"Map = {map}";
                                else
                                    MapLabel.Content = $"Map = null";
                            });
                        }
                        Classes.Startup.StatusBar_Update();
                    }
                    catch { }
                    System.Threading.Thread.Sleep(1000);
                }
            });
        }
        private void LaunchGithubSiteButton_Click(object sender, RoutedEventArgs e)
        {
            string uri = "https://github.com/kruumy/ActorConsole-Rewritten-iw4";
            ProcessStartInfo p = new ProcessStartInfo();
            p.UseShellExecute = true;
            p.FileName = uri;
            Process.Start(p);
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Core.Memory.IW4.IsRunning)
            {
                MessageBoxResult result = MessageBox.Show("", "", MessageBoxButton.YesNo, MessageBoxImage.Warning, defaultResult: MessageBoxResult.No);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
