namespace ActorConsole.Core.Actor
{
    public class Anims
    {
        internal string ActorName { get; set; }

        private string _Idle;
        public string Idle
        {
            get
            {
                return _Idle;
            }
            set
            {
                _Idle = value;
                Refresh();
            }
        }
        private string _Death;
        public string Death
        {
            get
            {
                return _Death;
            }
            set
            {
                _Death = value;
                Refresh();
            }
        }
        public Anims(string idle = "pb_stand_alert", string death = "pb_stand_death_neckdeath_thrash")
        {
            Idle = idle;
            Death = death;
        }
        private void Refresh()
        {
            Memory.IW4.SendDvar($"mvm_actor_anim {ActorName} {Idle}");
            Memory.IW4.SendDvar($"mvm_actor_death {ActorName} {Death}");
        }


    }
}
