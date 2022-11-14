using ActorConsole.Core.Misc.Json;
using System;
using System.Collections.Generic;
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

namespace ActorConsole.GUI.Views.ActorAttributes
{
    /// <summary>
    /// Interaction logic for Models.xaml
    /// </summary>
    public partial class Models : UserControl
    {
        public Models()
        {
            InitializeComponent();
        }

        private void BodyModelsBox_Loaded(object sender, RoutedEventArgs e)
        {
            BodyModelsBox.Items.Clear();
            try
            {

                foreach (string models in ModelsWrapper.Get(ModelType.Body))
                {
                    BodyModelsBox.Items.Add(models);
                }
                BodyModelsBox.Items.Add("void");
            }
            catch
            {
                BodyModelsBox.Items.Add("Failure adding models from map.");
            }

        }

        private void HeadModelsBox_Loaded(object sender, RoutedEventArgs e)
        {
            HeadModelsBox.Items.Clear();
            try
            {

                foreach (string models in ModelsWrapper.Get(ModelType.Head))
                {
                    HeadModelsBox.Items.Add(models);
                }
                HeadModelsBox.Items.Add("void");
            }
            catch
            {
                HeadModelsBox.Items.Add("Failure adding models from map.");
            }
        }

        private void BodyModelsBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ActorBar.SelectedActorIndex > -1 && Core.Memory.IW4.InGame)
                ActorBar.SelectedActor.Models.Body = BodyModelsBox.SelectedItem.ToString();
        }

        private void HeadModelsBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ActorBar.SelectedActorIndex > -1 && Core.Memory.IW4.InGame)
                ActorBar.SelectedActor.Models.Head = HeadModelsBox.SelectedItem.ToString();
        }

        private void BodyModelsBox_MouseEnter(object sender, MouseEventArgs e)
        {
            BodyModelsBox_Loaded(null, null);
        }

        private void HeadModelsBox_MouseEnter(object sender, MouseEventArgs e)
        {
            HeadModelsBox_Loaded(null, null);
        }
    }
}
