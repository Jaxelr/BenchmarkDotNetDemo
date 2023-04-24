using BenchmarkDotNet.Attributes;
using System.Security.Cryptography;

[ShortRunJob]
[AllStatisticsColumn]
[MemoryDiagnoser]
public class Md5VsSha256
{
    [Params(1000, 10000)]
    public int N;
    private readonly byte[] data;

    private readonly SHA256 sha256 = SHA256.Create();
    private readonly MD5 md5 = MD5.Create();

    public Md5VsSha256()
    {
        data = new byte[N];
        new Random(42).NextBytes(data);
    }

    [Benchmark]
    public byte[] Sha256() => sha256.ComputeHash(data);

    [Benchmark]
    public byte[] Md5() => md5.ComputeHash(data);
}
