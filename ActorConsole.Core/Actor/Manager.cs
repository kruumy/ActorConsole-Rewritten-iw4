namespace ActorConsole.Core.Actor
{
    public static class Manager
    {
        private static readonly List<Actor> ActorsList = new();
        public static Actor[] Actors => ActorsList.ToArray();

        public static void Add()
        {
            if (Memory.IW4.IsInGame)
            {
                ActorsList.Add(new Actor());
            }
        }
        public static void Delete(int index)
        {
            if (Memory.IW4.IsInGame)
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
            Actor.Amount = 1;
        }


    }

}
