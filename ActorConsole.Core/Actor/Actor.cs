namespace ActorConsole.Core.Actor
{
    public class Actor
    {
        private static int Amount = 1;
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name != null)
                    Memory.IW4.SendDvar($"mvm_actor_rename {_Name} {value}");
                _Name = value;
                this.Anims.ActorName = value;
                this.Models.ActorName = value;
                this.Weapons.ActorName = value;
            }
        }
        public Anims Anims { get; private set; }
        public Models Models { get; private set; }
        public Weapons Weapons { get; private set; }
        public Movement.Walking Movement_Walking { get; private set; }
        public Movement.Pathing Movement_Pathing { get; private set; }



        public Actor()
        {
            SpawnDefault();
            Anims = new Anims();
            Models = new Models();
            Weapons = new Weapons();
            Movement_Pathing = new Movement.Pathing();
            Movement_Walking = new Movement.Walking();

            Name = $"actor{Amount}";
            MoveToCurrentPostition();

            Amount++;
        }
        private void SpawnDefault()
        {
            Memory.IW4.SendDvar($"mvm_actor_spawn {Models.Body_Default} {Models.Head_Default}");
        }
        public void MoveToCurrentPostition()
        {
            Memory.IW4.SendDvar($"mvm_actor_move {Name}");
        }

        internal void Delete()
        {
            Memory.IW4.SendDvar($"mvm_actor_delete {Name}");
        }
    }
}
