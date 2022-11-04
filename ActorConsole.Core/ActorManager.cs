namespace ActorConsole.Core
{
    public class ActorManager
    {
        public List<Actor.Actor> Actors = new List<Actor.Actor>();

        public bool ShowActorNames { set { Memory.IW4.SendDvar($"ui_showActorNames {Convert.ToInt16(value)}"); } }

        public ActorManager()
        {

        }

        public void ResetActors()
        {
            Memory.IW4.SendDvar("actorback");
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
