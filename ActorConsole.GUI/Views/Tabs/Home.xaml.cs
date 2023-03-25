using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views.Tabs
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }




        public string[] PossibleWelcomeTexts { get; } = new string[]
        {
            "Hello",
            "Hello",
            "Hello",
            "Hello",
            "hello...",
            "Hey",
            "hey",
            "...",
            "Help",
            "Hallo",
            "👋",
            "🙋‍♂️"
        };

        public App App => Application.Current as App;
        private void ColorModeButton_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            App.CurrentTheme = App.CurrentTheme == App.Theme.Dark ? App.Theme.Light : App.Theme.Dark;
        }

        private void SelectYourPrecacheButton_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            OpenFileDialog selectPrecache = new OpenFileDialog
            {
                Title = "Select your _precache.gsc",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "gsc",
                Filter = "GSC files (*.gsc)|*.gsc",
                RestoreDirectory = false,
                ReadOnlyChecked = true,
                ShowReadOnly = true,
                Multiselect = false,
            };
            selectPrecache.FileOk += ( object _, CancelEventArgs _e ) =>
            {
                Core.Json.Settings.DefaultInstance.PrecachePath = selectPrecache.FileName;
                return;
            };
            selectPrecache.ShowDialog();
        }
    }
}
