using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Order;

namespace StringBenchmarks;

internal class Config : ManualConfig
{
    public Config()
    {
        var job = new Job(Job.MediumRun);

        AddLogger(ConsoleLogger.Default);

        job.WithRuntime(CoreRuntime.Core70);
        job.WithPlatform(Platform.X64);
        job.WithJit(Jit.RyuJit);

        AddDiagnoser(MemoryDiagnoser.Default);

        AddExporter(MarkdownExporter.GitHub);

        AddColumn(TargetMethodColumn.Method);
        AddColumn(TargetMethodColumn.Type);
        AddColumn(CategoriesColumn.Default);
        AddColumn(StatisticColumn.AllStatistics);
        AddColumnProvider(DefaultColumnProviders.Params);
        AddColumnProvider(DefaultColumnProviders.Metrics);

        AddJob(job);

        Orderer = new DefaultOrderer(SummaryOrderPolicy.Method);
        Options |= ConfigOptions.JoinSummary;
    }
}