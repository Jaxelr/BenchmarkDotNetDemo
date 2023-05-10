using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Text;
using System.Text.RegularExpressions;

namespace StringBenchmarks;

[LongRunJob]
[AllStatisticsColumn]
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[BenchmarkCategory("Replace")]
public class StringVsStringBuilderReplace
{
    [Benchmark]
    [ArgumentsSource(nameof(Arrays))]
    public string ReplaceString(string value) => value.Replace("*", string.Empty);

    [Benchmark]
    [ArgumentsSource(nameof(Arrays))]
    public string ReplaceStringBuilder(string value)
    {
        var builder = new StringBuilder(value);

        return builder.Replace("*", string.Empty).ToString();
    }

    [Benchmark]
    [ArgumentsSource(nameof(Arrays))]
    public string ReplaceRegexBuilder(string value) => Regex.Replace(value, ".*", string.Empty);

    public static IEnumerable<object[]> Arrays()
    {
        char input = '*';
        yield return new object[] { new string(input, 500) };
        yield return new object[] { new string(input, 50_000) };
        yield return new object[] { new string(input, 200_000) };
        yield return new object[] { new string(input, 1_000_000) };
    }
}