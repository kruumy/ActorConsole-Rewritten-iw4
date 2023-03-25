using System.Collections.ObjectModel;

namespace ActorConsole.Core
{
    public static class Manager
    {
        public static ObservableCollection<Actor> Actors { get; } = new ObservableCollection<Actor>();

        public static void ActorBack()
        {
            Memory.IW4.Send("actorback");
        }

        public static void RemoveActor( Actor actor )
        {
            actor.Dispose();
            Actors.Remove(actor);
        }

        public static void SpawnActor()
        {
            Actors.Add(new Actor());
        }

        public static void Reset()
        {
            Actors.Clear();
            Actor.Count = 0;
        }
    }
}
