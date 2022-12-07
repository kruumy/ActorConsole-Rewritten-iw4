using System.Collections.Generic;

namespace ActorConsole.Core.Actor
{
    public static class Manager
    {
        public delegate void ActorPropertyChanged(object sender);

        public static event ActorPropertyChanged OnActorPropertyChanged;

        internal static void RaiseActorPropertyChanged(Attributes.Attribute sender) => OnActorPropertyChanged?.Invoke(sender);

        public static readonly List<Actor> Actors = new List<Actor>();

        public static void Add()
        {
            if (Memory.IW4.IsInGame)
            {
                Actors.Add(new Actor());
            }
        }

        public static void Delete(int index)
        {
            if (Memory.IW4.IsInGame)
            {
                Actors[index].Dispose();
                Actors.RemoveAt(index);
            }
        }

        public static void Delete(string name)
        {
            Delete(Search(name));
        }

        public static void ActorBack()
        {
            Memory.IW4.SendDvar("actorback");
        }

        public static int Search(string name)
        {
            for (int i = 0; i < Actors.Count; i++)
                if (Actors[i].Name == name)
                    return i;
            return -1;
        }

        public static void ResetActorManager()
        {
            Actors.Clear();
            Actor.Amount = 1;
        }
    }
}