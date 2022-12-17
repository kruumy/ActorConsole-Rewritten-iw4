namespace ActorConsole.Core.Actor.Attributes
{
    /// <summary>
    /// Attribute class to hold all the information about the parant actors walking.
    /// Is equilivant to /mvm_actor_walk
    /// </summary>
    public sealed class Walking : Attribute
    {
        internal Walking(Actor _ParentActor) : base(_ParentActor) { }
        /// <summary>
        /// Determines if the walking attribute is enabled or not.
        /// Basically if key is undefined.
        /// </summary>
        public bool IsEnabled => Key != '\u0000';
        private int _Speed;
        /// <summary>
        /// The speed the actor will walk at.
        /// Higher = slower.
        /// </summary>
        public int Speed
        {
            get => _Speed;
            set
            {
                _Speed = value;
                CreateBind();
                Manager.RaiseOnActorAttributeModified(this);
            }
        }
        private char _Key;
        /// <summary>
        /// The key to bind the actor walk dvar to.
        /// </summary>
        public char Key
        {
            get => _Key;
            set
            {
                _Key = value;
                CreateBind();
                Manager.RaiseOnActorAttributeModified(this);
            }
        }
        private string Command => $"mvm_actor_walk {ParentActor.Name} {Speed}";
        /// <summary>
        /// Plays the actor walk without a bind.
        /// </summary>
        public void Play()
        {
            Memory.IW4.SendDvar(Command);
        }
        private void CreateBind()
        {
            if (IsEnabled)
                Memory.IW4.SendDvar($"bind {Key} \"{Command}\"");
        }
        /// <summary>
        /// Removes the bind if walking is enabled.
        /// </summary>
        public void RemoveBind()
        {
            if (IsEnabled)
            {
                Memory.IW4.SendDvar($"bind {Key} say");
                Key = '\u0000';
                Speed = 0;
            }
        }
    }

}
