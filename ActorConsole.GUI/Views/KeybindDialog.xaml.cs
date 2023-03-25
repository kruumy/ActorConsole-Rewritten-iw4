using ActorConsole.Core.CompositedActorProperties;
using System.Windows;

namespace ActorConsole.GUI.Views
{
    /// <summary>
    /// Interaction logic for KeybindDialog.xaml
    /// </summary>
    public partial class KeybindDialog : Window
    {
        public IKeybindable KeybindingObj { get; }
        public KeybindDialog( IKeybindable KeybindingObj )
        {
            InitializeComponent();
            this.KeybindingObj = KeybindingObj;
        }

        private void DoneButton_Click( object sender, RoutedEventArgs e )
        {
            KeybindingObj.Keybind(KeyTextBox.Text[ 0 ]);
            this.Close();
        }

        private void KeyTextBox_TextChanged( object sender, System.Windows.Controls.TextChangedEventArgs e )
        {
            if ( KeyTextBox.Text.Length > 1 )
            {
                KeyTextBox.Text = string.Empty;
            }
        }
    }
}
