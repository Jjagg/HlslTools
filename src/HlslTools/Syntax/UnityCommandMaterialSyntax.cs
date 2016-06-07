using System.Collections.Generic;

namespace HlslTools.Syntax
{
    public sealed class UnityCommandMaterialSyntax : UnityCommandSyntax
    {
        public readonly SyntaxToken MaterialKeyword;
        public readonly SyntaxToken OpenBraceToken;
        public readonly List<UnityCommandSyntax> Commands;
        public readonly SyntaxToken CloseBraceToken;

        public UnityCommandMaterialSyntax(SyntaxToken materialKeyword, SyntaxToken openBraceToken, List<UnityCommandSyntax> commands, SyntaxToken closeBraceToken)
            : base (SyntaxKind.UnityCommandMaterial)
        {
            RegisterChildNode(out MaterialKeyword, materialKeyword);
            RegisterChildNode(out OpenBraceToken, openBraceToken);
            RegisterChildNodes(out Commands, commands);
            RegisterChildNode(out CloseBraceToken, closeBraceToken);
        }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitUnityCommandMaterial(this);
        }

        public override T Accept<T>(SyntaxVisitor<T> visitor)
        {
            return visitor.VisitUnityCommandMaterial(this);
        }
    }
}