using ActorConsole.Core.Memory;

namespace ActorConsole.Core
{
    public static class ActorManager
    {
        private static readonly List<Actor.Actor> ActorsList = new();
        public static Actor.Actor[] Actors { get { return ActorsList.ToArray(); } }

        public static void Add()
        {
            if (Core.Memory.IW4.InGame)
            {
                ActorsList.Add(new Actor.Actor());
            }

        }
        public static void Delete(int index)
        {
            if (Core.Memory.IW4.InGame)
            {
                ActorsList[index].Delete();
                ActorsList.RemoveAt(index);
            }
        }

        public static void ActorBack()
        {
            Memory.IW4.SendDvar("actorback");
        }

        public static int Search(string name)
        {
            for (int i = 0; i < ActorsList.Count; i++)
            {
                if (ActorsList[i].Name == name)
                {
                    return i;
                }
            }
            return -1;
        }
        public static void ResetActorManager()
        {

            ActorsList.Clear();
            ActorConsole.Core.Actor.Actor.Amount = 1;
        }


    }

}
