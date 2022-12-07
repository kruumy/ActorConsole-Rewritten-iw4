namespace ActorConsole.Core.Actor.Attributes
{
    public sealed class Walking : Attribute
    {
        internal Walking(Actor _ParentActor) : base(_ParentActor)
        {
        }

        public bool IsEnabled => Key != '\u0000';
        private int _Speed;

        public int Speed
        {
            get => _Speed;
            set
            {
                _Speed = value;
                CreateBind();
                Manager.RaiseActorPropertyChanged(this);
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
                Manager.RaiseActorPropertyChanged(this);
            }
        }

        private string Command => $"mvm_actor_walk {ParentActor.Name} {Speed}";

        public void Play()
        {
            Memory.IW4.SendDvar(Command);
        }

        private void CreateBind()
        {
            if (IsEnabled)
                Memory.IW4.SendDvar($"bind {Key} \"{Command}\"");
        }

        public void RemoveBind()
        {
            if (IsEnabled)
            {
                Memory.IW4.SendDvar($"bind {Key} say");
                Key = '\u0000';
                Speed = 0;
            }
        }
    }
}