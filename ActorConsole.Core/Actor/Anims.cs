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
            get
            {
                return _Idle;
            }
            set
            {
                _Idle = value;
                Refresh("idle");
            }
        }
        private string _Death = Death_Default;
        public string Death
        {
            get
            {
                return _Death;
            }
            set
            {
                _Death = value;
                Refresh("death");
            }
        }
        public void Refresh()
        {
            Memory.IW4.SendDvar($"mvm_actor_anim {ActorName} {Idle}");
            Memory.IW4.SendDvar($"mvm_actor_death {ActorName} {Death}");
        }
        public void Refresh(string option)
        {
            switch (option.ToLower())
            {
                case "idle":
                    {
                        Memory.IW4.SendDvar($"mvm_actor_anim {ActorName} {Idle}");
                        break;
                    }
                case "death":
                    {
                        Memory.IW4.SendDvar($"mvm_actor_death {ActorName} {Death}");
                        break;
                    }
            }
        }


    }
}
