using ActorConsole.Core.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

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
        private bool IsSunColorPickerChanging = false;
        private void SetSunColorPicker( double r, double g, double b )
        {
            IsSunColorPickerChanging = true;
            SunColorPicker.Color.RGB_R = r;
            SunColorPicker.Color.RGB_G = g;
            SunColorPicker.Color.RGB_B = b;
            IsSunColorPickerChanging = false;
        }

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
            if ( !IsSunColorPickerChanging )
            {
                SunColorUpdateCooldown.Invoke();
            }
        }

        private void SunColorPicker_IsVisibleChanged( object sender, DependencyPropertyChangedEventArgs e )
        {
            if ( e.NewValue is bool b && b )
            {
                (float? Red, float? Green, float? Blue) = (Core.Memory.Sun.Red, Core.Memory.Sun.Green, Core.Memory.Sun.Blue);
                SetSunColorPicker(
                    (double)(Red * ConversionScale),
                    (double)(Green * ConversionScale),
                    (double)(Blue * ConversionScale)
                    );
            }
        }
    }
}
