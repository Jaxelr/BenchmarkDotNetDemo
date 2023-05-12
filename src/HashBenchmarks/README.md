# Hash Benchmarks results

By default this benchmark is running targeting just one runtime:

``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22621.1702)
11th Gen Intel Core i7-1185G7 3.00GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.203
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-OFLFKC : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2

Server=True  IterationCount=100  LaunchCount=3  
WarmupCount=15  

```
| Method |      N |     Mean |   Error |   StdDev |  StdErr |   Median |      Min |       Q1 |       Q3 |      Max |        Op/s |   Gen0 | Allocated |
|------- |------- |---------:|--------:|---------:|--------:|---------:|---------:|---------:|---------:|---------:|------------:|-------:|----------:|
| Sha256 |  10000 | 200.8 ns | 2.19 ns | 11.02 ns | 0.66 ns | 199.2 ns | 167.5 ns | 193.9 ns | 206.5 ns | 235.1 ns | 4,979,148.5 | 0.0021 |     112 B |
| Sha256 | 100000 | 220.6 ns | 8.06 ns | 39.51 ns | 2.42 ns | 212.6 ns | 170.0 ns | 191.4 ns | 230.2 ns | 377.7 ns | 4,532,857.0 | 0.0019 |     112 B |
| Sha256 |   1000 | 224.3 ns | 7.77 ns | 40.07 ns | 2.34 ns | 214.2 ns | 165.5 ns | 198.5 ns | 240.3 ns | 378.6 ns | 4,457,412.8 | 0.0019 |     112 B |
|    Md5 |  10000 | 260.2 ns | 3.55 ns | 17.79 ns | 1.07 ns | 255.4 ns | 233.0 ns | 248.3 ns | 264.6 ns | 332.5 ns | 3,843,907.0 | 0.0014 |      80 B |
|    Md5 | 100000 | 281.3 ns | 5.93 ns | 30.36 ns | 1.78 ns | 267.9 ns | 241.0 ns | 255.2 ns | 310.4 ns | 371.4 ns | 3,554,468.8 | 0.0014 |      80 B |
|    Md5 |   1000 | 308.1 ns | 6.99 ns | 34.79 ns | 2.10 ns | 307.8 ns | 249.8 ns | 276.4 ns | 327.1 ns | 421.2 ns | 3,245,948.8 | 0.0014 |      80 B |


..but it can executed targeting multiple runtimes by using the `--runtimes` argument. 

`dotnet run --configuration Release --framework net7.0 --runtimes net481 net7.0 --filter * --join` but we must swap the program.cs file to use the `BenchmarkSwitcher` instead of the `BenchmarkRunner`.

``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 11 (10.0.22621.1702/22H2/2022Update/SunValley2)
11th Gen Intel Core i7-1185G7 3.00GHz, 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.8.1 (4.8.9139.0), X64 RyuJIT VectorSize=256
  Job-BQVMTG : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  Job-PWJBPI : .NET Framework 4.8.1 (4.8.9139.0), X64 RyuJIT VectorSize=256


```
| Method |              Runtime |      N |       Mean |    Error |    StdDev |   StdErr |     Median |        Min |         Q1 |         Q3 |        Max |        Op/s | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------- |--------------------- |------- |-----------:|---------:|----------:|---------:|-----------:|-----------:|-----------:|-----------:|-----------:|------------:|------:|--------:|-------:|----------:|------------:|
| Sha256 |             .NET 7.0 |   1000 |   299.5 ns |  7.23 ns |  21.32 ns |  2.13 ns |   298.8 ns |   258.3 ns |   283.2 ns |   312.0 ns |   355.1 ns | 3,339,288.7 |  0.17 |    0.01 | 0.0176 |     112 B |        0.56 |
| Sha256 | .NET Framework 4.8.1 |   1000 | 1,784.9 ns | 33.26 ns |  68.69 ns |  9.53 ns | 1,793.8 ns | 1,655.8 ns | 1,731.0 ns | 1,822.8 ns | 1,947.0 ns |   560,244.0 |  1.00 |    0.00 | 0.0305 |     201 B |        1.00 |
|        |                      |        |            |          |           |          |            |            |            |            |            |             |       |         |        |           |             |
|    Md5 |             .NET 7.0 |   1000 |   398.0 ns |  8.57 ns |  25.26 ns |  2.53 ns |   394.9 ns |   353.4 ns |   378.7 ns |   417.5 ns |   458.0 ns | 2,512,612.5 |  0.20 |    0.02 | 0.0124 |      80 B |        0.71 |
|    Md5 | .NET Framework 4.8.1 |   1000 | 2,010.4 ns | 53.76 ns | 157.68 ns | 15.85 ns | 2,001.3 ns | 1,675.9 ns | 1,887.9 ns | 2,099.6 ns | 2,372.4 ns |   497,419.1 |  1.00 |    0.00 | 0.0153 |     112 B |        1.00 |
|        |                      |        |            |          |           |          |            |            |            |            |            |             |       |         |        |           |             |
| Sha256 |             .NET 7.0 |  10000 |   281.0 ns |  6.81 ns |  19.54 ns |  2.00 ns |   280.2 ns |   246.9 ns |   266.8 ns |   291.7 ns |   335.5 ns | 3,558,415.1 |  0.17 |    0.02 | 0.0176 |     112 B |        0.56 |
| Sha256 | .NET Framework 4.8.1 |  10000 | 1,708.9 ns | 40.77 ns | 118.93 ns | 12.01 ns | 1,697.2 ns | 1,452.9 ns | 1,634.3 ns | 1,783.5 ns | 1,992.6 ns |   585,177.0 |  1.00 |    0.00 | 0.0305 |     201 B |        1.00 |
|        |                      |        |            |          |           |          |            |            |            |            |            |             |       |         |        |           |             |
|    Md5 |             .NET 7.0 |  10000 |   370.3 ns |  7.40 ns |  15.11 ns |  2.12 ns |   369.3 ns |   345.0 ns |   358.6 ns |   380.9 ns |   407.9 ns | 2,700,297.1 |  0.20 |    0.02 | 0.0124 |      80 B |        0.71 |
|    Md5 | .NET Framework 4.8.1 |  10000 | 1,927.7 ns | 57.97 ns | 170.92 ns | 17.09 ns | 1,877.5 ns | 1,671.2 ns | 1,787.8 ns | 2,034.6 ns | 2,379.3 ns |   518,762.9 |  1.00 |    0.00 | 0.0153 |     112 B |        1.00 |
|        |                      |        |            |          |           |          |            |            |            |            |            |             |       |         |        |           |             |
| Sha256 |             .NET 7.0 | 100000 |   292.7 ns |  9.67 ns |  28.35 ns |  2.85 ns |   289.9 ns |   249.1 ns |   270.4 ns |   307.0 ns |   361.3 ns | 3,417,028.1 |  0.16 |    0.02 | 0.0176 |     112 B |        0.56 |
| Sha256 | .NET Framework 4.8.1 | 100000 | 1,825.9 ns | 44.48 ns | 129.74 ns | 13.11 ns | 1,820.1 ns | 1,583.0 ns | 1,724.0 ns | 1,928.9 ns | 2,130.1 ns |   547,665.7 |  1.00 |    0.00 | 0.0305 |     201 B |        1.00 |
|        |                      |        |            |          |           |          |            |            |            |            |            |             |       |         |        |           |             |
|    Md5 |             .NET 7.0 | 100000 |   394.2 ns |  9.64 ns |  28.42 ns |  2.84 ns |   394.7 ns |   340.1 ns |   371.4 ns |   413.3 ns |   459.7 ns | 2,536,704.3 |  0.19 |    0.01 | 0.0124 |      80 B |        0.71 |
|    Md5 | .NET Framework 4.8.1 | 100000 | 2,118.2 ns | 43.71 ns | 126.81 ns | 12.88 ns | 2,109.9 ns | 1,858.3 ns | 2,043.0 ns | 2,191.4 ns | 2,394.4 ns |   472,089.8 |  1.00 |    0.00 | 0.0172 |     112 B |        1.00 |


why would md5 be slower than sha256? [see here]https://stackoverflow.com/questions/65447185/why-would-md5-be-slower-than-sha256-in-my-c-sharp-code