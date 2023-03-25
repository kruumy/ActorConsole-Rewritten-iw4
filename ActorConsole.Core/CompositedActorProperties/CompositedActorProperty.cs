using System.ComponentModel;

namespace ActorConsole.Core.CompositedActorProperties
{
    public abstract class CompositedActorProperty : INotifyPropertyChanged
    {
        internal CompositedActorProperty( Actor Parent )
        {
            this.Parent = Parent;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected Actor Parent { get; }

        protected void RaisePropertyChanged( string propertyname )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
