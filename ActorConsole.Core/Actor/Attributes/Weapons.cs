namespace ActorConsole.Core.Actor.Attributes
{
    public class Weapons
    {
        internal string ActorName { get; set; }
        private void ApplyGunToBone(string boneName, string? gunName)
        {
            gunName ??= "";
            Memory.IW4.SendDvar($"mvm_actor_weapon {ActorName} {boneName} {gunName}");
        }
        private string? _j_gun = null;
        public string? j_gun
        {
            get => _j_gun;
            set
            {
                _j_gun = value;
                ApplyGunToBone("j_gun", value);
            }
        }
        private string? _tag_stowed_back = null;
        public string? tag_stowed_back
        {
            get => _tag_stowed_back;
            set
            {
                _tag_stowed_back = value;
                ApplyGunToBone("tag_stowed_back", value);
            }
        }
        private string? _tag_inhand = null;
        public string? tag_inhand
        {
            get => _tag_inhand;
            set
            {
                _tag_inhand = value;
                ApplyGunToBone("tag_inhand", value);
            }
        }
        private string? _tag_weapon_right = null;
        public string? tag_weapon_right
        {
            get => _tag_weapon_right;
            set
            {
                _tag_inhand = value;
                ApplyGunToBone("tag_weapon_right", value);
            }
        }
        private string? _tag_weapon_left = null;
        public string? tag_weapon_left
        {
            get => _tag_weapon_left;
            set
            {
                _tag_inhand = value;
                ApplyGunToBone("tag_weapon_left", value);
            }
        }
        private string? _tag_weapon_chest = null;
        public string? tag_weapon_chest
        {
            get => _tag_weapon_chest;
            set
            {
                _tag_inhand = value;
                ApplyGunToBone("tag_weapon_chest", value);
            }
        }
        private string? _tag_stowed_hip_rear = null;
        public string? tag_stowed_hip_rear
        {
            get => _tag_stowed_hip_rear;
            set
            {
                _tag_inhand = value;
                ApplyGunToBone("tag_stowed_hip_rear", value);
            }
        }
    }
}
