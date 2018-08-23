using System;
using System.Collections.Generic;
using System.Linq;
using VEdit.Core;
using VEdit.Core.Extensions;

namespace VEdit.CSharp
{
    [Flags]
    public enum TypeConstraints
    {
        None,
        New,
        Class
    }

    [Serializable]
    public class GenericArgument : Parameter
    {
        public TypeConstraints Constraints { get; }
        public Type BaseClass { get; private set; }

        private Type _type;
        public override Type Type => _type;

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

        public override bool TryChangeType(Type newType)
        {
            if (newType.IsGenericTypeDefinition)
                return false;

            bool hasClassConstraint = (Constraints & TypeConstraints.Class) == TypeConstraints.Class;
            if (hasClassConstraint && !newType.IsClass)
                return false;

            bool hasNewConstraint = (Constraints & TypeConstraints.New) == TypeConstraints.New;
            if (hasNewConstraint && !newType.HasDefaultConstructor())
                return false;

            _type = newType;

            NotifyObservers();
            return true;
        }
    }

}
