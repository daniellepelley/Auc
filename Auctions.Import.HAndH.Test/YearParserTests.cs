using Auctions.Import.Infrastructure.Parsers;
using NUnit.Framework;

namespace Auctions.Import.HAndH.Test
{
    public class YearParserTests
    {
        [TestCase("1960 Range Rover", 1960)]
        [TestCase("1985 BMW", 1985)]
        [TestCase("", null)]
        [TestCase("Text", null)]
        [TestCase(" No Year", null)]
        public void ParseYear(string input, int? expected)
        {
            var sut = new YearParser();
            var actual = sut.Parse(input);
            Assert.AreEqual(expected, actual);
        }
    }
}