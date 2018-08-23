using System;
using System.Collections.Generic;
using System.Linq;
using VEdit.Core;

namespace VEdit.CSharp
{
    [Serializable]
    public class GenericParameter : Parameter, IParameterObserver
    {
        private readonly Type _genericTypeDefinition;
        private readonly Parameter[] _arguments;

        public IReadOnlyList<Parameter> Arguments => _arguments;

        private Type _type;
        public override Type Type => _type;

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
                param.AddObserver(this);
            }
        }

        public void Update(Parameter param)
        {
            var arguments = _arguments.Select(p => p.Type).ToArray();
            _type = _genericTypeDefinition.MakeGenericType(arguments);

            // TODO: Check if this can create a loop
            NotifyObservers();
        }

        public override bool TryChangeType(Type newType) => throw new NotImplementedException();
    }
}
