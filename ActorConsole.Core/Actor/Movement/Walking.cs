namespace ActorConsole.Core.Actor.Movement
{

    internal class Walking
    {
        internal string ActorName { get; set; }
        private int _Speed { get; set; }
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
        private string Command { get { return $"mvm_actor_walk {ActorName} {Speed}"; } }

        public Walking(char key, int speed)
        {
            ActorName = Actor.NextActorName;
            Speed = speed;
            Key = key;
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
