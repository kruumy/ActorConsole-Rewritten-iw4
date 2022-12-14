using ActorConsole.Core.Actor.Attributes;
using System;

namespace ActorConsole.Core.Actor
{
    public sealed class Actor : IDisposable
    {
        internal static int Amount = 1;
        private string _Name;

        public string Name
        {
            get => _Name;
            set
            {
                if (_Name != null)
                    Memory.IW4.SendDvar($"mvm_actor_rename {_Name} {value}");
                _Name = value;
            }
        }

        public Anims Anims { get; }
        public Models Models { get; }
        public Weapons Weapons { get; }
        public Walking Walking { get; }
        public Pathing Pathing { get; }

        internal Actor()
        {
            Memory.IW4.SendDvar($"mvm_actor_spawn {Models.Body_Default} {Models.Head_Default}");
            Anims = new Anims(this);
            Models = new Models(this);
            Weapons = new Weapons(this);
            Pathing = new Pathing(this);
            Walking = new Walking(this);

            Name = $"actor{Amount}";

            Amount++;
        }

        public void MoveToCurrentPostition()
        {
            Memory.IW4.SendDvar($"mvm_actor_move {Name}");
        }

        public void Dispose()
        {
            this.Weapons.j_gun = null;
            this.Weapons.tag_inhand = null;
            this.Weapons.tag_stowed_back = null;
            this.Weapons.tag_stowed_hip_rear = null;
            this.Weapons.tag_weapon_chest = null;
            this.Weapons.tag_weapon_left = null;
            this.Weapons.tag_weapon_right = null;
            Memory.IW4.SendDvar($"mvm_actor_delete {Name}");
            GC.SuppressFinalize(this);
        }
    }
}