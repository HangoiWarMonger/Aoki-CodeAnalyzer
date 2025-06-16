using BenchmarkDotNet.Running;
using XmlCommentAnalyzer.Benchmarks;

Console.WriteLine("XML Documentation Analyzer Benchmark");
Console.WriteLine("=====================================");

BenchmarkRunner.Run<XmlDocumentationAnalyzerBenchmark>();
