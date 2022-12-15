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

        private void AddPointBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ActorBar.SelectedActor.Pathing.CreateNode();
        }

        private void RemovePointBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ActorBar.SelectedActor.Pathing.DeleteLastNode();
        }

        private void PlayBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ActorBar.SelectedActor.Pathing.Play((int)SpeedBox.Value);
        }
    }
}