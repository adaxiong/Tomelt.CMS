using Tomelt.Scripting.Compiler;

namespace Tomelt.Scripting.Ast {
    public interface IAstNodeWithToken {
        Token Token { get; }
    }
}