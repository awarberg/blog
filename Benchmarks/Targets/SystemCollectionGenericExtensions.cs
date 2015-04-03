using System;
using System.Collections.Generic;

namespace Targets
{
    public static class SystemCollectionGenericExtensions
    {
        public static IReadOnlyList<TOut> ToConvertedList<TIn, TOut>(this IReadOnlyList<TIn> input, Converter<TIn, TOut> converter)
        {
            var output = new TOut[input.Count];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = converter(input[i]);
            }
            return output;
        }

        public static IReadOnlyList<TOut> ToConvertedListHybrid<TIn, TOut>(this IReadOnlyList<TIn> input, Converter<TIn, TOut> converter)
        {
            var inputArray = input as TIn[];
            if (inputArray != null)
            {
                return Array.ConvertAll(inputArray, converter);
            }
            var inputList = input as List<TIn>;
            if (inputList != null)
            {
                return inputList.ConvertAll(converter);
            }
            return ToConvertedList(input, converter);
        }
    }
}
