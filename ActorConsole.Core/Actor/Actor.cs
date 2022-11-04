namespace ActorConsole.Core.Actor
{
    public class Actor
    {
        private static int Amount = 1;
        internal static string NextActorName { get { return $"actor{Amount}"; } }
        private string _Name = NextActorName;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if(_Name != value)
                {
                    Memory.IW4.SendDvar($"mvm_actor_rename {_Name} {value}");
                    _Name = value;
                    this.Anims.ActorName = value;
                    this.Models.ActorName = value;
                    this.Weapons.ActorName = value;
                }
                
            }
        }
        public Anims Anims { get; set; }
        public Models Models { get; set; }
        public Weapons Weapons { get; set; }

        public Actor(string? name = null, Anims? anims = null, Models? models = null, Weapons? weapons = null)
        {
            SpawnDefault();

            if (anims != null)
                Anims = anims;
            else
                Anims = new Anims();

            if (models != null)
                Models = models;
            else
                Models = new Models();

            if (weapons != null)
                Weapons = weapons;
            else
                Weapons = new Weapons();

            if (name != null)
                Name = name;
            else
                Name = $"actor{Amount}";

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
    }
}
