namespace ActorConsole.Core.Actor.Movement
{

    public class Walking
    {
        internal string ActorName { get; set; }
        private int _Speed;
        public int Speed
        {
            get => _Speed;
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
            get => _Key;
            set
            {
                RemoveBind();
                _Key = value;
                CreateBind();
            }
        }
        private string Command => $"mvm_actor_walk {ActorName} {Speed}";
        public void Play()
        {
            Memory.IW4.SendDvar(Command);
        }
        private void CreateBind()
        {
            if (Key != '\u0000')
                Memory.IW4.SendDvar($"bind {Key} \"{Command}\"");
        }
        public void RemoveBind()
        {
            if (Key != '\u0000')
                Memory.IW4.SendDvar($"bind {Key} say");
        }
    }

}
