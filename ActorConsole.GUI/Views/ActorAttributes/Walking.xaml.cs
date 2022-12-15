using System.Windows.Controls;

namespace ActorConsole.GUI.Views.ActorAttributes
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

        private void UserControl_Initialized(object sender, System.EventArgs e)
        {
            ActorBar.OnSelectedActorChanged += ActorBar_OnSelectedActorChanged;
        }

        private void ActorBar_OnSelectedActorChanged(object sender, int index, Core.Actor.Actor actor)
        {
            if (actor.Walking.IsEnabled)
            {
                SpeedBox.Value = actor.Walking.Speed;
                KeyBox.Text = new string(new char[] { actor.Walking.Key });
            }
        }

        private void BindsDataGrid_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            BindsDataGrid.Items.Clear();
            foreach (Core.Actor.Actor actor in Core.Actor.Manager.Actors)
            {
                if (actor.Walking.IsEnabled)
                {
                    BindsDataGrid.Items.Add(actor);
                }
            }
        }

        private void AddBindBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ActorBar.IsActorSelected)
            {
                ActorBar.SelectedActor.Walking.Key = KeyBox.Text[0];
                if (SpeedBox.Value != null)
                    ActorBar.SelectedActor.Walking.Speed = (int)SpeedBox.Value;
                BindsDataGrid_Loaded(null, null);
            }
        }

        private void RemoveBindBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ActorBar.IsActorSelected)
            {
                ActorBar.SelectedActor.Walking.RemoveBind();
                BindsDataGrid_Loaded(null, null);
            }
        }
    }
}