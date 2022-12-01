namespace ActorConsole.Core.Actor
{
    public class Anims
    {
        internal string ActorName { get; set; }
        public const string Idle_Default = "pb_stand_alert";
        public const string Death_Default = "pb_stand_death_leg_kickup";

        private string _Idle = Idle_Default;
        public string Idle
        {
            get => _Idle;
            set
            {
                _Idle = value;
                Memory.IW4.SendDvar($"mvm_actor_anim {ActorName} {Idle}");
            }
        }
        private string _Death = Death_Default;
        public string Death
        {
            get => _Death;
            set
            {
                _Death = value;
                Memory.IW4.SendDvar($"mvm_actor_death {ActorName} {Death}");
            }
        }
    }
}
