using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
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
    /// Interaction logic for ActorBar.xaml
    /// TODO: Please i need to do databinding and rework the whole globalselectingindex thing
    /// </summary>
    public partial class ActorBar : UserControl
    {
        public ActorBar()
        {
            InitializeComponent();
        }
        private static int GlobalSelectedIndex = 0;
        private void PrecacheButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.DefaultExt = ".gsc";
            openFileDialog.Multiselect = false;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "Select Precache";
            openFileDialog.ShowDialog();
            if (File.Exists(openFileDialog.FileName))
            {
                Classes.Precache.FromPrecachePath(openFileDialog.FileName);
                PrecacheButton.Content = "Change Precache";
                PrecacheButton.ToolTip = Classes.Precache._Precache.Path;
            }
        }

        private void RefreshActorSelectionComboBox()
        {
            int selectedIndex = ActorSelectionComboBox.SelectedIndex;
            ActorSelectionComboBox.Items.Clear();
            foreach (var actor in Core.ActorManager.Actors)
            {
                ActorSelectionComboBox.Items.Add(actor.Name);
            }
            ActorSelectionComboBox.SelectedIndex = selectedIndex;
        }

        private void ActorSelectionComboBox_MouseEnter(object sender, MouseEventArgs e)
        {
            RefreshActorSelectionComboBox();
        }

        private void CreateActorButton_Click(object sender, RoutedEventArgs e)
        {
            Core.ActorManager.Add();
            RefreshActorSelectionComboBox();
            ActorSelectionComboBox.SelectedIndex = ActorSelectionComboBox.Items.Count - 1;
            GlobalSelectedIndex = ActorSelectionComboBox.SelectedIndex;
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
            RefreshActorSelectionComboBox();
            GlobalSelectedIndex = ActorSelectionComboBox.SelectedIndex;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Classes.Precache._Precache != null)
            {
                PrecacheButton.Content = "Change Precache";
                PrecacheButton.ToolTip = Classes.Precache._Precache.Path;
            }
            RefreshActorSelectionComboBox();
            ActorSelectionComboBox.SelectedIndex = GlobalSelectedIndex;
        }

        private void ActorSelectionComboBox_MouseLeave(object sender, MouseEventArgs e)
        {
            GlobalSelectedIndex = ActorSelectionComboBox.SelectedIndex;
        }
    }
}
