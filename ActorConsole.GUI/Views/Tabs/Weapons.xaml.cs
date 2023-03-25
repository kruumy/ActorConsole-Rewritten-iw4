using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views.Tabs
{
    /// <summary>
    /// Interaction logic for Weapons.xaml
    /// </summary>
    public partial class Weapons : UserControl
    {
        public Weapons()
        {
            InitializeComponent();
        }

        public IEnumerable<string> Bones => typeof(Core.CompositedActorProperties.Weapons).GetProperties().Select(x => x.Name);
        public IEnumerable<string> CamoNames => typeof(Core.CompositedActorProperties.Weapons.Weapon.CamoName).GetEnumNames();

        public Dictionary<string, Dictionary<string, string[]>> WeaponsElement => Core.Json.Weapons.Element;

        private void ApplyWeaponButton_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            if ( DataContext is ActorManager am && am.SelectedActor is Core.Actor actor && BoneComboBox.SelectedItem is string bone && CamoComboBox.SelectedItem is string camo )
            {
                actor.Weapons.Set(bone, WeaponTextBox.Text, camo);
            }
        }

        private void WeaponDetailsListBox_MouseDoubleClick( object sender, System.Windows.Input.MouseButtonEventArgs e )
        {
            if ( WeaponDetailsListBox.SelectedItem is string gunName && DataContext is ActorManager am && am.SelectedActor is Core.Actor actor && BoneComboBox.SelectedItem is string bone && CamoComboBox.SelectedItem is string camo )
            {
                actor.Weapons.Set(bone, gunName, camo);

                int oldindex = BoneComboBox.SelectedIndex;
                BoneComboBox.SelectedIndex = -1; // TODO find better way to force a refresh on WeaponsTextBox
                BoneComboBox.SelectedIndex = oldindex;
            }
        }
    }
}
