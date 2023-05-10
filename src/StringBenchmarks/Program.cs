using BenchmarkDotNet.Running;
using StringBenchmarks;

_ = BenchmarkRunner.Run<StringVsStringBuilderReplace>();