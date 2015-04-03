using System;
using System.Linq;
using Targets;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            TestToProjectList();
            Console.ReadKey();
        }

        private static void TestToProjectList()
        {
            const int iterations = 1000;
            var dataAsList = Enumerable.Range(1, 100000).ToList();
            var dataAsArray = dataAsList.ToArray();
            Func<int, double> projection = i => Math.Pow(i, 2);
            Converter<int, double> conversion = i => Math.Pow(i, 2);

            Action selectToList = () =>
            {
                var result = dataAsArray.Select(projection).ToList();
            };

            Action listToConvertedList = () =>
            {
                var result = dataAsList.ToConvertedList(conversion);
            };

            Action arrayToConvertedList = () =>
            {
                var result = dataAsArray.ToConvertedList(conversion);
            };

            Action listToConvertedListHybrid = () =>
            {
                var result = dataAsList.ToConvertedListHybrid(conversion);
            };

            Action arrayToConvertedListHybrid = () =>
            {
                var result = dataAsArray.ToConvertedListHybrid(conversion);
            };

            Action listConvertAll = () =>
            {
                var result = dataAsList.ConvertAll(conversion);
            };

            Action arrayConvertAll = () =>
            {
                var result = Array.ConvertAll(dataAsArray, conversion);
            };

            BenchmarkTarget[] targets = 
            { 
               new BenchmarkTarget(selectToList, "SelectToList"), 
               new BenchmarkTarget(listToConvertedList ,"ListToConvertedList"), 
               new BenchmarkTarget(arrayToConvertedList, "ArrayToConvertedList"), 
               new BenchmarkTarget(listToConvertedListHybrid ,"ListToConvertedListHybrid"), 
               new BenchmarkTarget(arrayToConvertedListHybrid, "ArrayToConvertedListHybrid"), 
               new BenchmarkTarget(listConvertAll, "ListConvertAll"), 
               new BenchmarkTarget(arrayConvertAll, "ArrayConvertAll") 
            };

            var stats = targets.Benchmark(iterations);

            stats.PrintSummaryByAvgRuntime();
        }
    }
}
