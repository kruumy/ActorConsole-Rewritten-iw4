namespace ActorConsole.Core
{
    public class ActorManager
    {
        private List<Actor.Actor> ActorsList = new List<Actor.Actor>();
        public Actor.Actor[] Actors { get { return ActorsList.ToArray(); } }

        public bool ShowActorNames { set { Memory.IW4.SendDvar($"ui_showActorNames {Convert.ToInt16(value)}"); } }

        public ActorManager()
        {

        }
        
        public void Add(Actor.Actor actor)
        {
            ActorsList.Add(actor);
        }
        public void Remove(int index)
        {
            ActorsList[index].Delete();
            ActorsList.RemoveAt(index);
        }

        public void Reset()
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
