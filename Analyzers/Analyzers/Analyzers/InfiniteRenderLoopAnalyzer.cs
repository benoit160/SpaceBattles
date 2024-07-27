using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class InfiniteRenderLoopAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "BA0001";
    public const string Title = "Stackalloc is forbidden";
    public const string MessageFormat = "Stackalloc is forbidden";
    public const string Description = "Stackalloc is forbidden";

    public const string Category = "Forbidden code";
    
    private static readonly DiagnosticDescriptor Rule = new(DiagnosticId, Title, MessageFormat, Category,
        DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
        = ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);

        context.EnableConcurrentExecution();
        
        context.RegisterSyntaxNodeAction(AnalyzeSyntax, SyntaxKind.StackAllocKeyword);
        // context.RegisterSyntaxNodeAction(AnalyzeSyntax, SyntaxKind.ClassDeclaration);
    }

    private void AnalyzeSyntax(SyntaxNodeAnalysisContext context)
    {
        var diagnostic = Diagnostic.Create(Rule,
            // The highlighted area in the analyzed source code. Keep it as specific as possible.
            context.Node.GetLocation(),
            // The value is passed to 'MessageFormat' argument of your 'Rule'.
            context.Node.ToString());
        
        context.ReportDiagnostic(diagnostic);
    }
}