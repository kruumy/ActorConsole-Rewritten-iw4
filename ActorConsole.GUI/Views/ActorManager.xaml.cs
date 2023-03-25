using ActorConsole.Core;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views
{
    /// <summary>
    /// Interaction logic for ActorManager.xaml
    /// </summary>
    public partial class ActorManager : UserControl, INotifyPropertyChanged
    {
        public ActorManager()
        {
            InitializeComponent();

            ManagerReseter?.Dispose();
            ManagerReseter = new Timer(5000);
            ManagerReseter.Elapsed += ManagerReseter_Elapsed;
            ManagerReseter.Start();
        }

        private void ManagerReseter_Elapsed( object sender, ElapsedEventArgs e )
        {
            if ( !Core.Memory.IW4.IsRunning && Manager.Actors.Count > 0 )
            {
                Manager.Reset();
                Console.WriteLine("Cleared Manager");
            }
        }

        public static Timer ManagerReseter { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Actor> Actors => Manager.Actors;

        public Actor SelectedActor  // TODO could mirror SelectedActorComboBox field to prop so ActorManager doesnt need to implement inotepropchanged
        {
            get => SelectedActorComboBox.SelectedItem as Actor;
            set => SelectedActorComboBox.SelectedItem = value;
        }

        public Core.Json.Settings Settings => Core.Json.Settings.DefaultInstance;

        private void ActorBackButton_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            Manager.ActorBack();
        }

        private void DeleteActorButton_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            Actor toDelete = SelectedActor;
            SelectedActorComboBox.SelectedItem = Manager.Actors.FirstOrDefault(act => act != SelectedActor);
            Manager.RemoveActor(toDelete);
        }

        private void MoveActorButton_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            SelectedActor.MoveToLocalPlayer();
        }

        private void SelectedActorComboBox_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedActor)));
        }

        private void SpawnActorButton_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            if ( Core.Memory.IW4.IsRunning && Core.Memory.LocalPlayer.HasSpawned )
            {
                Manager.SpawnActor();
                SelectedActorComboBox.SelectedItem = Manager.Actors.LastOrDefault();
            }
            else
            {
                MessageBox.Show("Please be in a game before spawning an actor.", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }
    }
}
