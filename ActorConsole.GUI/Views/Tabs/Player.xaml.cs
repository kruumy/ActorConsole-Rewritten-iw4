using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views.Tabs
{
    /// <summary>
    /// Interaction logic for LocalPlayerProperties.xaml
    /// </summary>
    public partial class Player : UserControl
    {
        public Player()
        {
            InitializeComponent();
        }

        public IEnumerable<string> ClassTypes => typeof(Core.Memory.Game.Class).GetEnumNames();
        public IEnumerable<string> KillstreakTypes => Core.Manager.Instance.Game?.KillStreakEnum.GetEnumNames();

        private void ApplyModelButton_Click( object sender, RoutedEventArgs e )
        {
            if ( ClassTypeListBox.SelectedItem is string enumName && Enum.TryParse(enumName, true, out Core.Memory.Game.Class @class) )
            {
                if ( AlliesRadioButton.IsChecked is bool isCheckedAllies && isCheckedAllies )
                {
                    Core.Manager.Instance.Game.SetModel(@class, Core.Memory.Game.Team.allies);
                }
                else if ( AxisRadioButton.IsChecked is bool isCheckedAxis && isCheckedAxis )
                {
                    Core.Manager.Instance.Game.SetModel(@class, Core.Memory.Game.Team.axis);
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
            if ( KillstreakTypeListBox.SelectedItem is string enumName )
            {
                Core.Manager.Instance.Game.GiveKillstreak(enumName);
                KillstreakTypeListBox.SelectedIndex = -1;
            }
        }
    }
}
