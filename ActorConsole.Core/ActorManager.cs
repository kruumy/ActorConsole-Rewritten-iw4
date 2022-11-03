namespace ActorConsole.Core
{
    public class ActorManager
    {
        public List<Actor.Actor> Actors = new List<Actor.Actor>();

        public ActorManager()
        {

        }

        public int SearchActorsByName(string name)
        {
            for (int i = 0; i < Actors.Count; i++)
            {
                if (Actors[i].Name == name)
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
