using BenchmarkDotNet.Running;
using HashBenchmarks;

_ = BenchmarkRunner.Run<Md5VsSha256>();
//BenchmarkSwitcher.FromAssemblies(new[] { typeof(Md5VsSha256).Assembly }).Run(args);