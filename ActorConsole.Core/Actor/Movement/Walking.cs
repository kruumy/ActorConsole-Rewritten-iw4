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
        private string? Command
        {
            get
            {
                if (Speed != 0)
                    return $"mvm_actor_walk {ActorName} {Speed}";
                else
                    return null;
            }
        }
        public void Play()
        {
            Memory.IW4.SendDvar(Command);
        }
        private void CreateBind()
        {
            if (Key != '\u0000' && Command != null)
                Memory.IW4.SendDvar($"bind {Key} \"{Command}\"");
        }
        public void RemoveBind()
        {
            if (Key != '\u0000' && Command != null)
                Memory.IW4.SendDvar($"bind {Key} say");
        }
    }

}
