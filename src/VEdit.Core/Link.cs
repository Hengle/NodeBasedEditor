using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEdit.Core
{
    public class Link
    {
        public Socket Input { get; }
        public Socket Output { get; }

        public Link(Socket input, Socket output)
        {
            if (input is null)
                throw new ArgumentNullException($"{nameof(input)} is null.");

            if (output is null)
                throw new ArgumentNullException($"{nameof(output)} is null.");

            if (ReferenceEquals(input, output))
                throw new ArgumentException($"{nameof(input)} is the same as {nameof(output)}.");

            Input = input;
            Output = output;
        }
    }
}
