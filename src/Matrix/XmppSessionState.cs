using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Matrix
{   
    public class XmppSessionState
    {
        private readonly BehaviorSubject<SessionState> valueSubject;

        public XmppSessionState()
        {
            valueSubject = new BehaviorSubject<SessionState>(SessionState.Disconnected);
        }        

        public SessionState Value
        {
            get { return valueSubject.Value; }
            set { valueSubject.OnNext(value); }
        }

        /// <summary>
        /// When the SessionState changed
        /// </summary>
        public IObservable<SessionState> ValueChanged => valueSubject.DistinctUntilChanged();       
    }
}
