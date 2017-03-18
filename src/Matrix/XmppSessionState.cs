using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Matrix
{
    public class XmppSessionState
    {
        private readonly BehaviorSubject<SessionState> valueSubject;

        public SessionState Value
        {
            get { return valueSubject.Value; }
            set { valueSubject.OnNext(value); }
        }

        public IObservable<SessionState> ValueChanged => valueSubject.DistinctUntilChanged();

        public XmppSessionState()
        {
            valueSubject = new BehaviorSubject<SessionState>(SessionState.Disconnected);
        }
    }
}
