using System;
using System.Collections.Generic;
using System.Linq;

namespace Main
{
    public static class Extensions
    {
        public static IEnumerable<BenchmarkStats> Benchmark(this IReadOnlyList<BenchmarkTarget> targets, int nrIterations)
        {
            var rand = new Random();
            var iterationElapsedMs = targets.ToDictionary(t => t, t => new long[nrIterations]);
            var sw = new System.Diagnostics.Stopwatch();
            for (int i = 0; i < nrIterations; i++)
            {
                foreach (var target in targets.OrderBy(t => rand.NextDouble()))
                {
                    var iterationStarted = sw.ElapsedMilliseconds;
                    sw.Start();
                    target.Action();
                    sw.Stop();
                    var iterationFinished = sw.ElapsedMilliseconds;
                    iterationElapsedMs[target][i] = iterationFinished - iterationStarted;
                }
            }

            return iterationElapsedMs.Select(kvp => new BenchmarkStats
            {
                Target = kvp.Key,
                Iterations = nrIterations,
                TotalRuntimeMs = kvp.Value.Sum(),
                MinRuntimeMs = kvp.Value.Min(),
                MaxRuntimeMs = kvp.Value.Max(),
                AvgRuntimeMs = kvp.Value.Average()
            });
        }

        public static void PrintSummaryByAvgRuntime(this IEnumerable<BenchmarkStats> stats)
        {
            var byAverageRuntime = stats.OrderBy(s => s.AvgRuntimeMs).ToList();
            var firstRuntime = byAverageRuntime.First().AvgRuntimeMs;
            foreach (var s in byAverageRuntime)
            {
                Console.WriteLine("{0} {1:p}", s, s.AvgRuntimeMs / firstRuntime);
            }
        }
    }
}
