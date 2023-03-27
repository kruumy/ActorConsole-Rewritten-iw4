using ActorConsole.Core.Utils;
using ColorPickerExtraLib.Controls;
using ColorPickerExtraLib.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ActorConsole.GUI.Views.Tabs
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

        private const float ConversionScale = 127.5f;

        private readonly BlockingCooldown SunColorUpdateCooldown = new BlockingCooldown(new System.TimeSpan(0, 0, 0, 0, 200));
        private void UserControl_Initialized( object sender, EventArgs e )
        {
            SunColorUpdateCooldown.OnInvoke += ( object _, EventArgs __ ) =>
            {
                Core.Memory.Sun.Red = (float?)(SunColorPicker.Color.RGB_R / ConversionScale);
                Core.Memory.Sun.Green = (float?)(SunColorPicker.Color.RGB_G / ConversionScale);
                Core.Memory.Sun.Blue = (float?)(SunColorPicker.Color.RGB_B / ConversionScale);
            };
        }
        private void SunColorPicker_ColorChanged( object sender, RoutedEventArgs e )
        {
            SunColorUpdateCooldown.Invoke();
        }

        private void SunColorPicker_IsVisibleChanged( object sender, DependencyPropertyChangedEventArgs e )
        {
            if (e.NewValue is bool b && b)
            {
                (float? Red, float? Green, float? Blue) = (Core.Memory.Sun.Red, Core.Memory.Sun.Green, Core.Memory.Sun.Blue);
                SunColorPicker.Color.RGB_R = (double)(Red * ConversionScale);
                SunColorPicker.Color.RGB_G = (double)(Green * ConversionScale);
                SunColorPicker.Color.RGB_B = (double)(Blue * ConversionScale);
            }
        }
    }
}
