using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorConsole.Core.Utils
{
    public class BlockingCooldown
    {
        public BlockingCooldown( TimeSpan cooldown )
        {
            this.Cooldown = cooldown;
        }

        public BlockingCooldown( TimeSpan cooldown, params EventHandler[] action )
        {
            this.Cooldown = cooldown;
            foreach ( EventHandler act in action )
            {
                OnInvoke += act;
            }
        }
        public event EventHandler OnInvoke;
        public TimeSpan Cooldown { get; }
        public bool IsOnCooldown { get; private set; }

        public async void Invoke()
        {
            if ( !IsOnCooldown )
            {
                OnInvoke?.Invoke(this,EventArgs.Empty);
                IsOnCooldown = true;
                await Task.Delay( Cooldown );
                IsOnCooldown = false;
            }
        }
    }
    public class BlockingCooldown<T> // TODO implement
    {

    }
}
