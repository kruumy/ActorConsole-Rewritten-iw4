using ActorConsole.Core.Json;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace ActorConsole.GUI.Json
{
    public class Settings : SettingsBase
    {
        private string _Theme = App.Theme.Dark.ToString();

        public Settings( FileInfo File ) : base(File, true)
        {
        }

        [IgnoreDataMember]
        public static Settings DefaultInstance { get; } = new Settings(new FileInfo(Path.Combine(Environment.CurrentDirectory, @"Json\ActorConsole.GUI.appsettings.json")));

        [IgnoreDataMember]
        public App.Theme RealTheme
        {
            get => (App.Theme)Enum.Parse(typeof(App.Theme), Theme);
            set => Theme = value.ToString();
        }

        public string Theme
        {
            get => _Theme; set
            {
                _Theme = value;
                RaisePropertyChanged(nameof(Theme));
            }
        }
    }
}
