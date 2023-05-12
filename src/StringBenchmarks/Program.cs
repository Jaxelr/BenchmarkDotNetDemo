using BenchmarkDotNet.Running;
using StringBenchmarks;

//_ = BenchmarkSwitcher.FromAssembly(typeof(StringVsStringBuilderReplace).Assembly)
//    .Run(args, new Config());

var config = new Config();

_ = BenchmarkRunner.Run<StringVsStringBuilderReplace>(config);
_ = BenchmarkRunner.Run<StringVsStringBuilderConcatenation>(config);