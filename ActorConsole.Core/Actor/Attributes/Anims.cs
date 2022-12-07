namespace ActorConsole.Core.Actor.Attributes
{
    public sealed class Anims : Attribute
    {
        internal Anims(Actor _ParentActor) : base(_ParentActor)
        {
        }

        internal const string Idle_Default = "pb_stand_alert";
        internal const string Death_Default = "pb_stand_death_leg_kickup";

        private string _Idle = Idle_Default;

        public string Idle
        {
            get => _Idle;
            set
            {
                _Idle = value;
                Memory.IW4.SendDvar($"mvm_actor_anim {ParentActor.Name} {Idle}");
                Manager.RaiseActorPropertyChanged(this);
            }
        }

        private string _Death = Death_Default;

        public string Death
        {
            get => _Death;
            set
            {
                _Death = value;
                Memory.IW4.SendDvar($"mvm_actor_death {ParentActor.Name} {Death}");
                Manager.RaiseActorPropertyChanged(this);
            }
        }
    }
}