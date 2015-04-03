
namespace Main
{
    public class BenchmarkStats
    {
        public BenchmarkTarget Target { get; set; }
        public int Iterations { get; set; }
        public long TotalRuntimeMs { get; set; }
        public long MinRuntimeMs { get; set; }
        public long MaxRuntimeMs { get; set; }
        public double AvgRuntimeMs { get; set; }

        public override string ToString()
        {
            return string.Format("{0} time in ms: total={1} min={2} max={3} avg={4}",
                Target.Label, 
                TotalRuntimeMs, 
                MinRuntimeMs, 
                MaxRuntimeMs, 
                AvgRuntimeMs);
        }
    }
}
