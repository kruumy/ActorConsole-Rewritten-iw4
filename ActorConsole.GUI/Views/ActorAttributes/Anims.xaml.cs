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

        private void UserControl_Initialized(object sender, System.EventArgs e)
        {
            ActorBar.OnSelectedActorChanged += ActorBar_OnSelectedActorChanged;
        }

        private void ActorBar_OnSelectedActorChanged(object sender, int index, Core.Actor.Actor actor)
        {
            IdleAnimTextBox.Text = actor.Anims.Idle;
            DeathAnimTextBox.Text = actor.Anims.Death;
        }

        private void IdleAnimsBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (Classes.Objects.Settings.IsPrecacheSelected)
                IdleAnimsBox.ItemsSource = Classes.Objects.Precache.MP_Anims_Idle;
        }

        private void DeathAnimsBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (Classes.Objects.Settings.IsPrecacheSelected)
                DeathAnimsBox.ItemsSource = Classes.Objects.Precache.MP_Anims_Death;
        }

        private void IdleAnimsBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IdleAnimTextBox.Text = (string)IdleAnimsBox.SelectedItem;
            applyidlebtn_Click(null, null);
        }

        private void DeathAnimsBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DeathAnimTextBox.Text = (string)DeathAnimsBox.SelectedItem;
            applydeathbtn_Click(null, null);
        }

        private void SinglePlayer_Loaded(object sender, RoutedEventArgs e)
        {
            if (Classes.Objects.Settings.IsPrecacheSelected)
                SinglePlayerAnimsBox.ItemsSource = Classes.Objects.Precache.SP_Anims;
        }

        private void SinglePlayer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SPAnimSwitch.IsOn)
            {
                IdleAnimTextBox.Text = (string)SinglePlayerAnimsBox.SelectedItem;
                applyidlebtn_Click(null, null);
            }
            else
            {
                DeathAnimTextBox.Text = (string)SinglePlayerAnimsBox.SelectedItem;
                applydeathbtn_Click(null, null);
            }
        }

        private void applydeathbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ActorBar.SelectedActorIndex > -1)
                ActorBar.SelectedActor.Anims.Death = DeathAnimTextBox.Text;
        }

        private void applyidlebtn_Click(object sender, RoutedEventArgs e)
        {
            if (ActorBar.SelectedActorIndex > -1)
                ActorBar.SelectedActor.Anims.Idle = IdleAnimTextBox.Text;
        }
    }
}