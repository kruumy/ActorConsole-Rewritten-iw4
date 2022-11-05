using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ActorConsole.GUI.Views
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : UserControl
    {
        public Options()
        {
            InitializeComponent();
        }

        private void SelectPrecacheButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.DefaultExt = ".gsc";
            openFileDialog.Multiselect = false;
            openFileDialog.ShowDialog();
            if (File.Exists(openFileDialog.FileName))
            {
                Objects.Precache.Init(openFileDialog.FileName);
                PrecacheTextBox.Text = openFileDialog.FileName;
            }
        }
    }
}
