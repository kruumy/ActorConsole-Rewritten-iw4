using System.ComponentModel;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views.Tabs
{
    /// <summary>
    /// Interaction logic for Models.xaml
    /// </summary>
    public partial class Models : UserControl, INotifyPropertyChanged
    {
        public Models()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string CurrentMap => Core.Memory.LocalPlayer.Properties.Map;

        public Core.Json.Models.Map CurrentModels => Core.Json.Models.Element.TryGetValue(CurrentMapTextBox.Text, out Core.Json.Models.Map models) ? models : Core.Json.Models.Map.Empty;

        private void BodyModelApply_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            if ( !string.IsNullOrEmpty(BodyModelTextBox.Text) && DataContext is ActorManager am && am.SelectedActor is Core.Actor actor )
            {
                actor.Models.Body = BodyModelTextBox.Text;
            }
        }

        private void BodyModelListBox_MouseDoubleClick( object sender, System.Windows.Input.MouseButtonEventArgs e )
        {
            if ( BodyModelListBox.SelectedItem is string item && DataContext is ActorManager am && am.SelectedActor is Core.Actor actor )
            {
                actor.Models.Body = item;
                BodyModelListBox.SelectedIndex = -1;
            }
        }

        private void CurrentMapTextBox_TextChanged( object sender, TextChangedEventArgs e )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentModels)));
        }

        private void HeadModelApply_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            if ( !string.IsNullOrEmpty(HeadModelTextBox.Text) && DataContext is ActorManager am && am.SelectedActor is Core.Actor actor )
            {
                actor.Models.Head = HeadModelTextBox.Text;
            }
        }

        private void HeadModelListBox_MouseDoubleClick( object sender, System.Windows.Input.MouseButtonEventArgs e )
        {
            if ( HeadModelListBox.SelectedItem is string item && DataContext is ActorManager am && am.SelectedActor is Core.Actor actor )
            {
                actor.Models.Head = item;
                HeadModelListBox.SelectedIndex = -1;
            }
        }

        private void KillstreakModelListBox_MouseDoubleClick( object sender, System.Windows.Input.MouseButtonEventArgs e )
        {
            if ( KillstreakModelListBox.SelectedItem is ListBoxItem lbi && lbi.Content is string item && DataContext is ActorManager am && am.SelectedActor is Core.Actor actor )
            {
                actor.Models.Set(item, string.Empty);
                KillstreakModelListBox.SelectedIndex = -1;
            }
        }

        private void RefreshButton_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentMap)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentModels)));
        }

        private void SingleplayerModelListBox_MouseDoubleClick( object sender, System.Windows.Input.MouseButtonEventArgs e )
        {
            if ( SingleplayerModelListBox.SelectedItem is string item && DataContext is ActorManager am && am.SelectedActor is Core.Actor actor )
            {
                if ( item.Contains("head") )
                {
                    actor.Models.Head = item;
                }
                else
                {
                    actor.Models.Body = item;
                }
                SingleplayerModelListBox.SelectedIndex = -1;
            }
        }

        private void UserControl_Loaded( object sender, System.Windows.RoutedEventArgs e )
        {
            RefreshButton_Click(sender, e);
        }
    }
}
