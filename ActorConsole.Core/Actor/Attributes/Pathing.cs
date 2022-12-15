namespace ActorConsole.Core.Actor.Attributes
{
    public sealed class Pathing : Attribute
    {
        internal Pathing(Actor _ParentActor) : base(_ParentActor)
        {
        }

        public bool IsEnabled => NodeCount > 0;
        private uint _Speed = 0;

        public uint Speed
        {
            get => _Speed;
            set
            {
                _Speed = value;
                Manager.RaiseActorPropertyChanged(this);
            }
        }

        private uint NextNode = 1;

        public uint NodeCount => NextNode - 1;

        public uint CreateNode()
        {
            if (NextNode <= 13)
            {
                Memory.IW4.SendDvar($"mvm_actor_path_save {ParentActor.Name} {NextNode}");
                uint result = NextNode;
                NextNode++;
                return result;
            }
            else
                return 0;
        }

        public uint DeleteLastNode()
        {
            if (IsEnabled)
            {
                Memory.IW4.SendDvar($"mvm_actor_path_del {ParentActor.Name} {NextNode}");
                uint result = NextNode;
                NextNode--;
                return result;
            }
            else
                return 0;
        }

        public void Play()
        {
            Memory.IW4.SendDvar($"mvm_actor_path_walk {ParentActor.Name} {Speed}");
        }
        public void Play(int speed)
        {
            Memory.IW4.SendDvar($"mvm_actor_path_walk {ParentActor.Name} {speed}");
        }
    }
}