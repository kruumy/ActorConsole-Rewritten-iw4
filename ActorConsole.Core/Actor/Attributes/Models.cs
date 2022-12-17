namespace ActorConsole.Core.Actor.Attributes
{
    /// <summary>
    /// Attribute class to hold all the information about the parant actors models.
    /// Is equilivant to /mvm_actor_model.
    /// </summary>
    public sealed class Models : Attribute
    {
        internal Models(Actor _ParentActor) : base(_ParentActor) { }

        internal const string Head_Default = "defaultactor";
        internal const string Body_Default = "defaultactor";

        private string _Head = Head_Default;
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
        private string _Body = Body_Default;
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
