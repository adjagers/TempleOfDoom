using TempleOfDoom.HelperClasses;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer.Models.Items;

public class PressurePlate : IItem, IObservable<bool>
{
    public Position? Position { get; set; }
    private readonly List<IObserver<bool>> _observers = new List<IObserver<bool>>();
    
    public PressurePlate(Position position)
    {
        Position = position;
    }
    public void Interact(Player player)
    {
       foreach(var observer in _observers)
        {
            observer.OnNext(true);
        }
    }

    public IDisposable Subscribe(IObserver<bool> observer)
    {
        if(!_observers.Contains(observer)) _observers.Add(observer);
        return new Unsubscriber(_observers, observer);
    }

    // private class to avoid subscribing the same object (pressure plate) twice
    private class Unsubscriber : IDisposable
    {
        private readonly List<IObserver<bool>> _observers;
        private readonly IObserver<bool> _observer;
        public Unsubscriber(List<IObserver<bool>> observers, IObserver<bool> observer)
        {
            _observers = observers;
            _observer = observer;
        }
        public void Dispose()
        {
            if (_observers != null&&_observers.Contains(_observer)) _observers.Remove(_observer);
        }
    }
}