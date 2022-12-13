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
        private float MagicNumber = 127.5f;
        private void SunColorController_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Core.Memory.IW4.IsInGame)
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
                    if (Core.Memory.IW4.IsInGame)
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
            if (Core.Memory.IW4.IsInGame)
            {
                Xpos.Value = Core.World.Sun.X;
            }
        }

        private void Xpos_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (Core.Memory.IW4.IsInGame)
            {
                Core.World.Sun.X = (float)Xpos.Value;
            }
        }

        private void Zpos_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Core.Memory.IW4.IsInGame)
            {
                Zpos.Value = Core.World.Sun.Z;
            }
        }

        private void Zpos_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (Core.Memory.IW4.IsInGame)
            {
                Core.World.Sun.Z = (float)Zpos.Value;
            }
        }

        private void Ypos_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (Core.Memory.IW4.IsInGame)
            {
                Core.World.Sun.Y = (float)Ypos.Value;
            }
        }

        private void Ypos_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Core.Memory.IW4.IsInGame)
            {
                Ypos.Value = Core.World.Sun.Y;
            }
        }
    }
}