using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views.Tabs
{
    /// <summary>
    /// Interaction logic for FX.xaml
    /// </summary>
    public partial class FX : UserControl
    {
        public FX()
        {
            InitializeComponent();
        }

        public IEnumerable<string> BoneNames => typeof(Core.CompositedActorProperties.Bone).GetEnumNames();
        public IEnumerable<string> EffectNames => typeof(Core.CompositedActorProperties.FX.EffectName).GetEnumNames();


        private void PlayFXBtn_Click( object sender, RoutedEventArgs e )
        {
            if ( ActorGrid.SelectedItem is Core.Actor actor )
            {
                actor.FX.Play();
            }
        }

        private void BindMenuItem_Click( object sender, RoutedEventArgs e )
        {
            if ( ActorGrid.SelectedItem is Core.Actor actor )
            {
                KeybindDialog keybindWindow = new Views.KeybindDialog(actor.FX);
                keybindWindow.ShowDialog();
            }
        }
    }
}
