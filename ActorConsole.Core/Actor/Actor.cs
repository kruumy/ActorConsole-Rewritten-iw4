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
                this.Movement_Walking.ActorName = value;
                this.Movement_Pathing.ActorName = value;
            }
        }
        public Anims Anims { get; private set; }
        public Models Models { get; private set; }
        public Weapons Weapons { get; private set; }
        public bool IsMovement_Walking => Movement_Walking.Key != '\u0000';
        public bool IsMovement_Pathing => Movement_Pathing.NodeCount > 0;
        public Movement.Walking Movement_Walking { get; private set; }
        public Movement.Pathing Movement_Pathing { get; private set; }



        public Actor()
        {
            Memory.IW4.SendDvar($"mvm_actor_spawn {Models.Body_Default} {Models.Head_Default}");
            Anims = new Anims();
            Models = new Models();
            Weapons = new Weapons();
            Movement_Pathing = new Movement.Pathing();
            Movement_Walking = new Movement.Walking();

            Name = $"actor{Amount}";

            Amount++;
        }
        public void MoveToCurrentPostition()
        {
            Memory.IW4.SendDvar($"mvm_actor_move {Name}");
        }

        internal void Delete()
        {
            // TODO: delete all weapons on actor before deleting actor to not have floating weapon bug
            Memory.IW4.SendDvar($"mvm_actor_delete {Name}");
        }
    }
}
