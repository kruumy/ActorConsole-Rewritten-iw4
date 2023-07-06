using ActorConsole.Core.Memory;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace ActorConsole.Core
{
    public class Manager : INotifyPropertyChanged
    {
        private Game game;

        public static Manager Instance { get; } = new Manager();
        private Timer PropertyChangedDetecter { get; } = new Timer(5000);
        private Manager()
        {
            PropertyChangedDetecter.Elapsed += PropertyChangedDetecter_Elapsed;
            PropertyChangedDetecter.Start();
        }

        private void PropertyChangedDetecter_Elapsed( object sender, ElapsedEventArgs e )
        {
            Process[] processes = Process.GetProcesses();
            Process iw4Proc = processes.FirstOrDefault(p => IW4.TargetProcessNames.Any(tp => p.ProcessName == tp));
            Process iw5Proc = processes.FirstOrDefault(p => p.ProcessName == IW5.TargetProcessName);
            if ( iw4Proc != null )
            {
                if ( game == null || game.Process.Id != iw4Proc.Id )
                {
                    Game = new IW4();
                }
            }
            else if ( iw5Proc != null )
            {
                if ( game == null || game.Process.Id != iw5Proc.Id )
                {
                    Game = new IW5();
                }
            }
        }

        public ObservableCollection<Actor> Actors { get; } = new ObservableCollection<Actor>();
        public Game Game
        {
            get
            {
                PropertyChangedDetecter_Elapsed(this, null);
                return game;
            }

            private set
            {
                game = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Game)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ActorBack()
        {
            Game.Send("actorback");
        }

        public void RemoveActor( Actor actor )
        {
            actor.Dispose();
            Actors.Remove(actor);
        }

        public void SpawnActor()
        {
            Actors.Add(new Actor());
        }

        public void Reset()
        {
            Actors.Clear();
            Actor.Count = 0;
        }
    }
}
