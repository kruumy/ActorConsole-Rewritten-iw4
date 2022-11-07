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

namespace ActorConsole.GUI.Views.ActorAttributes
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void IdleAnimsBox_Loaded(object sender, RoutedEventArgs e)
        {
            IdleAnimsBox.Items.Clear();
            foreach (string anims in Classes.Objects.Precache.MP_Anims_Idle)
            {
                IdleAnimsBox.Items.Add(anims);
            }
        }
        private void DeathAnimsBox_Loaded(object sender, RoutedEventArgs e)
        {
            DeathAnimsBox.Items.Clear();
            foreach (string anims in Classes.Objects.Precache.MP_Anims_Death)
            {
                DeathAnimsBox.Items.Add(anims);
            }
        }

        private void IdleAnimsBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ActorBar.SelectedActorIndex > -1)
                ActorBar.SelectedActor.Anims.Idle = IdleAnimsBox.SelectedItem.ToString();
        }

        private void DeathAnimsBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ActorBar.SelectedActorIndex > -1)
                ActorBar.SelectedActor.Anims.Death = DeathAnimsBox.SelectedItem.ToString();
        }

        private void DeathAnimsBox_MouseEnter(object sender, MouseEventArgs e)
        {
            DeathAnimsBox_Loaded(null, null);
        }

        private void IdleAnimsBox_MouseEnter(object sender, MouseEventArgs e)
        {
            IdleAnimsBox_Loaded(null, null);
        }
    }
}
