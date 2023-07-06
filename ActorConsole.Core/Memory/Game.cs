using ActorConsole.Core.Utils;
using AnotherExternalMemoryLibrary;
using AnotherExternalMemoryLibrary.Extensions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace ActorConsole.Core.Memory
{
    public abstract class Game : INotifyPropertyChanged
    {
        public static bool IsProcessRunning( string processName )
        {
            return Process.GetProcessesByName(processName).Length > 0;
        }
        public static bool IsAnyGameRunning()
        {
            return IW4.TargetProcessNames.Any(iw4 => Game.IsProcessRunning(iw4)) || Game.IsProcessRunning(IW5.TargetProcessName);
        }
        public abstract Type KillStreakEnum { get; }
        public abstract Type CamoEnum { get; }
        public Process Process { get; }
        private bool isSpawned;

        private string map;

        private string name;

        public event PropertyChangedEventHandler PropertyChanged;

        public enum Class
        {
            ASSAULT,
            SHOTGUN,
            SMG,
            LMG,
            RIOT,
            SNIPER,
            GHILLIE
        }

        public enum Team
        {
            axis,
            allies
        }

        public abstract IntPtrEx Cbuf_AddText { get; }

        public QueuedCooldown<string> DvarQueue { get; }

        public bool LocalPlayerIsSpawned
        {
            get => isSpawned;
            private set
            {
                isSpawned = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LocalPlayerIsSpawned)));
            }
        }

        public abstract IntPtrEx LocalPlayerIsSpawnedAddress { get; }

        public string LocalPlayerMap
        {
            get => map;
            private set
            {
                map = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LocalPlayerMap)));
            }
        }

        public abstract IntPtrEx LocalPlayerMapNameAddress { get; }

        public string LocalPlayerName
        {
            get => name;
            private set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LocalPlayerName)));
            }
        }

        public abstract IntPtrEx LocalPlayerNameAddress { get; }

        protected Timer PropertyChangedDetecter { get; } = new Timer(1000);

        public Game( Process _Process )
        {
            this.Process = _Process;
            DvarQueue = new QueuedCooldown<string>(new TimeSpan(0, 0, 0, 1, 0), ( object obj, string dvar ) =>
            {
                Console.WriteLine(dvar);
                RemoteProcedureCall.Callx86(Process.Handle, Cbuf_AddText, 0u, 0, dvar);
            });
            PropertyChangedDetecter.Elapsed += PropertyChangedDetecter_Elapsed;
            PropertyChangedDetecter.Start();
        }

        public void Send( params string[] dvars )
        {
            foreach ( string dvar in dvars )
            {
                DvarQueue.Invoke(dvar);
            }
        }

        private void PropertyChangedDetecter_Elapsed( object sender, ElapsedEventArgs e )
        {
            string name = Process.Read<byte>(LocalPlayerNameAddress, 25)?.GetString();
            if ( LocalPlayerName != name )
            {
                LocalPlayerName = name;
            }
            int? internalHasSpawned = Process.Read<int>(LocalPlayerIsSpawnedAddress);
            bool isSpawned = internalHasSpawned != null && internalHasSpawned != 0 && internalHasSpawned != 8192;
            if ( LocalPlayerIsSpawned != isSpawned )
            {
                LocalPlayerIsSpawned = isSpawned;
            }
            string map = Process.Read<byte>(LocalPlayerMapNameAddress, 20)?.GetString();
            if ( LocalPlayerMap != map )
            {
                LocalPlayerMap = map;
            }
        }

        public void GiveWeapon( string gameName, string camo )
        {
            if ( camo == "none" )
            {
                Send($"mvm_give {gameName}");
            }
            else
            {
                Send($"mvm_give {gameName} {camo}");
            }

        }

        public void SetModel( Class model, Team team )
        {
            SetModel(model, team, LocalPlayerName);
        }

        public void SetModel( Class model, Team team, string name )
        {
            Send($"mvm_bot_model {name} {model} {team}");
        }

        public void GiveKillstreak( string killstreakName )
        {
            Send($"mvm_killstreak {killstreakName}");
        }
    }
}
