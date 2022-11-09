using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EasyNetQ;
using EasyNetQ.Benchmarks.MessageFactory;

BenchmarkRunner.Run<Benchmark>();

class MessageBodyExample
{
    public string? String { get; set; }
    public int Int { get; set; }
    public double Double { get; set; }
}

public class Benchmark
{
    private readonly static MessageBodyExample Body = new();
    private readonly static MessageProperties Properties = new();

    [Params(1000000)]
    public int N;

    [Benchmark(Baseline = true)]
    public IMessage NewOperatorTest() => new Message<MessageBodyExample>(Body, Properties);

    [Benchmark]
    public IMessage ExpressionMessageFactoryTest() => ExpressionMessageFactory.CreateInstance(typeof(MessageBodyExample), Body, Properties);

    [Benchmark]
    public IMessage ActivatorMessageFactoryTest() => ActivatorMessageFactory.CreateInstance(typeof(MessageBodyExample), Body, Properties);
}
