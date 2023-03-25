using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views.Tabs
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

        public IEnumerable<string> ClassTypes => typeof(Core.Memory.LocalPlayer.Class).GetEnumNames();
        public IEnumerable<string> KillstreakTypes => typeof(Core.Memory.LocalPlayer.Killstreak).GetEnumNames();

        private void ApplyModelButton_Click( object sender, RoutedEventArgs e )
        {
            if ( ClassTypeListBox.SelectedItem is string enumName && Enum.TryParse(enumName, true, out Core.Memory.LocalPlayer.Class @class) )
            {
                if ( AlliesRadioButton.IsChecked is bool isCheckedAllies && isCheckedAllies )
                {
                    Core.Memory.LocalPlayer.SetModel(@class, Core.Memory.LocalPlayer.Team.Allies);
                }
                else if ( AxisRadioButton.IsChecked is bool isCheckedAxis && isCheckedAxis )
                {
                    Core.Memory.LocalPlayer.SetModel(@class, Core.Memory.LocalPlayer.Team.Axis);
                }
                ClassTypeListBox.SelectedIndex = -1;
            }
        }

        private void ClassTypeListBox_MouseDoubleClick( object sender, System.Windows.Input.MouseButtonEventArgs e )
        {
            ApplyModelButton_Click(sender, e);
        }

        private void KillstreakTypeListBox_MouseDoubleClick( object sender, System.Windows.Input.MouseButtonEventArgs e )
        {
            GiveKillstreakButton_Click(sender, e);
        }

        private void GiveKillstreakButton_Click( object sender, RoutedEventArgs e )
        {
            if ( KillstreakTypeListBox.SelectedItem is string enumName && Enum.TryParse(enumName, true, out Core.Memory.LocalPlayer.Killstreak killstreak) )
            {
                Core.Memory.LocalPlayer.GiveKillstreak(killstreak);
                KillstreakTypeListBox.SelectedIndex = -1;
            }
        }
    }
}
