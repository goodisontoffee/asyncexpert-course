using FluentAssertions;
using Xunit;

namespace Dotnetos.AsyncExpert.Homework.Module01.Benchmark.Tests
{
    public class FibonacciCalcTests
    {
        [Theory]
        [InlineData(15)]
        [InlineData(35)]
        public void AllThreeStrategiesShouldAgree(ulong n)
        {
            // Arrange
            var sut = new FibonacciCalc();
            
            // Act
            var recursiveResult = sut.Recursive(n);
            var recursiveWithMemoizationResult = sut.RecursiveWithMemoization(n);
            var iterativeResult = sut.Iterative(n);
            
            // Assert
            recursiveResult.Should().Be(recursiveWithMemoizationResult);
            recursiveResult.Should().Be(iterativeResult);
        }
    }
}