namespace ActorConsole.Core.Actor
{
    public class Weapons
    {
        internal string ActorName { get; set; }
        private void ApplyGunToBone(string boneName)
        {
            Memory.IW4.SendDvar($"mvm_actor_weapon {ActorName} {boneName} {j_gun}");
        }
        private string? _j_gun;
        public string? j_gun
        {
            get => _j_gun;
            set
            {
                _j_gun = value;
                ApplyGunToBone("j_gun");
            }
        }
        private string? _tag_stowed_back;
        public string? tag_stowed_back
        {
            get => _tag_stowed_back;
            set
            {
                _tag_stowed_back = value;
                ApplyGunToBone("tag_stowed_back");
            }
        }
        private string? _tag_inhand;
        public string? tag_inhand
        {
            get => _tag_inhand;
            set
            {
                _tag_inhand = value;
                ApplyGunToBone("tag_inhand");
            }
        }
        private string? _tag_weapon_right;
        public string? tag_weapon_right
        {
            get => _tag_weapon_right;
            set
            {
                _tag_inhand = value;
                ApplyGunToBone("tag_weapon_right");
            }
        }
        private string? _tag_weapon_left;
        public string? tag_weapon_left
        {
            get => _tag_weapon_left;
            set
            {
                _tag_inhand = value;
                ApplyGunToBone("tag_weapon_left");
            }
        }
        private string? _tag_weapon_chest;
        public string? tag_weapon_chest
        {
            get => _tag_weapon_chest;
            set
            {
                _tag_inhand = value;
                ApplyGunToBone("tag_weapon_chest");
            }
        }
        private string? _tag_stowed_hip_rear;
        public string? tag_stowed_hip_rear
        {
            get => _tag_stowed_hip_rear;
            set
            {
                _tag_inhand = value;
                ApplyGunToBone("tag_stowed_hip_rear");
            }
        }
    }
}
