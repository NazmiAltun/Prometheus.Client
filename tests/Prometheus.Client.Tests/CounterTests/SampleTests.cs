using System;
using Xunit;

namespace Prometheus.Client.Tests.CounterTests
{
    public class SampleTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3.1)]
        public void CanIncrement(double inc)
        {
            var counter = CreateCounter();
            counter.Inc(inc);

            Assert.Equal(inc, counter.Value);
        }

        [Fact]
        public void ShouldIgnoreNaN()
        {
            var counter = CreateCounter();
            counter.Inc(42);

            counter.Inc(double.NaN);

            Assert.Equal(42, counter.Value);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-3.1)]
        public void CannotDecrement(double inc)
        {
            var counter = CreateCounter();
            Assert.Throws<ArgumentOutOfRangeException>(() => counter.Inc(inc));
        }

        [Fact]
        public void DefaultIncrement()
        {
            var counter = CreateCounter();
            counter.Inc();

            Assert.Equal(1, counter.Value);
        }

        private Counter CreateCounter()
        {
            var config = new MetricConfiguration("test", string.Empty, Array.Empty<string>(), false);
            return new Counter(config, Array.Empty<string>());
        }
    }
}
