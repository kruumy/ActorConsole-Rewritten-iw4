namespace ActorConsole.Core.Actor
{
    public class Models
    {
        internal string ActorName { get; set; }
        private string _Head;
        public string Head
        {
            get
            {
                return _Head;
            }
            set
            {
                _Head = value;
                Refresh();
            }
        }
        private string _Body;
        public string Body
        {
            get
            {
                return _Body;
            }
            set
            {
                _Body = value;
                Refresh();
            }
        }

        public Models(string head = "default_actor", string body = "default_actor")
        {
            Head = head;
            Body = body;
        }
        private void Refresh()
        {
            Memory.IW4.SendDvar($"mvm_actor_models {ActorName} {Body} {Head}");
        }
    }
}
