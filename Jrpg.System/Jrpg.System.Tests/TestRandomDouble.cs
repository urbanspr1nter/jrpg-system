using System;
using Xunit;

namespace Jrpg.System.Tests
{
    public class TestRandomDouble
    {
        [Fact]
        public void TestGetDouble()
        {
            double d = RandomDouble.Get(0, 1);
            Assert.True(d <= 1.0);
            Assert.True(d >= 0.0);

            Console.WriteLine("Random double " + d);

            d = RandomDouble.Get(2.0, 2.6);
            Assert.True(d <= 2.6);
            Assert.True(d >= 2.0);

            Console.WriteLine("Random double " + d);
        }
    }
}
