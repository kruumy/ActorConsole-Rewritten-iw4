using ActorConsole.Core.Actor.Attributes;

namespace ActorConsole.Core.Actor
{
    public class Actor
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
                this.Anims.ActorName = value;
                this.Models.ActorName = value;
                this.Weapons.ActorName = value;
                this.Walking.ActorName = value;
                this.Pathing.ActorName = value;
                Manager.RaiseActorPropertyChanged(this);
            }
        }
        public Anims Anims { get; private set; }
        public Models Models { get; private set; }
        public Weapons Weapons { get; private set; }
        public Walking Walking { get; private set; }
        public Pathing Pathing { get; private set; }
        public Actor()
        {
            Memory.IW4.SendDvar($"mvm_actor_spawn {Models.Body_Default} {Models.Head_Default}");
            Anims = new Anims();
            Models = new Models();
            Weapons = new Weapons();
            Pathing = new Pathing();
            Walking = new Walking();

            Name = $"actor{Amount}";

            Amount++;
        }
        public void MoveToCurrentPostition()
        {
            Memory.IW4.SendDvar($"mvm_actor_move {Name}");
        }
        public void Delete()
        {
            this.Weapons.j_gun = null;
            this.Weapons.tag_inhand = null;
            this.Weapons.tag_stowed_back = null;
            this.Weapons.tag_stowed_hip_rear = null;
            this.Weapons.tag_weapon_chest = null;
            this.Weapons.tag_weapon_left = null;
            this.Weapons.tag_weapon_right = null;
            Memory.IW4.SendDvar($"mvm_actor_delete {Name}");
        }
    }
}
