using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Auctions.Import.Infrastructure.Parsers
{
    public class DateParser
    {
        public DateTime? Parse(string date, string format)
        {
            DateTime? dateTime = null;

            var replaces = new[] { "st", "nd", "rd", "th", "mon", "tue", "wed", "thu", "fri", "sat", "sun" };
            
            date = date.ToLower();

            foreach (var replace in replaces)
            {
                date = date.Replace(replace, string.Empty);
            }

            foreach (var word in SplitWord(date))
            {
                try
                {
                    dateTime = DateTime.ParseExact(word, format, new DateTimeFormatInfo());
                    dateTime = new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day);
                }
                catch
                {
                }
            }
            return dateTime;             
        }

        private string[] SplitWord(string dateString)
        {
            var output = new List<string>();

            var parts = dateString.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var stringBuilder = new StringBuilder();

            foreach (var part in parts)
            {
                stringBuilder.Append(part + " ");
                output.Add(stringBuilder.ToString().Trim());
            }

            return output.ToArray();
        }

    }
}
