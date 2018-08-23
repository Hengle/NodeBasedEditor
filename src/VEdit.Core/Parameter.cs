using System;
using System.Collections.Generic;
using System.Linq;
using VEdit.Core.Extensions;

namespace VEdit.Core
{
    [Flags]
    public enum TypeConstraints
    {
        None,
        New,
        Class
    }

    internal interface IParameterObserver
    {
        void Update(Parameter param);
    }

    [Serializable]
    public abstract class Parameter
    {
        internal List<IParameterObserver> Observers { get; } = new List<IParameterObserver>();
        public abstract Type Type { get; internal set; }

        protected virtual void NotifyObservers()
        {
            foreach (IParameterObserver param in Observers)
            {
                param.Update(this);
            }
        }
    }

    [Serializable]
    public class GenericArgument : Parameter
    {
        public TypeConstraints Constraints { get; }
        public Type BaseClass { get; private set; }

        public override Type Type { get; internal set; }

        private readonly HashSet<Type> _interfaces = new HashSet<Type>();
        public IReadOnlyList<Type> Interfaces => _interfaces.ToList();

        public GenericArgument(TypeConstraints constraints)
        {
            Constraints = constraints;
        }

        public void SetBaseClass(Type type)
        {
            if (type.IsGenericTypeDefinition)
                throw new InvalidOperationException($"{nameof(type)} is not a constructed type.");

            if (!type.CanBeDerived())
                throw new InvalidOperationException($"{nameof(type)} is not a valid class.");

            BaseClass = type;
        }

        public void AddInterface(Type type)
        {
            if (!type.IsInterface)
                throw new InvalidOperationException($"{nameof(type)} is not an interface.");

            _interfaces.Add(type);
        }

        public bool TryChangeType(Type newType)
        {
            if (newType.IsGenericTypeDefinition)
                return false;

            bool hasClassConstraint = (Constraints & TypeConstraints.Class) == TypeConstraints.Class;
            if (hasClassConstraint && !newType.IsClass)
                return false;

            bool hasNewConstraint = (Constraints & TypeConstraints.New) == TypeConstraints.New;
            if (hasNewConstraint && !newType.HasDefaultConstructor())
                return false;

            Type = newType;

            NotifyObservers();
            return true;
        }
    }

    [Serializable]
    public class GenericParameter : Parameter, IParameterObserver
    {
        private readonly Type _genericTypeDefinition;
        private readonly Parameter[] _arguments;

        public IReadOnlyList<Parameter> Arguments => _arguments;

        private Type _type;
        public override Type Type
        {
            get => _type;
            internal set => throw new InvalidOperationException($"Cannot set {nameof(Type)} property in {nameof(GenericParameter)}");
        }

        public GenericParameter(Type typeDefinition, params Parameter[] arguments)
        {
            if (!typeDefinition.IsGenericTypeDefinition)
                throw new ArgumentException($"{nameof(typeDefinition)} is not a generic type definition.");

            if (arguments.Length == 0)
                throw new ArgumentException($"{nameof(arguments)} count should be greater than 0.");

            ObserveArguments(arguments);

            _genericTypeDefinition = typeDefinition;
            _arguments = arguments;
        }

        private void ObserveArguments(Parameter[] arguments)
        {
            foreach (var param in arguments)
            {
                param.Observers.Add(this);
            }
        }

        public void Update(Parameter param)
        {
            var arguments = _arguments.Select(p => p.Type).ToArray();
            _type = _genericTypeDefinition.MakeGenericType(arguments);

            // TODO: Check if this can create a loop
            NotifyObservers();
        }
    }
}
