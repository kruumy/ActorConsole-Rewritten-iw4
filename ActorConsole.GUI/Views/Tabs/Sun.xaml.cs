using ActorConsole.Core.Json.TinyJson;
using ActorConsole.Core.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ActorConsole.GUI.Views.Tabs
{
    /// <summary>
    /// Interaction logic for SunAddress.xaml
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
                if ( Core.Manager.Instance.Game is Core.Memory.IW4 iw4Game )
                {
                    iw4Game.SunRed = (float)(SunColorPicker.Color.RGB_R / ConversionScale);
                    iw4Game.SunGreen = (float)(SunColorPicker.Color.RGB_G / ConversionScale);
                    iw4Game.SunBlue = (float)(SunColorPicker.Color.RGB_B / ConversionScale);
                }

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
            if ( e.NewValue is bool b && b && Core.Manager.Instance.Game is Core.Memory.IW4 iw4Game )
            {
                (float? Red, float? Green, float? Blue) = (iw4Game.SunRed, iw4Game.SunGreen, iw4Game.SunBlue);
                SetSunColorPicker(
                    (double)(Red * ConversionScale),
                    (double)(Green * ConversionScale),
                    (double)(Blue * ConversionScale)
                    );
            }
        }

        private void LoadBtn_Click( object sender, RoutedEventArgs e )
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FileOk += ( object _, CancelEventArgs __ ) =>
            {
                string rawtext = File.ReadAllText(openFileDialog.FileName);
                Dictionary<string, float> tempSunObj = rawtext.FromJson<Dictionary<string, float>>();
                SunColorPicker.Color.RGB_R = tempSunObj[ nameof(SunColorPicker.Color.RGB_R) ];
                SunColorPicker.Color.RGB_G = tempSunObj[ nameof(SunColorPicker.Color.RGB_G) ];
                SunColorPicker.Color.RGB_B = tempSunObj[ nameof(SunColorPicker.Color.RGB_B) ];
                // TODO add xyz
            };
            openFileDialog.ShowDialog();
        }

        private void SaveBtn_Click( object sender, RoutedEventArgs e )
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileOk += ( object _, CancelEventArgs __ ) =>
            {
                Dictionary<string, float> tempSunObj = new Dictionary<string, float>
                {
                    [ nameof(SunColorPicker.Color.RGB_R) ] = (float)SunColorPicker.Color.RGB_R,
                    [ nameof(SunColorPicker.Color.RGB_G) ] = (float)SunColorPicker.Color.RGB_G,
                    [ nameof(SunColorPicker.Color.RGB_B) ] = (float)SunColorPicker.Color.RGB_B
                    // TODO add xyz
                };
                string rawtext = tempSunObj.ToJson();
                File.WriteAllText(saveFileDialog.FileName, rawtext);
            };
            saveFileDialog.ShowDialog();

        }
    }
}
