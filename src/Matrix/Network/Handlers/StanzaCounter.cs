using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Matrix.Network.Handlers
{   
    public class StanzaCounter
    {
        private readonly BehaviorSubject<int> valueSubject;

        public StanzaCounter()
        {
            valueSubject = new BehaviorSubject<int>(0);
        }

        public int Value
        {
            get { return valueSubject.Value; }
            set { valueSubject.OnNext(value); }
        }

        /// <summary>
        /// When the SessionState changed
        /// </summary>
        public IObservable<int> ValueChanged => valueSubject.DistinctUntilChanged();
    }
}
