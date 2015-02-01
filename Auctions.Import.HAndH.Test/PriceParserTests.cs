using Auctions.Import.Infrastructure.Parsers;
using NUnit.Framework;

namespace Auctions.Import.HAndH.Test
{
    public class PriceParserTests
    {
        [TestCase("£2,464", 2464)]
        [TestCase("£100", 100)]
        [TestCase("", null)]
        [TestCase("Text", null)]
        public void ParsePrice(string input, int? expected)
        {
            var sut = new PriceParser();
            var actual = sut.Parse(input);
            Assert.AreEqual(expected, actual);
        }
    }
}