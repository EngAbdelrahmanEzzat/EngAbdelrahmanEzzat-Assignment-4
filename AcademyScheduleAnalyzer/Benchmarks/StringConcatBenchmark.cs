using System.Text;
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class StringConcatBenchmark
{
    [Params(100, 1000, 10000, 100000)]
    public int Iterations;

    [Benchmark]
    public string StringConcatenation()
    {
        string result = string.Empty;
        for (int i = 0; i < Iterations; i++)
        {
            result += "abc";
        }
        return result;
    }

    [Benchmark]
    public string StringBuilderConcatenation()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < Iterations; i++)
        {
            sb.Append("abc");
        }
        return sb.ToString();
    }
}
