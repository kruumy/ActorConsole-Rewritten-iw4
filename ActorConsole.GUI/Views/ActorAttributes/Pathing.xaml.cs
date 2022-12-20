using System.Windows.Controls;

namespace ActorConsole.GUI.Views.ActorAttributes
{
    /// <summary>
    /// Interaction logic for Pathing.xaml
    /// </summary>
    public partial class Pathing : UserControl
    {
        public Pathing()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, System.EventArgs e)
        {
            ActorBar.OnSelectedActorChanged += ActorBar_OnSelectedActorChanged;
        }

        private void ActorBar_OnSelectedActorChanged(object sender, int index, Core.Actor.Actor actor)
        {
            PointsDataGrid.Items.Clear();
            for (int i = 0; i < actor.Pathing.NodeCount; i++)
            {
                PointsDataGrid.Items.Add(new
                {
                    Name = actor.Name,
                    Point = i
                });
            }
            SpeedBox.Value = actor.Pathing.Speed;
        }

        private void AddPointBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ActorBar.SelectedActor != null)
            {
                ActorBar.SelectedActor.Pathing.CreateNode();
                ActorBar_OnSelectedActorChanged(this, ActorBar.SelectedActorIndex, ActorBar.SelectedActor);
            }
        }

        private void RemovePointBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ActorBar.SelectedActor != null)
            {
                ActorBar.SelectedActor.Pathing.DeleteLastNode();
                ActorBar_OnSelectedActorChanged(this, ActorBar.SelectedActorIndex, ActorBar.SelectedActor);
            }
        }

        private void PlayBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ActorBar.SelectedActor?.Pathing.Play();
        }

        private void SpeedBox_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double?> e)
        {
            if (SpeedBox.Value > -1 && ActorBar.SelectedActor != null)
            {
                ActorBar.SelectedActor.Pathing.Speed = (uint)SpeedBox.Value;
            }
        }
    }
}