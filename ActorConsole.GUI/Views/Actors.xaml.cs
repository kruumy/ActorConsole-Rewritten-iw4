using ActorConsole.Core.Actor;
using ActorConsole.GUI.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        private void ActorsDataGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            RefreshDataGrid();
        }

        private void AddActorButton_Click(object sender, RoutedEventArgs e)
        {
            Core.ActorManager.Add();
            RefreshDataGrid();
        }
        private void RemoveActorButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ActorsDataGrid.SelectedIndex;
            if (selectedIndex > -1)
            {
                Core.ActorManager.Delete(selectedIndex);
                RefreshDataGrid();
            }
        }

        private void MoveActorButton_Click(object sender, RoutedEventArgs e)
        {
            if (ActorsDataGrid.SelectedIndex > -1)
                Core.ActorManager.Actors[ActorsDataGrid.SelectedIndex].MoveToCurrentPostition();
        }
        private void RefreshDataGrid()
        {
            ActorsDataGrid.Items.Clear();
            foreach (var actor in Core.ActorManager.Actors)
            {
                ActorsDataGrid.Items.Add(actor);
            }
        }

        private void SavePresetButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ActorsDataGrid.SelectedIndex;
            if (selectedIndex > -1)
            {
                //TODO: Add filter to dialog
                var actor = Core.ActorManager.Actors[selectedIndex];
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
            if (Core.Memory.IW4.InGame)
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
            }

        }

        private void SpawnActorContextMenu_Click(object sender, RoutedEventArgs e)
        {
            AddActorButton_Click(null, null);
        }


    }
}
