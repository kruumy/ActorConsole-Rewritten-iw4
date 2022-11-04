namespace ActorConsole.Core.Actor.Movement
{

    public class Walking
    {
        internal string ActorName { get; set; }
        private int _Speed;
        public int Speed
        {
            get
            {
                return _Speed;
            }
            set
            {
                RemoveBind();
                _Speed = value;
                CreateBind();
            }
        }
        private char _Key;
        public char Key
        {
            get
            {
                return _Key;
            }
            set
            {
                RemoveBind();
                _Key = value;
                CreateBind();
            }
        }
        private string Command
        {
            get
            {
                return $"mvm_actor_walk {ActorName} {Speed}";
            }
        }
        public void Play()
        {
            Memory.IW4.SendDvar(Command);
        }
        private void CreateBind()
        {
            Memory.IW4.SendDvar($"bind {Key} \"{Command}\"");
        }
        public void RemoveBind()
        {
            Memory.IW4.SendDvar($"bind {Key} say");
        }
    }

}
