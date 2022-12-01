namespace ActorConsole.Core.Actor
{
    public class Weapons
    {
        internal string ActorName { get; set; }
        public static readonly string[] Bones = new string[]
        {
            "j_gun"
        };
        private string? _j_gun;
        public string? j_gun
        {
            get => _j_gun;
            set
            {
                _j_gun = value;
                Memory.IW4.SendDvar($"mvm_actor_weapon {ActorName} j_gun {j_gun}");
            }
        }
    }
}
