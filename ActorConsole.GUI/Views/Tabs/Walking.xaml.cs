using System.Collections.Generic;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views.Tabs
{
    /// <summary>
    /// Interaction logic for Walking.xaml
    /// </summary>
    public partial class Walking : UserControl
    {
        public Walking()
        {
            InitializeComponent();
        }

        public IEnumerable<string> DirectionNames => typeof(Core.CompositedActorProperties.Walking.DirectionType).GetEnumNames();

        private void PlayWalkBtn_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            if ( ActorGrid.SelectedItem is Core.Actor actor )
            {
                actor.Walking.Play();
            }
        }

        private void BindMenuItem_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            if ( ActorGrid.SelectedItem is Core.Actor actor )
            {
                KeybindDialog keybindWindow = new Views.KeybindDialog(actor.Walking);
                keybindWindow.ShowDialog();
            }

        }
    }
}
