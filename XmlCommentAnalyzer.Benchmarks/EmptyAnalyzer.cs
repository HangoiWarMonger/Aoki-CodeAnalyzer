// ReSharper properties

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace XmlCommentAnalyzer.Benchmarks;

/// <summary>
/// Пустой анализатор для бенчмарков.
/// </summary>
public class EmptyAnalyzer : DiagnosticAnalyzer
{
    /// <summary>
    /// Пустое правило.
    /// </summary>
    private static readonly DiagnosticDescriptor Rule = new(
        "EMPTY0001",
        "Empty",
        "Empty",
        "Test",
        DiagnosticSeverity.Info,
        isEnabledByDefault: true
    );

    /// <inheritdoc />
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

    /// <inheritdoc />
    public override void Initialize(AnalysisContext context)
    {
        // Не регистрируем никаких анализов
    }
}