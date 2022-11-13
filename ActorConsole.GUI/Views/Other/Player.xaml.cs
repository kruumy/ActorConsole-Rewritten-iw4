using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            var name = Core.Memory.IW4.PlayerName;
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

            Core.Misc.Player.SetModel((string)((ListViewItem)ModelsBox.SelectedValue).Content, team, PlayerNameBox.Text);
        }
    }
}
