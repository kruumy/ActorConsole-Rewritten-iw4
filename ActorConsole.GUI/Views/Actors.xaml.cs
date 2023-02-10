using ActorConsole.Core.Actor;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views
{
    /// <summary>
    /// Interaction logic for Actors.xaml
    /// TODO: Data bind ItemSource to Core.Manager.Actors
    /// </summary>
    public partial class Actors : UserControl
    {
        public Actors()
        {
            InitializeComponent();
        }

        private void ActorsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            ActorsDataGrid.ItemsSource = Core.Actor.Manager.Actors;
        }

        private void AddActorButton_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Memory.IW4.IsInMatch)
            {
                Core.Actor.Manager.Add();
                ActorBar.SelectedActorIndex = Core.Actor.Manager.Actors.Count - 1;
                ActorsDataGrid_Loaded(null, null);
            }
        }

        private void RemoveActorButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ActorsDataGrid.SelectedIndex;
            if (selectedIndex > -1 && Core.Memory.IW4.IsInMatch)
            {
                Core.Actor.Manager.Delete(selectedIndex);
                ActorsDataGrid_Loaded(null, null);
            }
        }

        private void MoveActorButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ActorsDataGrid.SelectedIndex;
            if (selectedIndex > -1 && Core.Memory.IW4.IsInMatch)
                Core.Actor.Manager.Actors[selectedIndex].MoveToCurrentPostition();
        }

        private void SavePresetButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ActorsDataGrid.SelectedIndex;
            if (selectedIndex > -1 && Core.Memory.IW4.IsInMatch)
            {
                Actor actor = Core.Actor.Manager.Actors[selectedIndex];
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "Save Actor",
                    FileName = actor.Name,
                    DefaultExt = ".json",
                    AddExtension = true,
                    Filter = "json files (*.json)|*.json|All files (*.*)|*.*"
                };
                saveFileDialog.FileOk += SaveFileDialog_FileOk;
                saveFileDialog.ShowDialog();

                void SaveFileDialog_FileOk(object sender2, System.ComponentModel.CancelEventArgs e2)
                {
                    Presets.Save(actor, saveFileDialog.FileName);
                }
            }
        }

        private void LoadPresetButton_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Memory.IW4.IsInMatch)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    DefaultExt = ".json",
                    Multiselect = false,
                    RestoreDirectory = true,
                    Title = "Select Actor",
                    Filter = "json files (*.json)|*.json|All files (*.*)|*.*"
                };
                openFileDialog.FileOk += OpenFileDialog_FileOk;
                openFileDialog.ShowDialog();
                void OpenFileDialog_FileOk(object sender2, System.ComponentModel.CancelEventArgs e2)
                {
                    Presets.Load(openFileDialog.FileName);
                    ActorsDataGrid_Loaded(null, null);
                }
            }
        }

        private void ResetSceneButton_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Memory.IW4.IsInMatch)
            {
                Core.Memory.IW4.SendDvar("actorback");
            }
        }
    }
}