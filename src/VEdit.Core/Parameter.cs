using System;
using System.Collections.Generic;
using System.Linq;

namespace VEdit.Core
{
    public interface IParameterObserver
    {
        void Update(Parameter param);
    }

    public interface IParameterObservable
    {
        IReadOnlyList<IParameterObserver> Observers { get; }
        void AddObserver(IParameterObserver observer);
        void RemoveObserver(IParameterObserver observer);
        void NotifyObservers();
    }

    [Serializable]
    public abstract class Parameter : IParameterObservable
    {
        public abstract Type Type { get; }

        private readonly HashSet<IParameterObserver> _observers = new HashSet<IParameterObserver>();
        public IReadOnlyList<IParameterObserver> Observers => _observers.ToList();

        public void NotifyObservers()
        {
            foreach (IParameterObserver param in Observers)
            {
                param.Update(this);
            }
        }

        public void AddObserver(IParameterObserver observer) => _observers.Add(observer);
        public void RemoveObserver(IParameterObserver observer) => _observers.Remove(observer);

        public abstract bool TryChangeType(Type newType);
    }
}
