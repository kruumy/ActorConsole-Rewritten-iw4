using ActorConsole.Core.Json;
using System;
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
                foreach (System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.Dictionary<string, string[]>> item in WeaponsWrapper.RootElement["weapons"])
                {
                    WeaponsClassBrowser.Items.Add(item.Key);
                }
            }
            catch { }
        }

        private void WeaponsClassBrowser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WeaponsTypeBrowser.Items.Clear();
            try
            {
                foreach (System.Collections.Generic.KeyValuePair<string, string[]> item in WeaponsWrapper.RootElement["weapons"][WeaponsClassBrowser.SelectedItem.ToString()])
                {
                    WeaponsTypeBrowser.Items.Add(item.Key);
                }
            }
            catch { }
        }

        private void WeaponsTypeBrowser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WeaponsExactBrowser.Items.Clear();
            try
            {
                foreach (string item in WeaponsWrapper.RootElement["weapons"][WeaponsClassBrowser.SelectedItem.ToString()][WeaponsTypeBrowser.SelectedItem.ToString()])
                {
                    WeaponsExactBrowser.Items.Add(item);
                }
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

        private void WeaponsExactBrowser_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ApplyBtn_Click(null, null);
        }

        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ActorBar.SelectedActor != null)
            {
                string Camo = "";
                if (CamoBox.Text != "None")
                {
                    Camo = CamoBox.Text;
                }
                string Weapon = SelectedWeaponBox.Text;
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