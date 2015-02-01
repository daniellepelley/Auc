using NUnit.Framework;

namespace Auctions.Import.HAndH.Test
{
    public class MakeParserTests
    {
        [TestCase("1960 Rolls-Royce Silver Cloud II", "Rolls-Royce")]
        [TestCase("1985 Land Rover 90 2.5 Diesel", "Land Rover")]
        public void ParseYear(string input, string expected)
        {
            var sut = new MakeParser();
            var actual = sut.Parse(input);
            Assert.AreEqual(expected, actual);
        }
    }
}