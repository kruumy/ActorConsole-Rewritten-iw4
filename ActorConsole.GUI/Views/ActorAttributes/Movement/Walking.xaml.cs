﻿using System.Windows.Controls;

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
            BindsDataGrid.Items.Clear();
            foreach (Core.Actor.Actor actor in Core.Actor.Manager.Actors)
            {
                if (actor.IsMovement_Walking)
                {
                    BindsDataGrid.Items.Add(actor);
                }
            }
        }
        private void AddBindBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ActorBar.SelectedActorIndex > -1)
            {
                ActorBar.SelectedActor.Movement_Walking.Key = KeyBox.Text[0];
                if (SpeedBox.Value != null)
                    ActorBar.SelectedActor.Movement_Walking.Speed = (int)SpeedBox.Value;
                BindsDataGrid_Loaded(null, null);
            }
        }

        private void RemoveBindBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ActorBar.SelectedActorIndex > -1)
            {
                ActorBar.SelectedActor.Movement_Walking.RemoveBind();
                BindsDataGrid_Loaded(null, null);
            }
        }
    }
}
