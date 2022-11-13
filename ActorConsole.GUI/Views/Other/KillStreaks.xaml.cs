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
