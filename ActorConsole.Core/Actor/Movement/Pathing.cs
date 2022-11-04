namespace ActorConsole.Core.Actor.Movement
{
    public class Pathing
    {
        internal string ActorName { get; set; }
        public int Speed { get; set; }
        private int LastNode = 1;
        public int CreateNode()
        {
            if (LastNode <= 13)
            {
                Memory.IW4.SendDvar($"mvm_actor_path_save {ActorName} {LastNode}");
                int result = LastNode;
                LastNode++;
                return result;
            }
            else
            {
                return -1;
            }

        }
        public int DeleteLastNode()
        {
            Memory.IW4.SendDvar($"mvm_actor_path_del {ActorName} {LastNode}");
            int result = LastNode;
            LastNode--;
            return result;

        }
        public void Play()
        {
            Memory.IW4.SendDvar($"mvm_actor_path_walk {ActorName} {Speed}");
        }


    }
}
