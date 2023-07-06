namespace ActorConsole.Core.CompositedActorProperties
{
    public class Walking : CompositedActorProperty, IKeybindable
    {
        private DirectionType _Direction = DirectionType.forward;
        private bool _OnActorBack;
        private float _Speed;
        private bool _StopOnDeath;

        internal Walking( Actor Parent ) : base(Parent)
        {
        }


        public enum DirectionType
        {
            forward,
            backward,
            left,
            right,
            down,
            up
        }

        public DirectionType Direction
        {
            get => _Direction;
            set
            {
                _Direction = value;
                Manager.Instance.Game.Send($"mvm_actor_walk {Parent.Name} {Speed} {value}");
                RaisePropertyChanged(nameof(Direction));
            }
        }

        public bool OnActorBack
        {
            get => _OnActorBack; set
            {
                _OnActorBack = value;
                Manager.Instance.Game.Send($"mvm_actor_walk_actorback {Parent.Name}");
                RaisePropertyChanged(nameof(OnActorBack));
            }
        }

        public float Speed
        {
            get => _Speed; set
            {
                _Speed = value;
                Manager.Instance.Game.Send($"mvm_actor_walk {Parent.Name} {value} {Direction}");
                RaisePropertyChanged(nameof(Speed));
            }
        }

        public bool StopOnDeath
        {
            get => _StopOnDeath; set
            {
                _StopOnDeath = value;
                Manager.Instance.Game.Send($"mvm_actor_walk_autostop {Parent.Name}");
                RaisePropertyChanged(nameof(StopOnDeath));
            }
        }

        public void Play()
        {
            Manager.Instance.Game.Send($"mvm_actor_walk {Parent.Name} {Speed} {Direction}");
        }

        public void Keybind( char key )
        {
            Manager.Instance.Game.Send($"bind {key} \"mvm_actor_walk {Parent.Name} {Speed} {Direction}\"");
        }
    }
}
