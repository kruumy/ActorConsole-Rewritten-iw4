namespace ActorConsole.Core.Actor
{
    public class Models
    {
        internal string ActorName { get; set; }

        public const string Head_Default = "defaultactor";
        public const string Body_Default = "defaultactor";

        private string _Head = Head_Default;
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
        private string _Body = Body_Default;
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
        public void Refresh()
        {
            Memory.IW4.SendDvar($"mvm_actor_models {ActorName} {Body} {Head}");
        }       
    }
}
