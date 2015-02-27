using Auctions.Import.Infrastructure.Parsers;
using NUnit.Framework;

namespace Auctions.Imports.Infrastructure.Test
{
    public class DateParserTests
    {
        [TestCase("22nd February 2015 12:00", "dd MMMM yyyy HH:mm", "22/02/2015")]
        [TestCase("Sat 10th January 2015 at 2:00pm", "d MMMM yyyy", "10/01/2015")]
        [TestCase("Mon 29th October 2014 at 1:00pm", "d MMMM yyyy", "29/10/2014")]
        [TestCase("Fri 9th May 2014 at 2:30pm", "d MMMM yyyy", "09/05/2014")]
        public void ParseYear(string input, string format, string expected)
        {
            var sut = new DateParser();

            var date = sut.Parse(input, format);

            string actual = string.Empty;

            if (date.HasValue)
            {
                actual = date.Value.ToString("dd/MM/yyyy");                
            }
            
            Assert.AreEqual(expected, actual);
        }
    }
}