using BenchmarkDotNet.Running;
using HashBenchmarks;

_ = BenchmarkRunner.Run<Md5VsSha256>();