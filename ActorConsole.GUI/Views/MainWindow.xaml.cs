using System.Windows;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views
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

        public ActorManager actorManagerProperty => actorManager;

        private void TabItem_IsEnabledChanged( object sender, DependencyPropertyChangedEventArgs e )
        {
            if ( e.NewValue is bool b && b == false && sender is TabItem tab )
            {
                tab.IsSelected = false;
            }
        }
    }
}
