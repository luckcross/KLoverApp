using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KLoversApp
{
    internal delegate void TimerCallback(object state);

    internal sealed class Timer : CancellationTokenSource, IDisposable
    {
        internal string Name { get; private set; }
        internal Timer(string name, TimerCallback callback, object state, int dueTimeMilliseconds, int period)
        {
            Name = name;

            Contract.Assert(period == -1, "This stub implementation only supports dueTime.");
            Task.Delay(dueTimeMilliseconds, Token).ContinueWith((t, s) =>
            {
                if (IsCancellationRequested)
                    return;

                var tuple = (Tuple<TimerCallback, object>)s;
                tuple.Item1(tuple.Item2);

            }, Tuple.Create(callback, state), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public new void Dispose()
        {
            base.Cancel();
        }
    }
}
