using System.Windows;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views.ActorAttributes.Movement
{
    /// <summary>
    /// Interaction logic for Walking.xaml
    /// </summary>
    public partial class Walking : UserControl
    {
        public Walking()
        {
            InitializeComponent();
        }
        private void BindsDataGrid_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            BindsDataGrid.ItemsSource = Core.ActorManager.Actors;
            // TODO: remove actors that arnt IsMovement_Walking
        }
        private void AddBindBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ActorBar.SelectedActorIndex > -1)
            {
                ActorBar.SelectedActor.Movement_Walking.Key = (char)KeyBox.HotKey.Key;
                if (SpeedBox.Value != null)
                    ActorBar.SelectedActor.Movement_Walking.Speed = (int)SpeedBox.Value;
                BindsDataGrid_Loaded(null, null);
            }
        }

        private void RemoveBindBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
