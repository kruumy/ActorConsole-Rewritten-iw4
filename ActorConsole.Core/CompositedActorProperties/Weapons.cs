﻿using System.ComponentModel;
using System.Reflection;

namespace ActorConsole.Core.CompositedActorProperties
{
    public class Weapons : CompositedActorProperty
    {
        public class Weapon : INotifyPropertyChanged
        {
            private string name;
            private string camo;

            public Weapon()
            {
            }
            public Weapon( string name, string camo )
            {
                Name = name;
                Camo = camo;
            }
            public string Name
            {
                get => name;
                set
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
            public string Camo
            {
                get
                {
                    if ( string.IsNullOrEmpty(camo) )
                    {
                        Camo = Core.Manager.Instance.Game.CamoEnum?.GetEnumNames()[ 0 ];
                    }
                    return camo;
                }

                set
                {
                    camo = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Camo)));
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public override string ToString()
            {
                string realCamo = Camo == "none" ? string.Empty : Camo.ToString();
                return $"{Name} {realCamo}".Trim();
            }
        }

        private Weapon _j_gun = new Weapon();

        private Weapon _tag_inhand = new Weapon();

        private Weapon _tag_stowed_back = new Weapon();

        private Weapon _tag_stowed_hip_rear = new Weapon();

        private Weapon _tag_weapon_chest = new Weapon();

        private Weapon _tag_weapon_left = new Weapon();

        private Weapon _tag_weapon_right = new Weapon();

        internal Weapons( Actor Parent ) : base(Parent)
        {
        }

#pragma warning disable IDE1006

        public Weapon j_gun

        {
            get => _j_gun;
            set
            {
                _j_gun = value;
                Manager.Instance.Game.Send($"mvm_actor_weapon {Parent.Name} {nameof(j_gun)} {value}");
                RaisePropertyChanged(nameof(j_gun));
            }
        }

        public Weapon tag_inhand

        {
            get => _tag_inhand;
            set
            {
                _tag_inhand = value;
                Manager.Instance.Game.Send($"mvm_actor_weapon {Parent.Name} {nameof(tag_inhand)} {value}");
                RaisePropertyChanged(nameof(tag_inhand));
            }
        }

        public Weapon tag_stowed_back

        {
            get => _tag_stowed_back;
            set
            {
                _tag_stowed_back = value;
                Manager.Instance.Game.Send($"mvm_actor_weapon {Parent.Name} {nameof(tag_stowed_back)} {value}");
                RaisePropertyChanged(nameof(tag_stowed_back));
            }
        }

        public Weapon tag_stowed_hip_rear

        {
            get => _tag_stowed_hip_rear;
            set
            {
                _tag_stowed_hip_rear = value;
                Manager.Instance.Game.Send($"mvm_actor_weapon {Parent.Name} {nameof(tag_stowed_hip_rear)} {value}");
                RaisePropertyChanged(nameof(tag_stowed_hip_rear));
            }
        }

        public Weapon tag_weapon_chest

        {
            get => _tag_weapon_chest;
            set
            {
                _tag_weapon_chest = value;
                Manager.Instance.Game.Send($"mvm_actor_weapon {Parent.Name} {nameof(tag_weapon_chest)} {value}");
                RaisePropertyChanged(nameof(tag_weapon_chest));
            }
        }

        public Weapon tag_weapon_left

        {
            get => _tag_weapon_left;
            set
            {
                _tag_weapon_left = value;
                Manager.Instance.Game.Send($"mvm_actor_weapon {Parent.Name} {nameof(tag_weapon_left)} {value}");
                RaisePropertyChanged(nameof(tag_weapon_left));
            }
        }

        public Weapon tag_weapon_right

        {
            get => _tag_weapon_right;
            set
            {
                _tag_weapon_right = value;
                Manager.Instance.Game.Send($"mvm_actor_weapon {Parent.Name} {nameof(tag_weapon_right)} {value}");
                RaisePropertyChanged(nameof(tag_weapon_right));
            }
        }

#pragma warning restore IDE1006

        public void Set( string bone, string gun, string camo )
        {
            PropertyInfo prop = typeof(Weapons).GetProperty(bone, BindingFlags.Public | BindingFlags.Instance);
            prop?.SetValue(this, new Weapon(gun, camo));
        }
    }
}
