using ActorConsole.GUI.Classes;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views
{
    public partial class ActorBar : UserControl
    {
        public delegate void SelectedActorChanged(object sender, int index, Core.Actor.Actor actor);

        public static event SelectedActorChanged OnSelectedActorChanged;

        public static int SelectedActorIndex { get; private set; } = -1;
        public static Core.Actor.Actor SelectedActor
        {
            get
            {
                try
                {
                    return Core.Actor.Manager.Actors[SelectedActorIndex];
                }
                catch (System.IndexOutOfRangeException)
                {
                    return null;
                }
                catch
                {
                    throw;
                }
            }
        }

        public ActorBar()
        {
            InitializeComponent();
        }
        public static void Reset()
        {
            SelectedActorIndex = -1;
        }
        private void ActorSelectionComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ActorSelectionComboBox.ItemsSource = Core.Actor.Manager.Actors;
            ActorSelectionComboBox.SelectedIndex = SelectedActorIndex;
        }

        private void ActorSelectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedActorIndex = ActorSelectionComboBox.SelectedIndex;
            if (SelectedActor != null)
                OnSelectedActorChanged?.Invoke(this, SelectedActorIndex, SelectedActor);
        }

        private void CreateActorButton_Click(object sender, RoutedEventArgs e)
        {
            if(Core.Memory.IW4.IsInMatch)
            {
                Core.Actor.Manager.Add();
                ActorSelectionComboBox.SelectedIndex = ActorSelectionComboBox.Items.Count;
                ActorSelectionComboBox_Loaded(null, null);
            }
        }

        private void DeleteActorButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedActor != null)
            {
                int selectedIndex = ActorSelectionComboBox.SelectedIndex;
                Core.Actor.Manager.Delete(selectedIndex);
                if (selectedIndex > 0)
                    ActorSelectionComboBox.SelectedIndex = selectedIndex - 1;
                else
                    ActorSelectionComboBox.SelectedIndex = selectedIndex;
            }
            ActorSelectionComboBox_Loaded(null, null);
        }

        private void MoveActorButton_Click(object sender, RoutedEventArgs e)
        {
            if (ActorSelectionComboBox.SelectedIndex > -1)
                Core.Actor.Manager.Actors[ActorSelectionComboBox.SelectedIndex].MoveToCurrentPostition();
        }

        private void PrecacheButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (Objects.Settings.PathToPrecache != null)
            {
                PrecacheButton.Content = "Change Precache";
                PrecacheButton.ToolTip = Objects.Settings.PathToPrecache;
            }
        }

        private void PrecacheButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Add filter to dialog
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".gsc",
                Multiselect = false,
                RestoreDirectory = true,
                Title = "Select Precache",
                Filter = "gsc files (*.gsc)|*.gsc|All files (*.*)|*.*"
            };
            openFileDialog.FileOk += OpenFileDialog_FileOk;
            openFileDialog.ShowDialog();
            void OpenFileDialog_FileOk(object sender2, System.ComponentModel.CancelEventArgs e2)
            {
                Objects.Settings.PathToPrecache = openFileDialog.FileName;
                PrecacheButton.Content = "Change Precache";
                PrecacheButton.ToolTip = Objects.Settings.PathToPrecache;
            }
        }
    }
}