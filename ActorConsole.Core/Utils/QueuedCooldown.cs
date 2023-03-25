using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ActorConsole.Core.Utils
{
    public class QueuedCooldown
    {
        public QueuedCooldown( TimeSpan cooldown )
        {
            this.Cooldown = cooldown;
        }

        public event EventHandler OnInvoke;

        public TimeSpan Cooldown { get; }
        public int Overflow { get; private set; } = 0;

        public async void Invoke()
        {
            Overflow++;
            if ( Overflow == 1 )
            {
                while ( Overflow > 0 )
                {
                    OnInvoke?.Invoke(this, EventArgs.Empty);
                    await Task.Delay(Cooldown);
                    Overflow--;
                }
            }
        }
    }

    public class QueuedCooldown<T>
    {
        public QueuedCooldown( TimeSpan cooldown )
        {
            this.Cooldown = cooldown;
        }

        public event EventHandler<T> OnInvoke;

        public TimeSpan Cooldown { get; }
        public Queue<T> Overflow { get; private set; } = new Queue<T>();

        public async void Invoke( T arg )
        {
            Overflow.Enqueue(arg);
            if ( Overflow.Count == 1 )
            {
                while ( Overflow.Count > 0 )
                {
                    OnInvoke?.Invoke(this, Overflow.Peek());
                    await Task.Delay(Cooldown);
                    Overflow.Dequeue();
                }
            }
        }
    }
}
