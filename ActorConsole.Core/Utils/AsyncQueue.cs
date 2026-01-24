using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ActorConsole.Core.Utils
{
    public class AsyncQueue
    {
        private readonly Queue<Func<Task>> _queue = new Queue<Func<Task>>();
        private bool _isProcessing = false;

        public void Enqueue( Func<Task> action )
        {
            if ( action == null ) throw new ArgumentNullException(nameof(action));

            _queue.Enqueue(action);

            if ( !_isProcessing )
            {
                _ = ProcessQueueAsync();
            }
        }

        private async Task ProcessQueueAsync()
        {
            _isProcessing = true;

            while ( _queue.Count > 0 )
            {
                var next = _queue.Dequeue();
                await next();
            }

            _isProcessing = false;
        }
    }
}
