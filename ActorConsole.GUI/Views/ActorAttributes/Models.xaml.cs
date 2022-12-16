using ActorConsole.Core.Json;
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

        private void UserControl_Initialized(object sender, System.EventArgs e)
        {
            ActorBar.OnSelectedActorChanged += ActorBar_OnSelectedActorChanged;
        }

        private void ActorBar_OnSelectedActorChanged(object sender, int index, Core.Actor.Actor actor)
        {
            BodyModelTextBox.Text = actor.Models.Body;
            HeadModelTextBox.Text = actor.Models.Head;
        }

        private void BodyModelsBox_Loaded(object sender, RoutedEventArgs e)
        {
            BodyModelsBox.ItemsSource = ModelsWrapper.GetByCurrentMap(ModelType.Body);
        }

        private void HeadModelsBox_Loaded(object sender, RoutedEventArgs e)
        {
            HeadModelsBox.ItemsSource = ModelsWrapper.GetByCurrentMap(ModelType.Head);
        }

        private void SPModelsBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (Classes.Objects.Settings.IsPrecacheSelected)
                SPModelsBox.ItemsSource = Classes.Objects.Precache.SP_Models;
        }

        private void BodyModelsBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BodyModelTextBox.Text = (string)BodyModelsBox.SelectedItem;
            applybodybtn_Click(null, null);
        }

        private void HeadModelsBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HeadModelTextBox.Text = (string)HeadModelsBox.SelectedItem;
            applyheadbtn_Click(null, null);
        }

        private void SPModelsBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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

        private void applybodybtn_Click(object sender, RoutedEventArgs e)
        {
            if (ActorBar.SelectedActor != null)
                ActorBar.SelectedActor.Models.Body = BodyModelTextBox.Text;
        }

        private void applyheadbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ActorBar.SelectedActor != null)
                ActorBar.SelectedActor.Models.Head = HeadModelTextBox.Text;
        }
    }
}