namespace ActorConsole.Core.Actor
{
    public class Weapons
    {
        internal string ActorName { get; set; }
        private string _j_gun;
        public string j_gun
        {
            get
            {
                return _j_gun;
            }
            set
            {
                _j_gun = value;
                Refresh();
            }
        }


        public Weapons(string? __j_gun = null)
        {
            if (__j_gun != null)
                j_gun = __j_gun;
        }
        private void Refresh()
        {
            if (j_gun != null)
                Memory.IW4.SendDvar($"mvm_actor_weapons {ActorName} j_gun {j_gun}");
        }
    }
}
