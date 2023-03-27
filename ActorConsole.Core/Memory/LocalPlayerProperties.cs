using AnotherExternalMemoryLibrary;
using AnotherExternalMemoryLibrary.Extensions;
using System.ComponentModel;
using System.Timers;

namespace ActorConsole.Core.Memory
{
    public class LocalPlayerProperties : INotifyPropertyChanged
    {
        internal LocalPlayerProperties()
        {
            PropertyChangedDetecter.Elapsed += PropertyChangedDetecter_Elapsed;
            PropertyChangedDetecter.Start();
        }

        private void PropertyChangedDetecter_Elapsed( object sender, ElapsedEventArgs e )
        {
            string name = IW4.Game?.Read<byte>((IntPtrEx)Addresses.LocalPlayer_Name, 25)?.GetString();
            if ( Name != name )
            {
                Name = name;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
            int? internalHasSpawned = IW4.Game?.Read<int>(Addresses.LocalPlayer_HasSpawned);
            bool hasSpawned = internalHasSpawned != null && internalHasSpawned != 0;
            if ( HasSpawned != hasSpawned )
            {
                HasSpawned = hasSpawned;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasSpawned)));
            }
            string map = IW4.Game?.Read<byte>((IntPtrEx)Addresses.LocalPlayer_MapName, 20)?.GetString();
            if ( Map != map )
            {
                Map = map;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Map)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public Timer PropertyChangedDetecter { get; } = new Timer(1000);
        public string Name { get; private set; }
        public bool HasSpawned { get; private set; }
        public string Map { get; private set; }
    }
}
