using Microsoft.Win32;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ActorConsole.GUI.Views.Other
{
    /// <summary>
    /// Interaction logic for Sun.xaml
    /// </summary>
    public partial class Sun : UserControl
    {
        public Sun()
        {
            InitializeComponent();
        }
        // Core.World.Sun ranges from 0...2, SunColorController is 0...255
        private const float MagicNumber = 127.5f;
        private void SunColorController_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Core.Memory.IW4.IsInMatch)
            {
                SunColorController.R = (byte)(Core.World.Sun.Red * MagicNumber);
                SunColorController.G = (byte)(Core.World.Sun.Green * MagicNumber);
                SunColorController.B = (byte)(Core.World.Sun.Blue * MagicNumber);
            }
        }
        private Task SelectedColorChangedTask = null;
        private void SunColorController_SelectedColorChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            // all this is needed so SunColorController does not lag terribly, i assume it is because SelectedColorChanged is invoked a lot.
            if (SelectedColorChangedTask == null || SelectedColorChangedTask.Status != TaskStatus.Running)
            {
                SelectedColorChangedTask?.Dispose();
                SelectedColorChangedTask = Task.Run(() =>
                {
                    if (Core.Memory.IW4.IsInMatch)
                    {
                        Dispatcher.InvokeAsync(() =>
                        {
                            Core.World.Sun.Red = SunColorController.R / MagicNumber;
                            Core.World.Sun.Green = SunColorController.G / MagicNumber;
                            Core.World.Sun.Blue = SunColorController.B / MagicNumber;
                        });
                    }
                });
            }
        }

        private void Xpos_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Core.Memory.IW4.IsInMatch)
            {
                Xpos.Value = Core.World.Sun.X;
            }
        }

        private void Xpos_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (Core.Memory.IW4.IsInMatch)
            {
                Core.World.Sun.X = (float)Xpos.Value;
            }
        }

        private void Zpos_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Core.Memory.IW4.IsInMatch)
            {
                Zpos.Value = Core.World.Sun.Z;
            }
        }

        private void Zpos_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (Core.Memory.IW4.IsInMatch)
            {
                Core.World.Sun.Z = (float)Zpos.Value;
            }
        }

        private void Ypos_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (Core.Memory.IW4.IsInMatch)
            {
                Core.World.Sun.Y = (float)Ypos.Value;
            }
        }

        private void Ypos_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Core.Memory.IW4.IsInMatch)
            {
                Ypos.Value = Core.World.Sun.Y;
            }
        }

        private void SavePresetBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Core.Memory.IW4.IsInMatch)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    DefaultExt = ".json",
                    RestoreDirectory = true,
                    Title = "Save Sun",
                    Filter = "json files (*.json)|*.json|All files (*.*)|*.*"
                };
                saveFileDialog.FileOk += SaveFileDialog_FileOk;
                saveFileDialog.ShowDialog();
                void SaveFileDialog_FileOk(object sender2, System.ComponentModel.CancelEventArgs e2)
                {
                    File.WriteAllText(saveFileDialog.FileName, Core.World.Sun.ToString());
                }
            }
        }

        private void LoadPresetBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Core.Memory.IW4.IsInMatch)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    DefaultExt = ".json",
                    Multiselect = false,
                    RestoreDirectory = true,
                    Title = "Select Sun",
                    Filter = "json files (*.json)|*.json|All files (*.*)|*.*"
                };
                openFileDialog.FileOk += OpenFileDialog_FileOk;
                openFileDialog.ShowDialog();
                async void OpenFileDialog_FileOk(object sender2, System.ComponentModel.CancelEventArgs e2)
                {
                    await Task.Run(() => Core.World.Sun.LoadJson(File.ReadAllText(openFileDialog.FileName)));
                    SunColorController_Loaded(null, null);
                    Xpos_Loaded(null, null);
                    Ypos_Loaded(null, null);
                    Zpos_Loaded(null, null);
                }
            }
        }
    }
}