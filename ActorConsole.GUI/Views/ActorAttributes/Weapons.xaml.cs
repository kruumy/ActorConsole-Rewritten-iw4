using ActorConsole.Core.Json;
using System.Windows;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views.ActorAttributes
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

        private void WeaponsClassBrowser_Initialized(object sender, EventArgs e)
        {
            WeaponsClassBrowser.Items.Clear();
            try
            {
                foreach (System.Text.Json.JsonProperty item in GunsWrapper.RootElement.GetProperty("weapons").EnumerateObject())
                    WeaponsClassBrowser.Items.Add(item.Name);
            }
            catch { }
        }

        private void WeaponsClassBrowser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WeaponsTypeBrowser.Items.Clear();
            try
            {
                foreach (System.Text.Json.JsonProperty item in GunsWrapper.RootElement.GetProperty("weapons").GetProperty(WeaponsClassBrowser.SelectedItem.ToString()).EnumerateObject())
                    WeaponsTypeBrowser.Items.Add(item.Name);
            }
            catch { }
        }

        private void WeaponsTypeBrowser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WeaponsExactBrowser.Items.Clear();
            try
            {
                foreach (System.Text.Json.JsonElement item in GunsWrapper.RootElement.GetProperty("weapons").GetProperty(WeaponsClassBrowser.SelectedItem.ToString()).GetProperty(WeaponsTypeBrowser.SelectedItem.ToString()).EnumerateArray())
                    WeaponsExactBrowser.Items.Add(item.GetString());
            }
            catch { }
        }

        private void WeaponsExactBrowser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SelectedWeaponBox.Text = WeaponsExactBrowser.SelectedItem.ToString();
            }
            catch { }
        }
        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ActorBar.SelectedActorIndex > -1 && Core.Memory.IW4.InGame && SelectedWeaponBox.Text != string.Empty)
            {
                string Camo = "";
                string Weapon = "";
                if (CamoBox.Text != "None")
                    Camo = CamoBox.Text;
                Weapon = SelectedWeaponBox.Text;
                switch (BoneBox.Text)
                {
                    case "j_gun":
                        {
                            ActorBar.SelectedActor.Weapons.j_gun = $"{Weapon} {Camo}";
                            break;
                        }
                    case "tag_inhand":
                        {
                            ActorBar.SelectedActor.Weapons.tag_inhand = $"{Weapon} {Camo}";
                            break;
                        }
                    case "tag_stowed_back":
                        {
                            ActorBar.SelectedActor.Weapons.tag_stowed_back = $"{Weapon} {Camo}";
                            break;
                        }
                    case "tag_stowed_hip_rear":
                        {
                            ActorBar.SelectedActor.Weapons.tag_stowed_hip_rear = $"{Weapon} {Camo}";
                            break;
                        }
                    case "tag_weapon_chest":
                        {
                            ActorBar.SelectedActor.Weapons.tag_weapon_chest = $"{Weapon} {Camo}";
                            break;
                        }
                    case "tag_weapon_left":
                        {
                            ActorBar.SelectedActor.Weapons.tag_weapon_left = $"{Weapon} {Camo}";
                            break;
                        }
                    case "tag_weapon_right":
                        {
                            ActorBar.SelectedActor.Weapons.tag_weapon_right = $"{Weapon} {Camo}";
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}
