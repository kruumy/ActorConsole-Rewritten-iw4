namespace ActorConsole.Core.Actor
{
    public class Actor
    {
        private static int Amount;
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                this.Anims.ActorName = value;
                this.Models.ActorName = value;
                this.Weapons.ActorName = value;
            }
        }
        public Anims Anims { get; set; }
        public Models Models { get; set; }
        public Weapons Weapons { get; set; }

        public Actor(string? name = null, Anims? anims = null, Models? models = null, Weapons? weapons = null)
        {
            Amount++;

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

        }
    }
}
