namespace ActorConsole.Core.Actor.Attributes
{
    /// <summary>
    /// Attribute class to hold all the information about the parant actors models.
    /// Is equilivant to /mvm_actor_model.
    /// </summary>
    public sealed class Models : Attribute
    {
        internal Models(Actor _ParentActor) : base(_ParentActor)
        {
        }

        /// <summary>
        /// "defaultactor"
        /// </summary>
        public static readonly string HEAD_DEFAULT = "defaultactor";
        /// <summary>
        /// "defaultactor"
        /// </summary>
        public static readonly string BODY_DEFAULT = "defaultactor";

        private string _Head = HEAD_DEFAULT;

        /// <summary>
        /// The head model of the actor.
        /// </summary>
        public string Head
        {
            get => _Head;
            set
            {
                _Head = value;
                Memory.IW4.SendDvar($"mvm_actor_model {ParentActor.Name} {Body} {Head}");
                Manager.RaiseOnActorAttributeModified(this);
            }
        }

        private string _Body = BODY_DEFAULT;

        /// <summary>
        /// The body model of the actor.
        /// </summary>
        public string Body
        {
            get => _Body;
            set
            {
                _Body = value;
                Memory.IW4.SendDvar($"mvm_actor_model {ParentActor.Name} {Body} {Head}");
                Manager.RaiseOnActorAttributeModified(this);
            }
        }
    }
}