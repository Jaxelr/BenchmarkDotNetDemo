﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Security.Cryptography;

namespace HashBenchmarks;

[MediumRunJob]
//[LongRunJob(runtimeMoniker: RuntimeMoniker.Net70)] //ShortRunJob, DryJob, MediumRunJob, LongRunJob, VeryLongRunJob
//[SimpleJob(runtimeMoniker: RuntimeMoniker.Net70)]
//[MediumRunJob(runtimeMoniker: RuntimeMoniker.Net472)] //: Target as many platforms as you want
//[GcServer(true)] //Runs Server Garbage Collector, default behavior is false
//[LegacyJitX64Job, RyuJitX64Job] //use the jit compiler you prefer
[AllStatisticsColumn] //Other Columns: Min, Max, Mean, Median, StdDev, Op/s, Skewness, Kurtosis, Percentiles
[RankColumn]
[MemoryDiagnoser] //Other Diagnosers: ThreadingDiagnoser, ExceptionDiagnoser, HardwareCounters, TailCallDiagnoser, ETWProfiler
[MarkdownExporterAttribute.GitHub] //Other Exporters: AsciiDocExporter, CsvExporter, HtmlExporter, RPlotExporter, PlainExporter, JsonExporter, XmlExporter
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class Md5VsSha256
{
    [Params(1_000, 10_000, 100_000)]
    public int N;

    private readonly byte[] data;

    private readonly SHA256 sha256 = SHA256.Create();
    private readonly MD5 md5 = MD5.Create();

    public Md5VsSha256()
    {
        data = new byte[N];
        new Random(42).NextBytes(data);
    }

    [Benchmark] //Can also add: Baseline
    public byte[] Sha256() => sha256.ComputeHash(data);

    [Benchmark]
    public byte[] Md5() => md5.ComputeHash(data);
}