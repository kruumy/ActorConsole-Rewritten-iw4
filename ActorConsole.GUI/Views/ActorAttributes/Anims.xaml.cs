using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            if (Classes.Objects.Settings.IsPrecacheSelected)
            {
                foreach (string anims in Classes.Objects.Precache.MP_Anims_Idle)
                {
                    IdleAnimsBox.Items.Add(anims);
                }
            }
            else
            {
                IdleAnimsBox.Items.Add("No Precache Selected");
            }

        }
        private void DeathAnimsBox_Loaded(object sender, RoutedEventArgs e)
        {
            DeathAnimsBox.Items.Clear();
            if (Classes.Objects.Settings.IsPrecacheSelected)
            {
                foreach (string anims in Classes.Objects.Precache.MP_Anims_Death)
                {
                    DeathAnimsBox.Items.Add(anims);
                }
            }
            else
            {
                DeathAnimsBox.Items.Add("No Precache Selected");
            }

        }

        private void IdleAnimsBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ActorBar.SelectedActorIndex > -1 && Classes.Objects.Settings.IsPrecacheSelected)
                ActorBar.SelectedActor.Anims.Idle = IdleAnimsBox.SelectedItem.ToString();
        }

        private void DeathAnimsBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ActorBar.SelectedActorIndex > -1 && Classes.Objects.Settings.IsPrecacheSelected)
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

        private void SinglePlayer_Loaded(object sender, RoutedEventArgs e)
        {
            SinglePlayerAnimsBox.Items.Clear();
            if (Classes.Objects.Settings.IsPrecacheSelected)
            {
                foreach (string anims in Classes.Objects.Precache.SP_Anims)
                {
                    SinglePlayerAnimsBox.Items.Add(anims);
                }
            }
            else
            {
                SinglePlayerAnimsBox.Items.Add("No Precache Selected");
            }
        }

        private void SinglePlayer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void SinglePlayer_MouseEnter(object sender, MouseEventArgs e)
        {
            SinglePlayer_Loaded(null, null);
        }
    }
}
