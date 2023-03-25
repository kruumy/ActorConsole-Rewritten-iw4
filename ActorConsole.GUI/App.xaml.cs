using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;

namespace ActorConsole.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {
        private void Application_DispatcherUnhandledException( object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e )
        {
#if RELEASE
            string ErrorDumpPath = System.IO.Path.Combine(Environment.CurrentDirectory, $"error_{DateTime.Now:yyyy-dd-M--HH-mm-ss}.txt");
            System.IO.File.WriteAllText(ErrorDumpPath, e.Exception.ToString());
            MessageBox.Show($"Error:\n\n{e.Exception.Message}\n\nWrote full error details to:\n\n\"{ErrorDumpPath}\"", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
#endif
        }

        [DllImport("Kernel32")]
        private static extern void AllocConsole();

        [DllImport("Kernel32", SetLastError = true)]
        private static extern void FreeConsole();

        public enum Theme : uint
        {
            Light,
            Dark
        }

        public Theme CurrentTheme
        {
            get => (Theme)Resources[ "CurrentTheme" ];
            set
            {
                Resources[ "CurrentTheme" ] = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentTheme)));
                string mainUri = value == Theme.Light ? LightTheme : DarkTheme;
                string colorPickerUri = value == Theme.Light ? ColorPickerExtraLib_LightBrushes : ColorPickerExtraLib_DarkBrushes;
                while ( Resources.MergedDictionaries.Count > 0 )
                {
                    try
                    {
                        Resources.MergedDictionaries.RemoveAt(0);
                    }
                    catch ( ArgumentOutOfRangeException )
                    {
                        break;
                    }
                }
                Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(colorPickerUri, UriKind.Absolute) });
                Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(ColorPickerExtraLib_CustomTheme, UriKind.Absolute) });
                Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(mainUri, UriKind.Relative) });
            }
        }

        public const string ColorPickerExtraLib_LightBrushes = "pack://application:,,,/ColorPickerExtraLib;component/Themes/LightBrushes.xaml";
        public const string ColorPickerExtraLib_DarkBrushes = "pack://application:,,,/ColorPickerExtraLib;component/Themes/DarkBrushes.xaml";
        public const string ColorPickerExtraLib_CustomTheme = "pack://application:,,,/ColorPickerExtraLib;component/Themes/CustomTheme.xaml";
        public const string LightTheme = "/Themes/ColourfulLightTheme.xaml";
        public const string DarkTheme = "/Themes/ColourfulDarkTheme.xaml";

        public event PropertyChangedEventHandler PropertyChanged;

        protected override void OnStartup( StartupEventArgs e )
        {
            base.OnStartup(e);
            CurrentTheme = Theme.Dark;
            if ( Environment.GetCommandLineArgs().Any(a => a == "-console") ) // TODO possibly add real cli parsing
            {
                AllocConsole();
            }
        }
        protected override void OnExit( ExitEventArgs e )
        {
            base.OnExit(e);
            if ( Environment.GetCommandLineArgs().Any(a => a == "-console") ) // TODO possibly add real cli parsing
            {
                FreeConsole();
            }
        }
    }
}
