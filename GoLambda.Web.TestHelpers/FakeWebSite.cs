using System.Collections.Generic;
using System.IO;

namespace GoLambda.Web.TestHelpers
{
    public class FakeWebSite
    {
        private readonly Dictionary<string, string> _dictionary;

        public FakeWebSite()
        {
            _dictionary = new Dictionary<string, string>();
        }

        public void AddFile(string url, string filePath)
        {
            _dictionary.Add(url, File.ReadAllText(filePath));
        }

        public void AddHtml(string url, string html)
        {
            _dictionary.Add(url, html);
        }

        public string Get(string url)
        {
            return _dictionary[url];
        }
    }
}
