using ActorConsole.Core;
using ActorConsole.Core.Memory;
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
            ManagerReseter = new Timer(2000);
            ManagerReseter.Elapsed += ManagerReseter_Elapsed;
            ManagerReseter.Start();
        }

        private void ManagerReseter_Elapsed( object sender, ElapsedEventArgs e )
        {
            if ( (!Game.IsAnyGameRunning() || !Core.Manager.Instance.Game.LocalPlayerIsSpawned) && Manager.Instance.Actors.Count > 0 )
            {
                Application.Current.Dispatcher.Invoke(Manager.Instance.Reset);
                Console.WriteLine("Cleared Manager");
            }
        }

        public static Timer ManagerReseter { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Actor> Actors => Manager.Instance.Actors;

        public Actor SelectedActor  // TODO could mirror SelectedActorComboBox field to prop so ActorManager doesnt need to implement inotepropchanged
        {
            get => SelectedActorComboBox.SelectedItem as Actor;
            set => SelectedActorComboBox.SelectedItem = value;
        }

        public Core.Json.Settings Settings => Core.Json.Settings.DefaultInstance;
        public Manager Manager => Core.Manager.Instance;

        private void ActorBackButton_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            Manager.Instance.ActorBack();
        }

        private void DeleteActorButton_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            Actor toDelete = SelectedActor;
            SelectedActorComboBox.SelectedItem = Manager.Instance.Actors.FirstOrDefault(act => act != SelectedActor);
            Manager.Instance.RemoveActor(toDelete);
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
            Manager.Instance.SpawnActor();
            SelectedActorComboBox.SelectedItem = Manager.Instance.Actors.LastOrDefault();
        }

        private void ForceSpawnActorEnableMenuItem_Click( object sender, RoutedEventArgs e )
        {
            SpawnActorButton.IsEnabled = true;
        }
    }
}
