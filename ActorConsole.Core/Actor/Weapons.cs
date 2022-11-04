namespace ActorConsole.Core.Actor
{
    public class Weapons
    {
        internal string ActorName { get; set; }
        private string? _j_gun;
        public string? j_gun
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
        public void Refresh()
        {
            if (j_gun != null)
                Memory.IW4.SendDvar($"mvm_actor_weapon {ActorName} j_gun {j_gun}");
        }
    }
}
