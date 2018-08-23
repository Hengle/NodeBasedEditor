using Microsoft.CodeAnalysis;
using VEdit.Core;

namespace VEdit.CSharp
{
    public interface ISyntaxGenerator
    {
        //SyntaxNode Generate(Scope currentScope);
    }

    public abstract class CSharpNode : Node, ISyntaxGenerator
    {
        //public abstract SyntaxNode Generate(Scope currentScope);
    }
}
