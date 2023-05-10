using BenchmarkDotNet.Running;
using StringBenchmarks;

_ = BenchmarkSwitcher.FromAssembly(typeof(StringVsStringBuilderReplace).Assembly)
    .Run(args);
//_ = BenchmarkRunner.Run<StringVsStringBuilderReplace>();
//_ = BenchmarkRunner.Run<StringVsStringBuilderConcatenation>();