using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views.Tabs
{
    /// <summary>
    /// Interaction logic for Weapons.xaml
    /// </summary>
    public partial class Weapons : UserControl, INotifyPropertyChanged
    {
        public Weapons()
        {
            InitializeComponent();
            Core.Manager.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private void Instance_PropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == nameof(Core.Manager.Game) )
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WeaponsElement)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CamoNames)));
            }
        }

        public IEnumerable<string> Bones => typeof(Core.CompositedActorProperties.Weapons).GetProperties().Select(x => x.Name);
        public IEnumerable<string> CamoNames => Core.Manager.Instance?.Game.CamoEnum.GetEnumNames();

        public Dictionary<string, Dictionary<string, string[]>> WeaponsElement
        {
            get
            {
                if ( Core.Manager.Instance.Game is Core.Memory.IW5 )
                {
                    return Core.Json.Weapons.Element[ "iw5" ];
                }
                else if ( Core.Manager.Instance.Game is Core.Memory.IW4 )
                {
                    return Core.Json.Weapons.Element[ "iw4" ];
                }
                else
                {
                    return null;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        private void GiveWeaponToPlayerBtn_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            if ( WeaponDetailsListBox.SelectedItem is string gunName && CamoComboBox.SelectedItem is string camo )
            {
                Core.Manager.Instance.Game.GiveWeapon(gunName, camo);
            }
        }
    }
}
