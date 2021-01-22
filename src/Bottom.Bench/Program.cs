using System;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace Bottom.Bench
{

    [WarmupCount(5)]
    [IterationCount(10)]
    public class BottomBenchmark
    {

        [Params(1, 2, 3, 4)]
        public int N;

        private readonly string _encode_data;
        private readonly string _decode_data;

        public BottomBenchmark()
        {
            _encode_data = BenchmarkData.ENCODE_INPUT[N];
            _decode_data = BenchmarkData.DECODE_INPUT[N];

            Bottomify.encode_byte(0); // preload cache
        }

        [Benchmark]
        public string Encode() => Bottomify.encode_string(_encode_data);

        [Benchmark]
        public string Decode() => Bottomify.decode_string(_decode_data);

    }

    class Program
    {
        static void Main(string[] args)
        {
            for (int N = 0; N < 5; N++)
            {

                float encode_size = (float)Encoding.UTF8.GetByteCount(BenchmarkData.ENCODE_INPUT[N]) / 1_000f;
                float decode_size = (float)Encoding.UTF8.GetByteCount(BenchmarkData.DECODE_INPUT[N]) / 1_000f;

                Console.WriteLine($"{N} ENCODE: {encode_size:F} KB, DECODE {decode_size:F} KB");
            }

            Summary summary = BenchmarkRunner.Run<BottomBenchmark>();
        }
    }
}
