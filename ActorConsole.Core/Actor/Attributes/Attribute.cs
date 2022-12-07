namespace ActorConsole.Core.Actor.Attributes
{
    public abstract class Attribute
    {
        internal Actor ParentActor { get; }

        internal Attribute(Actor _ParentActor)
        {
            ParentActor = _ParentActor;
        }
    }
}