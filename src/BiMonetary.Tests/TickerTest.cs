using NetCoreKit.Samples.BiMonetaryApi.v1.Controllers;
using Xunit;

namespace NetCoreKit.Samples.Tests
{
    public class TickerTest
    {
        [Fact]
        public void CanInstanceTicker()
        {
            var ticker = new Ticker();
            Assert.NotNull(ticker);
        }

        [Fact]
        public void CanAddEventCreatedToTicker()
        {
            var ticker = new Ticker();
            var @events = ticker.GetUncommittedEvents();
            Assert.True(@events.Count >= 1);
        }
    }
}
