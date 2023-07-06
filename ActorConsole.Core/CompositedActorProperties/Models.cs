namespace ActorConsole.Core.CompositedActorProperties
{
    public class Models : CompositedActorProperty
    {
        public static readonly string BODY_DEFAULT = "defaultactor";
        public static readonly string HEAD_DEFAULT = "defaultactor";
        private string _Body = BODY_DEFAULT;
        private string _Head = HEAD_DEFAULT;

        internal Models( Actor Parent ) : base(Parent)
        {
        }

        public string Body
        {
            get => _Body;
            set
            {
                _Body = value;
                Manager.Instance.Game.Send($"mvm_actor_model {Parent.Name} {value} {Head}");
                RaisePropertyChanged(nameof(Body));
            }
        }

        public string Head
        {
            get => _Head;
            set
            {
                _Head = value;
                Manager.Instance.Game.Send($"mvm_actor_model {Parent.Name} {Body} {value}");
                RaisePropertyChanged(nameof(Head));
            }
        }

        public void Set( string body, string head )
        {
            _Body = body;
            _Head = head;
            Manager.Instance.Game.Send($"mvm_actor_model {Parent.Name} {body} {head}");
            RaisePropertyChanged(nameof(Body));
            RaisePropertyChanged(nameof(Head));
        }
    }
}
