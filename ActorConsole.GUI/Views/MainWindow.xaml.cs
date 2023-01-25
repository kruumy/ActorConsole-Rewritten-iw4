using ControlzEx.Theming;
using MahApps.Metro.Controls;
using System.Diagnostics;
using System.Timers;
using System.Windows;

namespace ActorConsole.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private Timer StatusBarTimer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LaunchGithubSiteButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = "https://github.com/kruumy/ActorConsole-Rewritten-iw4"
            });
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Core.Memory.IW4.IsInMatch)
            {
                if (MessageBoxResult.No == MessageBox.Show("Are you want to close sure?\nAll current actor data will be lost.", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, defaultResult: MessageBoxResult.No))
                {
                    e.Cancel = true;
                }
            }
            StatusBarTimer?.Dispose();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string theme = Classes.Objects.Settings.DarkMode ? "Dark" : "Light";
            ThemeManager.Current.ChangeTheme(this, $"{theme}.{Classes.MetroColorTheme.GetRandomColorTheme()}");

            StatusBarTimer = new Timer(1000);
            StatusBarTimer.Elapsed += (object timerSender, ElapsedEventArgs timerEventArgs) =>
            {
                if (Core.Memory.IW4.IsRunning)
                {
                    string map = Core.Memory.IW4.Map;
                    map = !string.IsNullOrEmpty(map) ? $"Map = {map}" : $"Map = null";
                    Dispatcher.Invoke(() =>
                    {
                        MapLabel.Content = map;
                    });
                    if (!Core.Memory.IW4.IsInMatch)
                    {
                        Core.Actor.Manager.Reset();
                        Views.ActorBar.Reset();
                    }
                }
            };
            StatusBarTimer.Start();
        }
    }
}
