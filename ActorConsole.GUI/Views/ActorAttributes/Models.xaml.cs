using ActorConsole.Core.Misc.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void SPModelsBox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void SPModelsBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
