using System.Windows;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views.Other
{
    /// <summary>
    /// Interaction logic for KillStreaks.xaml
    /// </summary>
    public partial class KillStreaks : UserControl
    {
        public KillStreaks()
        {
            InitializeComponent();
        }

        private void GiveKillStreakBtn_Click(object sender, RoutedEventArgs e)
        {
            if (KillStreakComboBox.SelectedIndex > -1)
                Core.Misc.KillStreak.Give(KillStreakComboBox.Text);
        }
    }
}
