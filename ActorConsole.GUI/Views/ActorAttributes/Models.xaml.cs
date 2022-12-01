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

                foreach (string models in ModelsWrapper.GetByCurrentMap(ModelType.Body))
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

                foreach (string models in ModelsWrapper.GetByCurrentMap(ModelType.Head))
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
        private void SPModelsBox_Loaded(object sender, RoutedEventArgs e)
        {
            SPModelsBox.Items.Clear();
            try
            {
                foreach (string models in Classes.Objects.Precache.SP_Models)
                {
                    SPModelsBox.Items.Add(models);
                }
                SPModelsBox.Items.Add("void");
            }
            catch
            {
                SPModelsBox.Items.Add("Failure adding models from precache.");
            }
        }

        private void BodyModelsBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((string)BodyModelsBox.SelectedItem != "Failure adding models from map.")
            {
                BodyModelTextBox.Text = (string)BodyModelsBox.SelectedItem;
                applybodybtn_Click(null, null);
            }
        }

        private void HeadModelsBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((string)HeadModelsBox.SelectedItem != "Failure adding models from map.")
            {
                HeadModelTextBox.Text = (string)HeadModelsBox.SelectedItem;
                applyheadbtn_Click(null, null);
            }
        }

        private void SPModelsBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((string)SPModelsBox.SelectedItem != "Failure adding models from precache.")
            {
                if (((string)SPModelsBox.SelectedItem).Contains("head"))
                {
                    HeadModelTextBox.Text = (string)SPModelsBox.SelectedItem;
                    applyheadbtn_Click(null, null);
                }
                else
                {
                    BodyModelTextBox.Text = (string)SPModelsBox.SelectedItem;
                    applybodybtn_Click(null, null);
                }
            }
        }

        private void BodyModelTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (ActorBar.SelectedActorIndex > -1)
                BodyModelTextBox.Text = ActorBar.SelectedActor.Models.Body;
        }

        private void HeadModelTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (ActorBar.SelectedActorIndex > -1)
                HeadModelTextBox.Text = ActorBar.SelectedActor.Models.Head;
        }

        private void applybodybtn_Click(object sender, RoutedEventArgs e)
        {
            if (ActorBar.SelectedActorIndex > -1 && Core.Memory.IW4.InGame)
                ActorBar.SelectedActor.Models.Body = BodyModelTextBox.Text;
        }

        private void applyheadbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ActorBar.SelectedActorIndex > -1 && Core.Memory.IW4.InGame)
                ActorBar.SelectedActor.Models.Head = HeadModelTextBox.Text;
        }
    }
}
