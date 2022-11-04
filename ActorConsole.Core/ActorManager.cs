namespace ActorConsole.Core
{
    public class ActorManager
    {
        private readonly List<Actor.Actor> ActorsList = new();
        public Actor.Actor[] Actors { get { return ActorsList.ToArray(); } }

        public bool ShowActorNames { set { Memory.IW4.SendDvar($"ui_showActorNames {Convert.ToInt16(value)}"); } }

        public void Add()
        {
            ActorsList.Add(new Actor.Actor());
        }
        public void Delete(int index)
        {
            ActorsList[index].Delete();
            ActorsList.RemoveAt(index);
        }

        public void ActorBack()
        {
            Memory.IW4.SendDvar("actorback");
        }

        public int Search(string name)
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

    }
}
