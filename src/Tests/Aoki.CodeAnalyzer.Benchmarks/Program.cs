using BenchmarkDotNet.Running;
using Aoki.CodeAnalyzer.Benchmarks;

Console.WriteLine("XML Documentation Analyzer Benchmark");
Console.WriteLine("=====================================");

BenchmarkRunner.Run<XmlDocumentationAnalyzerBenchmark>();
