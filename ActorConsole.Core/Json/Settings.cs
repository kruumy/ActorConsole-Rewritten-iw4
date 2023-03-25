using ActorConsole.Core.Json.TinyJson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace ActorConsole.Core.Json
{
    public class Settings : INotifyPropertyChanged
    {
        public static Settings DefaultInstance { get; } = new Settings(new FileInfo(Path.Combine(Environment.CurrentDirectory, @"Json\ActorConsole.Core.appsettings.json")));
        private Precache _Precache;
        private string _PrecachePath;

        public Settings( FileInfo File )
        {
            this.File = File;
            if ( File.Exists )
            {
                Pull();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public FileInfo File { get; }
        public bool IsPrecachePathValid => !string.IsNullOrEmpty(PrecachePath) && System.IO.File.Exists(PrecachePath);

        public Precache Precache
        {
            get => _Precache;
            private set
            {
                _Precache = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Precache)));
            }
        }

        public string PrecachePath
        {
            get => _PrecachePath;
            set
            {
                _PrecachePath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PrecachePath)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsPrecachePathValid)));
                if ( IsPrecachePathValid )
                {
                    Precache = new Precache(new FileInfo(value));
                }
                Push();
            }
        }

        public void Pull()
        {
            string rawFileContents = System.IO.File.ReadAllText(File.FullName);
            Dictionary<string, string> json = TinyJson.JSONParser.FromJson<Dictionary<string, string>>(rawFileContents);
            PrecachePath = json[ nameof(PrecachePath) ];
        }

        public void Push()
        {
            Dictionary<string, string> json = new Dictionary<string, string>();
            json[ nameof(PrecachePath) ] = PrecachePath;
            System.IO.File.WriteAllText(File.FullName, json.ToJson());
        }
    }
}
