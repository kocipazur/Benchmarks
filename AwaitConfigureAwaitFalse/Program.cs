using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Threading.Tasks;

namespace AsyncConfigureAwaitFalse
{
    public class Program
    {
        [MemoryDiagnoser]
        public class AsyncOperations
        {
            [Benchmark]
            public async Task<object> WithoutConfigureAwaitFalse() => await ReturnObjectAsync();

            [Benchmark]
            public async Task<object> WithConfigureAwaitFalse() => await ReturnObjectAsync().ConfigureAwait(false);

            [Benchmark]
            public async Task<object> ReturnTaskInsteadOfAwaiting() => await ReturnObjectAsyncWithoutAwait();

            public async Task<object> ReturnObjectAsync() => await Task.Run(() => new object());

            public Task<object> ReturnObjectAsyncWithoutAwait() => Task.Run(() => new object());
        }
        static void Main(string[] args)
        {
            _ = BenchmarkRunner.Run<AsyncOperations>();
        }
    }
}
