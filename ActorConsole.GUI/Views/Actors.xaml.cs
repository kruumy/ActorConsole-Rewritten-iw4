using ActorConsole.Core.Actor;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views
{
    /// <summary>
    /// Interaction logic for Actors.xaml
    /// TODO: Data bind ItemSource to Core.ActorManager.Actors
    /// </summary>
    public partial class Actors : UserControl
    {
        public Actors()
        {
            InitializeComponent();
        }
        private void ActorsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            ActorsDataGrid.ItemsSource = Core.ActorManager.Actors;
        }
        private void AddActorButton_Click(object sender, RoutedEventArgs e)
        {
            Core.ActorManager.Add();
            ActorsDataGrid_Loaded(null, null);
        }
        private void RemoveActorButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ActorsDataGrid.SelectedIndex;
            if (selectedIndex > -1)
            {
                Core.ActorManager.Delete(selectedIndex);
                ActorsDataGrid_Loaded(null, null);
            }
        }
        private void MoveActorButton_Click(object sender, RoutedEventArgs e)
        {
            if (ActorsDataGrid.SelectedIndex > -1)
                Core.ActorManager.Actors[ActorsDataGrid.SelectedIndex].MoveToCurrentPostition();
        }
        private void SavePresetButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ActorsDataGrid.SelectedIndex;
            if (selectedIndex > -1)
            {
                //TODO: Add filter to dialog
                Actor actor = Core.ActorManager.Actors[selectedIndex];
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Save Actor";
                saveFileDialog.FileName = actor.Name;
                saveFileDialog.DefaultExt = ".json";
                saveFileDialog.AddExtension = true;
                saveFileDialog.FileOk += SaveFileDialog_FileOk;
                saveFileDialog.ShowDialog();

                void SaveFileDialog_FileOk(object? sender, System.ComponentModel.CancelEventArgs e)
                {
                    Core.Presets.Save(actor, saveFileDialog.FileName);
                }
            }
        }
        private void LoadPresetButton_Click(object sender, RoutedEventArgs e)
        {
            if (Core.Memory.IW4.IsInGame)
            {
                //TODO: Add filter to dialog
                OpenFileDialog openFileDialog = new();
                openFileDialog.DefaultExt = ".json";
                openFileDialog.Multiselect = false;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Select Actor";
                openFileDialog.FileOk += OpenFileDialog_FileOk;
                openFileDialog.ShowDialog();
                void OpenFileDialog_FileOk(object? sender, System.ComponentModel.CancelEventArgs e)
                {
                    Core.Presets.Load(openFileDialog.FileName);
                }
                ActorsDataGrid_Loaded(null, null);
            }

        }
    }
}
