namespace ActorConsole.Core.Actor.Attributes
{
    /// <summary>
    /// Attribute class to hold all the information about the parant actors weapons.
    /// Is equilivant to /mvm_actor_weapon
    /// </summary>
    public sealed class Weapons : Attribute
    {
        /// <summary>
        /// "default_weapon"
        /// </summary>
        public static readonly string NO_WEAPON = "default_weapon";
        internal Weapons(Actor _ParentActor) : base(_ParentActor)
        {
        }

        private void ApplyGunToBone(string boneName, string gunName)
        {
            gunName = gunName.Trim();
            if (string.IsNullOrEmpty(gunName))
            {
                gunName = NO_WEAPON;
            }
            Memory.IW4.SendDvar($"mvm_actor_weapon {ParentActor.Name} {boneName} {gunName}");
        }

        private string _j_gun = null;

        /// <summary>
        /// Bone primarily used as the right hand for a gun.
        /// </summary>
        public string j_gun
        {
            get => _j_gun;
            set
            {
                _j_gun = value;
                ApplyGunToBone(nameof(j_gun), value);
                Manager.RaiseOnActorAttributeModified(this);
            }
        }

        private string _tag_stowed_back = null;

        /// <summary>
        /// Bone primarily used to store weapons on the back of the acotr. Like it is a secondary weapon.
        /// </summary>
        public string tag_stowed_back
        {
            get => _tag_stowed_back;
            set
            {
                _tag_stowed_back = value;
                ApplyGunToBone(nameof(tag_stowed_back), value);
                Manager.RaiseOnActorAttributeModified(this);
            }
        }

        private string _tag_inhand = null;

        /// <summary>
        /// Bone primarily used for C4 walking animation.
        /// </summary>
        public string tag_inhand
        {
            get => _tag_inhand;
            set
            {
                _tag_inhand = value;
                ApplyGunToBone(nameof(tag_inhand), value);
                Manager.RaiseOnActorAttributeModified(this);
            }
        }

        private string _tag_weapon_right = null;

        /// <summary>
        /// Bone primarily used for when j_gun does not work.
        /// </summary>
        public string tag_weapon_right
        {
            get => _tag_weapon_right;
            set
            {
                _tag_inhand = value;
                ApplyGunToBone(nameof(tag_weapon_right), value);
                Manager.RaiseOnActorAttributeModified(this);
            }
        }

        private string _tag_weapon_left = null;

        /// <summary>
        /// Bone primarily used for the left hand. Useful for faking akimbo.
        /// </summary>
        public string tag_weapon_left
        {
            get => _tag_weapon_left;
            set
            {
                _tag_inhand = value;
                ApplyGunToBone(nameof(tag_weapon_left), value);
                Manager.RaiseOnActorAttributeModified(this);
            }
        }

        private string _tag_weapon_chest = null;

        /// <summary>
        /// Bone for something, I forgot
        /// </summary>
        public string tag_weapon_chest
        {
            get => _tag_weapon_chest;
            set
            {
                _tag_inhand = value;
                ApplyGunToBone(nameof(tag_weapon_chest), value);
                Manager.RaiseOnActorAttributeModified(this);
            }
        }

        private string _tag_stowed_hip_rear = null;

        /// <summary>
        /// Bone primarily used to grenades or claymores.
        /// </summary>
        public string tag_stowed_hip_rear
        {
            get => _tag_stowed_hip_rear;
            set
            {
                _tag_inhand = value;
                ApplyGunToBone(nameof(tag_stowed_hip_rear), value);
                Manager.RaiseOnActorAttributeModified(this);
            }
        }

        internal void NullAllBones()
        {

            foreach (System.Reflection.PropertyInfo prop in typeof(Weapons).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if (!string.IsNullOrEmpty((string)prop.GetValue(this)))
                {
                    prop.SetValue(this, NO_WEAPON);
                }
            }
        }
    }
}