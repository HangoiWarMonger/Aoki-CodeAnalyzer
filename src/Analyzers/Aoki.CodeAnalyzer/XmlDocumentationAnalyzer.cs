using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Aoki.CodeAnalyzer;

/// <summary>
/// Анализатор отсутствия XML комментарев.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class XmlDocumentationAnalyzer : DiagnosticAnalyzer
{
    /// <summary>
    /// Описание правила диагностики для XML комантариев.
    /// </summary>
    private static readonly DiagnosticDescriptor NoXmlRule = new(
        "AO0001",
        Resources.AO0001Title,
        Resources.AO0001MessageFormat,
        "Documentation",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: Resources.AO0001Description);

    /// <inheritdoc />
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(NoXmlRule);

    /// <inheritdoc />
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSymbolAction(AnalyzeSymbol,
            SymbolKind.NamedType,
            SymbolKind.Method,
            SymbolKind.Property,
            SymbolKind.Field);
    }

    private static void AnalyzeSymbol(SymbolAnalysisContext context)
    {
        var symbol = context.Symbol;

        // Пропускаем приватные символы
        if (symbol.DeclaredAccessibility is not (Accessibility.Public or Accessibility.Protected))
            return;

        // Пропускаем аксессоры (get, set, init)
        if (symbol is IMethodSymbol {MethodKind: MethodKind.PropertyGet or MethodKind.PropertySet})
            return;

        // Пропускаем, если есть XML-документация
        if (!string.IsNullOrWhiteSpace(symbol.GetDocumentationCommentXml()))
            return;

        // Генерируем диагностику
        var diagnostic = Diagnostic.Create(NoXmlRule, symbol.Locations[0]);
        context.ReportDiagnostic(diagnostic);
    }
}