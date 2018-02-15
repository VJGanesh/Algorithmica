namespace ObserverPattern
{
    public interface ISubject
    {
        void Notify(ISubject sender);
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        string Data { get; }
    }
}