using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Linq;
using Targets;

namespace UnitTests
{
    [TestClass]
    public class SystemCollectionGenericExtensionsTests
    {
        [TestMethod]
        public void ToConvertedList_CalledWithValidInputAndProjection_ShouldReturnSameResultsAsBuiltIn()
        {
            // Arrange
            var input = new[] { 1, 2, 3 };
            Converter<int, int> conversion = i => i * i;
            var expectedResult = input.Select(i => conversion(i)).ToList();

            // Act
            var output = input.ToConvertedList(conversion);

            // Assert
            CollectionAssert.AreEqual(expectedResult, output.ToList());
        }
    }
}
