namespace ActorConsole.Core.Actor.Movement
{

    public class Basic
    {
        public string ActorName { get; set; }
        public int Speed { get; set; }
        public char Key { get; set; }

        public Basic(int speed, char key, string actorName)
        {
            ActorName = Actor.NextActorName;
            Speed = speed;
            Key = key;
            CreateBind();
        }
        private void CreateBind()
        {
            Memory.IW4.SendDvar($"bind {Key} \"mvm_actor_walk {ActorName} {Speed}\"");
        }
        private void RemoveBind()
        {
            Memory.IW4.SendDvar($"bind {Key} say ");
        }
    }

}
