using System;

namespace Main
{
    public class BenchmarkTarget
    {
        public Action Action { get; private set; }
        public string Label { get; private set; }

        public BenchmarkTarget(Action action, string label)
        {
            Action = action;
            Label = label;
        }
    }
}
