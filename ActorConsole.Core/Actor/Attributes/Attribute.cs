namespace ActorConsole.Core.Actor.Attributes
{
    /// <summary>
    /// Base class of all the actor attibutes. Keeps track of the parent actor attribute is assigned to.
    /// </summary>
    public abstract class Attribute
    {
        /// <summary>
        /// The parent actor that the attribute is assigned to.
        /// </summary>
        internal Actor ParentActor { get; }

        internal Attribute(Actor _ParentActor)
        {
            ParentActor = _ParentActor;
        }
    }
}