using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;

namespace Dotnetos.AsyncExpert.Homework.Module01.Benchmark
{
    [DisassemblyDiagnoser(exportCombinedDisassemblyReport: true)]
    [NativeMemoryProfiler]
    public class FibonacciCalc
    {
        private readonly IDictionary<ulong, ulong> memoized = new Dictionary<ulong, ulong>(36)
        {
            { 1, 1 },
            { 2, 1 }
        };

        // HOMEWORK:
        // 1. Write implementations for RecursiveWithMemoization and Iterative solutions
        // 2. Add memory profiler to the benchmark
        // 3. Run with release configuration and compare results
        // 4. Open disassembler report and compare machine code
        // 
        // You can use the discussion panel to compare your results with other students

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Data))]
        public ulong Recursive(ulong n)
        {
            if (n == 1 || n == 2) return 1;
            return Recursive(n - 2) + Recursive(n - 1);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong RecursiveWithMemoization(ulong n)
        {
            if (this.memoized.ContainsKey(n))
            {
                return memoized[n];
            }
            
            var result = RecursiveWithMemoization(n - 1) + RecursiveWithMemoization(n - 2);
            memoized[n] = result;
            return result;
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong Iterative(ulong n)
        {
            if (n == 1 || n == 2) return 1;

            ulong minus2 = 1;
            ulong minus1 = 1;
            ulong value = 0;

            for (ulong i = 3; i <= n; i++)
            {
                value = minus2 + minus1;
                minus2 = minus1;
                minus1 = value;
            }

            return value;
        }

        public IEnumerable<ulong> Data()
        {
            yield return 15;
            yield return 35;
        }
    }
}
