using ActorConsole.GUI.Classes;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ActorConsole.GUI.Views
{
    public partial class ActorBar : UserControl
    {
        public static int SelectedActorIndex { get; private set; } = -1;
        public static Core.Actor.Actor SelectedActor => Core.ActorManager.Actors[SelectedActorIndex];

        public ActorBar()
        {
            InitializeComponent();
        }
        private void ActorSelectionComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ActorSelectionComboBox.ItemsSource = Core.ActorManager.Actors;
            ActorSelectionComboBox.SelectedIndex = SelectedActorIndex;
        }
        private void ActorSelectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedActorIndex = ActorSelectionComboBox.SelectedIndex;
        }
        private void CreateActorButton_Click(object sender, RoutedEventArgs e)
        {
            Core.ActorManager.Add();
            ActorSelectionComboBox.SelectedIndex = ActorSelectionComboBox.Items.Count;
            ActorSelectionComboBox_Loaded(null, null);
        }
        private void DeleteActorButton_Click(object sender, RoutedEventArgs e)
        {
            if (ActorSelectionComboBox.SelectedIndex > -1)
            {
                int selectedIndex = ActorSelectionComboBox.SelectedIndex;
                Core.ActorManager.Delete(selectedIndex);
                if (selectedIndex > 0)
                    ActorSelectionComboBox.SelectedIndex = selectedIndex - 1;
                else
                    ActorSelectionComboBox.SelectedIndex = selectedIndex;
            }
            ActorSelectionComboBox_Loaded(null,null);
        }
        private void MoveActorButton_Click(object sender, RoutedEventArgs e)
        {
            if (ActorSelectionComboBox.SelectedIndex > -1)
                Core.ActorManager.Actors[ActorSelectionComboBox.SelectedIndex].MoveToCurrentPostition();
        }
        private void PrecacheButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (Objects.Settings.Path_To_Precache != null)
            {
                PrecacheButton.Content = "Change Precache";
                PrecacheButton.ToolTip = Objects.Settings.Path_To_Precache;
            }
        }
        private void PrecacheButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Add filter to dialog
            OpenFileDialog openFileDialog = new();
            openFileDialog.DefaultExt = ".gsc";
            openFileDialog.Multiselect = false;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "Select Precache";
            openFileDialog.FileOk += OpenFileDialog_FileOk;
            openFileDialog.ShowDialog();
            void OpenFileDialog_FileOk(object? sender, System.ComponentModel.CancelEventArgs e)
            {
                Objects.Settings.Path_To_Precache = openFileDialog.FileName;
                PrecacheButton.Content = "Change Precache";
                PrecacheButton.ToolTip = Objects.Settings.Path_To_Precache;
            }
        }
    }
}
