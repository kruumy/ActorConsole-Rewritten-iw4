namespace ActorConsole.Core.Actor.Attributes
{
    /// <summary>
    /// Attribute class to hold all the information about the parant actors animations.
    /// Is equilivant to /mvm_actor_anim.
    /// </summary>
    public sealed class Anims : Attribute
    {
        internal Anims(Actor _ParentActor) : base(_ParentActor)
        {
        }

        internal const string Idle_Default = "pb_stand_alert";
        internal const string Death_Default = "pb_stand_death_chest_blowback";

        private string _Idle = Idle_Default;
        /// <summary>
        /// The idle animation of the actor.
        /// Is played on actor reset.
        /// </summary>
        public string Idle
        {
            get => _Idle;
            set
            {
                _Idle = value;
                Memory.IW4.SendDvar($"mvm_actor_anim {ParentActor.Name} {Idle}");
                Manager.RaiseOnActorAttributeModified(this);
            }
        }

        private string _Death = Death_Default;
        /// <summary>
        /// The death animation of the actor.
        /// Is played on actor dying.
        /// </summary>
        public string Death
        {
            get => _Death;
            set
            {
                _Death = value;
                Memory.IW4.SendDvar($"mvm_actor_death {ParentActor.Name} {Death}");
                Manager.RaiseOnActorAttributeModified(this);
            }
        }
    }
}