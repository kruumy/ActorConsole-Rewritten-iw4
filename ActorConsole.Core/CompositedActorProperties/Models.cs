namespace ActorConsole.Core.CompositedActorProperties
{
    public class Models : CompositedActorProperty
    {
        public const string BODY_DEFAULT = "defaultactor";
        public const string HEAD_DEFAULT = "defaultactor";
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
                Set(Body, Head);
            }
        }

        public string Head
        {
            get => _Head;
            set
            {
                _Head = value;
                Set(Body, Head);
            }
        }

        public void Set( string body, string head )
        {
            if ( string.IsNullOrEmpty(body.Trim()) )
            {
                body = "tag_origin";
            }
            if ( string.IsNullOrEmpty(head.Trim()) )
            {
                head = "tag_origin";
            }
            _Body = body;
            _Head = head;
            Manager.Instance.Game.Send($"mvm_actor_model {Parent.Name} {body} {head}");
            RaisePropertyChanged(nameof(Body));
            RaisePropertyChanged(nameof(Head));
        }
    }
}
