﻿namespace ActorConsole.Core.Actor.Attributes.Movement
{
    public class Pathing
    {
        internal string ActorName { get; set; }
        public bool IsEnabled => NodeCount > 0;
        private int _Speed;
        public int Speed 
        { 
            get => _Speed; 
            set 
            {
                _Speed = value;
                Manager.RaiseActorPropertyChanged(this, ActorName); 
            } 
        }
        private int NextNode = 1;
        

        public int NodeCount => NextNode - 1;
        public int CreateNode()
        {
            if (NextNode <= 13)
            {
                Memory.IW4.SendDvar($"mvm_actor_path_save {ActorName} {NextNode}");
                int result = NextNode;
                NextNode++;
                return result;
            }
            else
                return -1;

        }
        public int DeleteLastNode()
        {
            Memory.IW4.SendDvar($"mvm_actor_path_del {ActorName} {NextNode}");
            int result = NextNode;
            NextNode--;
            return result;
        }
        public void Play()
        {
            Memory.IW4.SendDvar($"mvm_actor_path_walk {ActorName} {Speed}");
        }


    }
}