using System.Windows.Controls;

namespace ActorConsole.GUI.Views.Tabs
{
    /// <summary>
    /// Interaction logic for Anims.xaml
    /// </summary>
    public partial class Anims : UserControl
    {
        public Anims()
        {
            InitializeComponent();
        }

        private void DeathAnimApply_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            if ( !string.IsNullOrEmpty(DeathAnimTextBox.Text) && DataContext is ActorManager am && am.SelectedActor is Core.Actor actor )
            {
                actor.Anims.Death = DeathAnimTextBox.Text;
            }
        }

        private void DeathAnimListBox_MouseDoubleClick( object sender, System.Windows.Input.MouseButtonEventArgs e )
        {
            if ( DeathAnimListBox.SelectedItem is string item && DataContext is ActorManager am && am.SelectedActor is Core.Actor actor )
            {
                actor.Anims.Death = item;
                DeathAnimListBox.SelectedIndex = -1;
            }
        }

        private void IdleAnimApply_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            if ( !string.IsNullOrEmpty(IdleAnimTextBox.Text) && DataContext is ActorManager am && am.SelectedActor is Core.Actor actor )
            {
                actor.Anims.Idle = IdleAnimTextBox.Text;
            }
        }

        private void IdleAnimListBox_MouseDoubleClick( object sender, System.Windows.Input.MouseButtonEventArgs e )
        {
            if ( IdleAnimListBox.SelectedItem is string item && DataContext is ActorManager am && am.SelectedActor is Core.Actor actor )
            {
                actor.Anims.Idle = item;
                IdleAnimListBox.SelectedIndex = -1;
            }
        }

        private void ReplaceDeathAnimMenuItem_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            if ( SingleplayerAnimListBox.SelectedItem is string item && DataContext is ActorManager am && am.SelectedActor is Core.Actor actor )
            {
                actor.Anims.Death = item;
                SingleplayerAnimListBox.SelectedIndex = -1;
            }
        }

        private void SingleplayerAnimListBox_MouseDoubleClick( object sender, System.Windows.Input.MouseButtonEventArgs e )
        {
            if ( SingleplayerAnimListBox.SelectedItem is string item && DataContext is ActorManager am && am.SelectedActor is Core.Actor actor )
            {
                actor.Anims.Idle = item;
                SingleplayerAnimListBox.SelectedIndex = -1;
            }
        }
    }
}
