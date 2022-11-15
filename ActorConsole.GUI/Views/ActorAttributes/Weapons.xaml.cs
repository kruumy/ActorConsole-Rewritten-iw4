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
    /// Interaction logic for Weapons.xaml
    /// </summary>
    public partial class Weapons : UserControl
    {
        public Weapons()
        {
            InitializeComponent();
        }

        private void WeaponsClassBrowser_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                WeaponsClassBrowser.Items.Clear();
                foreach (var item in Core.Misc.Json.GunsWrapper.RootElement.GetProperty("weapons").EnumerateObject())
                    WeaponsClassBrowser.Items.Add(item.Name);
            }
            catch { }
        }

        private void WeaponsClassBrowser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                WeaponsTypeBrowser.Items.Clear();
                foreach (var item in Core.Misc.Json.GunsWrapper.RootElement.GetProperty("weapons").GetProperty(WeaponsClassBrowser.SelectedItem.ToString()).EnumerateObject())
                    WeaponsTypeBrowser.Items.Add(item.Name);
            }
            catch { }
        }

        private void WeaponsTypeBrowser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                WeaponsExactBrowser.Items.Clear();
                foreach (var item in Core.Misc.Json.GunsWrapper.RootElement.GetProperty("weapons").GetProperty(WeaponsClassBrowser.SelectedItem.ToString()).GetProperty(WeaponsTypeBrowser.SelectedItem.ToString()).EnumerateArray())
                    WeaponsExactBrowser.Items.Add(item.GetString());
            }
            catch { }
        }

        private void WeaponsExactBrowser_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectedWeaponBox.Text = WeaponsExactBrowser.SelectedItem.ToString();
        }

        private void CamoBox_Initialized(object sender, EventArgs e)
        {
            CamoBox.Items.Clear();
            CamoBox.Items.Add("None");
            CamoBox.SelectedIndex = 0;
            foreach (var item in Core.Misc.Json.GunsWrapper.Camos)
                CamoBox.Items.Add(item);
        }

        private void BoneBox_Initialized(object sender, EventArgs e)
        {
            BoneBox.Items.Clear();
            foreach (var item in Core.Actor.Weapons.Bones)
                BoneBox.Items.Add(item);
            BoneBox.SelectedIndex = 0;
        }

        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            string Camo = "";
            string Weapon = "";
            if (CamoBox.Text != "None")
                Camo = CamoBox.Text;
            if (SelectedWeaponBox.Text != string.Empty)
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
