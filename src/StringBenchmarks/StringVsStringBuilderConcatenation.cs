using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Text;

namespace StringBenchmarks;

[LongRunJob]
[AllStatisticsColumn]
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[BenchmarkCategory("Concatenation")]
public class StringVsStringBuilderConcatenation
{
    [Params(500, 50_000, 200_000, 1_000_000)]
    public int N;

    private const char input = 'X';
    private string resultA = string.Empty;
    private StringBuilder resultB = new();

    [IterationSetup]
    public void Setup()
    {
        resultA = string.Empty;
        resultB = new();
    }

    [Benchmark]
    public string StringConcatenation()
    {
        for (int i = 0; i < N; i++)
        {
            resultA += input;
        }

        return resultA;
    }

    [Benchmark]
    public string StringBuilderConcatenation()
    {
        for (int i = 0; i < N; i++)
        {
            resultB.Append(input);
        }

        return resultB.ToString();
    }
}