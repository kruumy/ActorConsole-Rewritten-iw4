namespace ActorConsole.Core.Actor.Attributes
{
    /// <summary>
    /// Attribute class to hold all the information about the parant actors pathing.
    /// Is equilivant to /mvm_actor_path.
    /// </summary>
    public sealed class Pathing : Attribute
    {
        internal Pathing(Actor _ParentActor) : base(_ParentActor)
        {
        }

        /// <summary>
        /// Determines if the pathing attribute is enabled.
        /// Checks if NodeCount > 0.
        /// </summary>
        public bool IsEnabled => NodeCount > 0;

        private uint _Speed = 0;

        /// <summary>
        /// The speed for the pathing to be played back at.
        /// Higher = faster.
        /// </summary>
        public uint Speed
        {
            get => _Speed;
            set
            {
                _Speed = value;
                Manager.RaiseOnActorAttributeModified(this);
            }
        }

        private uint NextNode = 1;

        /// <summary>
        /// The total number of nodes attached to the actor.
        /// </summary>
        public uint NodeCount => NextNode - 1;

        /// <summary>
        /// Creates a node at the players current position.
        /// </summary>
        /// <returns>The index of the node placed.</returns>
        public uint CreateNode()
        {
            if (NextNode <= 13)
            {
                Memory.IW4.SendDvar($"mvm_actor_path_save {ParentActor.Name} {NextNode}");
                uint result = NextNode;
                NextNode++;
                return result;
            }
            else
                return 0;
        }

        /// <summary>
        /// Deleted the last node placed.
        /// </summary>
        /// <returns>The index of the last node deleted.</returns>
        public uint DeleteLastNode()
        {
            if (IsEnabled)
            {
                Memory.IW4.SendDvar($"mvm_actor_path_del {ParentActor.Name} {NextNode}");
                uint result = NextNode;
                NextNode--;
                return result;
            }
            else
                return 0;
        }

        /// <summary>
        /// Plays the path of all the nodes.
        /// </summary>
        public void Play()
        {
            Memory.IW4.SendDvar($"mvm_actor_path_walk {ParentActor.Name} {Speed}");
        }

        /// <summary>
        /// Plays the path of all the nodes.
        /// </summary>
        /// <param name="speed">Overrides the speed instead of manually assigning it to the property.</param>
        public void Play(int speed)
        {
            Memory.IW4.SendDvar($"mvm_actor_path_walk {ParentActor.Name} {speed}");
        }
    }
}