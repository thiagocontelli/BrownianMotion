namespace BrownianMotion.Tests
{
    public class BrownianMotionTests
    {
        [Fact]
        public void Generate_ShouldReturnArrayWithCorrectLength()
        {
            int numDays = 10;
            var bm = new Models.BrownianMotion(0.1, 0.05, 100, numDays);

            var prices = bm.Generate();

            Assert.Equal(numDays, prices.Length);
        }

        [Fact]
        public void Generate_ShouldStartWithInitialPrice()
        {
            double initialPrice = 100;
            var bm = new Models.BrownianMotion(0.1, 0.05, initialPrice, 10);

            var prices = bm.Generate();

            Assert.Equal(initialPrice, prices[0], precision: 5);
        }

        [Fact]
        public void Generate_ShouldReturnPositivePrices()
        {
            var bm = new Models.BrownianMotion(0.2, 0.01, 100, 50);

            var prices = bm.Generate();

            Assert.All(prices, price => Assert.True(price > 0));
        }

        [Fact]
        public void Generate_ShouldVaryOverTime()
        {
            var bm = new Models.BrownianMotion(0.3, 0.01, 100, 20);

            var prices = bm.Generate();

            Assert.Contains(prices, price => Math.Abs(price - 100) > 0.0001);
        }
    }
}
