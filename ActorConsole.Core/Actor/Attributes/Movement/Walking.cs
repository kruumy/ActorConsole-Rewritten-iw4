namespace ActorConsole.Core.Actor.Attributes.Movement
{

    public class Walking
    {
        internal string ActorName { get; set; }
        public bool IsEnabled => Key != '\u0000';
        private int _Speed;
        public int Speed
        {
            get => _Speed;
            set
            {
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
            {
                Memory.IW4.SendDvar($"bind {Key} say");
                Key = '\u0000';
                Speed = 0;
            }
        }
    }

}
