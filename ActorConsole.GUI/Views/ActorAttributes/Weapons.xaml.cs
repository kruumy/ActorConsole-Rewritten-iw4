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
            try
            {
                WeaponsClassBrowser.Items.Clear();
                foreach (System.Text.Json.JsonProperty item in Core.Misc.Json.GunsWrapper.RootElement.GetProperty("weapons").EnumerateObject())
                    WeaponsClassBrowser.Items.Add(item.Name);
            }
            catch { }
        }

        private void WeaponsClassBrowser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                WeaponsTypeBrowser.Items.Clear();
                foreach (System.Text.Json.JsonProperty item in Core.Misc.Json.GunsWrapper.RootElement.GetProperty("weapons").GetProperty(WeaponsClassBrowser.SelectedItem.ToString()).EnumerateObject())
                    WeaponsTypeBrowser.Items.Add(item.Name);
            }
            catch { }
        }

        private void WeaponsTypeBrowser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                WeaponsExactBrowser.Items.Clear();
                foreach (System.Text.Json.JsonElement item in Core.Misc.Json.GunsWrapper.RootElement.GetProperty("weapons").GetProperty(WeaponsClassBrowser.SelectedItem.ToString()).GetProperty(WeaponsTypeBrowser.SelectedItem.ToString()).EnumerateArray())
                    WeaponsExactBrowser.Items.Add(item.GetString());
            }
            catch { }
        }

        private void WeaponsExactBrowser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedWeaponBox.Text = WeaponsExactBrowser.SelectedItem.ToString();
        }

        private void CamoBox_Initialized(object sender, EventArgs e)
        {
            CamoBox.Items.Clear();
            CamoBox.Items.Add("None");
            CamoBox.SelectedIndex = 0;
            foreach (string item in Core.Misc.Json.GunsWrapper.Camos)
                CamoBox.Items.Add(item);
        }

        private void BoneBox_Initialized(object sender, EventArgs e)
        {
            BoneBox.Items.Clear();
            foreach (string item in Core.Actor.Weapons.Bones)
                BoneBox.Items.Add(item);
            BoneBox.SelectedIndex = 0;
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

                switch (BoneBox.SelectedItem.ToString())
                {
                    case "j_gun":
                        {
                            ActorBar.SelectedActor.Weapons.j_gun = $"{Weapon} {Camo}";
                            break;
                        }
                }
            }
        }
    }
}
