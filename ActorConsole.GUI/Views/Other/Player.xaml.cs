using System.Windows;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views.Other
{
    /// <summary>
    /// Interaction logic for Player.xaml
    /// </summary>
    public partial class Player : UserControl
    {
        public Player()
        {
            InitializeComponent();
        }
        private void PlayerNameBox_Loaded(object sender, RoutedEventArgs e)
        {
            string? name = Core.Memory.IW4.PlayerName;
            if (name != null)
                PlayerNameBox.Text = name;
        }

        private void ApplyModelsButton_Click(object sender, RoutedEventArgs e)
        {
            string team = "";
            if (TeamSwitch.IsOn)
                team = "allies";
            else
                team = "axis";

            Core.Other.Player.SetModel((string)((ListViewItem)ModelsBox.SelectedValue).Content, team, PlayerNameBox.Text);
        }
    }
}
