namespace ActorConsole.Core.CompositedActorProperties
{
    public class Anims : CompositedActorProperty
    {
        public static readonly string DEATH_DEFAULT = "pb_stand_death_chest_blowback";
        public static readonly string IDLE_DEFAULT = "pb_stand_alert";
        private string _Death = DEATH_DEFAULT;
        private string _Idle = IDLE_DEFAULT;

        internal Anims( Actor Parent ) : base(Parent)
        {
        }

        public string Death
        {
            get => _Death;
            set
            {
                _Death = value;
                Memory.IW4.Send($"mvm_actor_death {Parent.Name} {value}");
                RaisePropertyChanged(nameof(Death));
            }
        }

        public string Idle
        {
            get => _Idle;
            set
            {
                _Idle = value;
                Memory.IW4.Send($"mvm_actor_anim {Parent.Name} {value}");
                RaisePropertyChanged(nameof(Idle));
            }
        }
    }
}
