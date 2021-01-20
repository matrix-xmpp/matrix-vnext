namespace Matrix
{
    using System;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;

    public abstract class DistinctBehaviorSubject<T>
    {
        protected DistinctBehaviorSubject(T init)
        {
            Subject = new BehaviorSubject<T>(init);
        }

        public T Value
        {
            get => Subject.Value;
            set => Subject.OnNext(value);
        }

        public BehaviorSubject<T> Subject { get; private set; }

        /// <summary>
        /// When the Value changed
        /// </summary>
        public IObservable<T> ValueChanged => Subject.DistinctUntilChanged();
    }
}
