using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            Core.Manager.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private void Instance_PropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == nameof(Core.Manager.Game) )
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DoesGameHaveSunSupport)));
            }
        }

        public ActorManager actorManagerProperty => actorManager;
        public bool DoesGameHaveSunSupport => Core.Manager.Instance.Game is Core.Memory.IW4;

        public event PropertyChangedEventHandler PropertyChanged;

        private void TabItem_IsEnabledChanged( object sender, DependencyPropertyChangedEventArgs e )
        {
            if ( e.NewValue is bool b && b == false && sender is TabItem tab )
            {
                tab.IsSelected = false;
            }
        }
    }
}
