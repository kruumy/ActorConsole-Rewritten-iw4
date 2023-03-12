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

        private void UserControl_Initialized( object sender, System.EventArgs e )
        {
            ActorBar.OnSelectedActorChanged += ActorBar_OnSelectedActorChanged;
        }

        private void ActorBar_OnSelectedActorChanged( object sender, int index, Core.Actor.Actor actor )
        {
            if ( actor.Walking.IsEnabled )
            {
                SpeedBox.Value = actor.Walking.Speed;
                KeyBox.Text = new string(new char[] { actor.Walking.Key });
            }
        }

        private void BindsDataGrid_Loaded( object sender, System.Windows.RoutedEventArgs e )
        {
            BindsDataGrid.Items.Clear();
            foreach ( Core.Actor.Actor actor in Core.Actor.Manager.Actors )
            {
                if ( actor.Walking.IsEnabled )
                {
                    BindsDataGrid.Items.Add(actor);
                }
            }
        }

        private void AddBindBtn_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            if ( ActorBar.SelectedActor != null && KeyBox.Text.Length > 0 && SpeedBox.Value != null && DirectionBox.SelectedItem is string dirText )
            {
                ActorBar.SelectedActor.Walking.Key = KeyBox.Text[ 0 ];
                ActorBar.SelectedActor.Walking.Speed = (int)SpeedBox.Value;
                string[] array = typeof(Core.Actor.Attributes.Walking.WalkingDirection).GetEnumNames();
                for ( int i = 0; i < array.Length; i++ )
                {
                    if ( array[ i ] == dirText )
                    {
                        ActorBar.SelectedActor.Walking.Direction = (Core.Actor.Attributes.Walking.WalkingDirection)i;
                    }
                }

                BindsDataGrid_Loaded(null, null);
            }
        }

        private void RemoveBindBtn_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            if ( ActorBar.SelectedActor != null )
            {
                ActorBar.SelectedActor.Walking.RemoveBind();
                BindsDataGrid_Loaded(null, null);
            }
        }

        private void DirectionBox_Loaded( object sender, System.Windows.RoutedEventArgs e )
        {
            DirectionBox.ItemsSource = typeof(Core.Actor.Attributes.Walking.WalkingDirection).GetEnumNames();
            DirectionBox.SelectedIndex = 0;
            DirectionBox.Items.Refresh();
        }
    }
}