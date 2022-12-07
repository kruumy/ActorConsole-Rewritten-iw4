namespace ActorConsole.Core.Actor.Attributes
{
    public sealed class Models : Attribute
    {
        internal Models(Actor _ParentActor) : base(_ParentActor)
        {
        }

        internal const string Head_Default = "defaultactor";
        internal const string Body_Default = "defaultactor";

        private string _Head = Head_Default;

        public string Head
        {
            get => _Head;
            set
            {
                _Head = value;
                Memory.IW4.SendDvar($"mvm_actor_model {ParentActor.Name} {Body} {Head}");
                Manager.RaiseActorPropertyChanged(this);
            }
        }

        private string _Body = Body_Default;

        public string Body
        {
            get => _Body;
            set
            {
                _Body = value;
                Memory.IW4.SendDvar($"mvm_actor_model {ParentActor.Name} {Body} {Head}");
                Manager.RaiseActorPropertyChanged(this);
            }
        }
    }
}