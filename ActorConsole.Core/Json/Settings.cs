using System;
using System.IO;
using System.Runtime.Serialization;

namespace ActorConsole.Core.Json
{
    public class Settings : SettingsBase
    {
        private Precache _Precache;
        private string _PrecachePath;

        public Settings( FileInfo File ) : base(File, false)
        {
        }

        [IgnoreDataMember]
        public static Settings DefaultInstance { get; } = new Settings(new FileInfo(Path.Combine(Environment.CurrentDirectory, @"Json\ActorConsole.Core.appsettings.json")));

        [IgnoreDataMember]
        public bool IsPrecachePathValid => !string.IsNullOrEmpty(PrecachePath) && System.IO.File.Exists(PrecachePath);

        [IgnoreDataMember]
        public Precache Precache
        {
            get => _Precache;
            private set
            {
                _Precache = value;
                RaisePropertyChanged(nameof(Precache));
            }
        }

        public string PrecachePath
        {
            get => _PrecachePath;
            set
            {
                _PrecachePath = value;
                RaisePropertyChanged(nameof(PrecachePath));
                RaisePropertyChanged(nameof(IsPrecachePathValid));
                if ( IsPrecachePathValid )
                {
                    Precache = new Precache(new FileInfo(value));
                }
                Push();
            }
        }
    }
}
