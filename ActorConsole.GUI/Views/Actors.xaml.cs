using ActorConsole.Core.Actor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private void ActorsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ActorsDataGrid_Initialized(object sender, EventArgs e)
        {
        }

        private void AddActorButton_Click(object sender, RoutedEventArgs e)
        {
            Core.ActorManager.Add();
            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            ActorsDataGrid.Items.Clear();
            foreach (var actor in Core.ActorManager.Actors)
            {
                ActorsDataGrid.Items.Add(actor);
            }
        }
    }
}
