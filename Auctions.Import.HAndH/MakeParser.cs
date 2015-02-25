using System.Collections.Generic;
using System.Linq;

namespace Auctions.Import.HAndH
{
    public class MakeParser
    {
        private readonly Dictionary<string, string> _models;

        public MakeParser()
            : this(new[]
            {
                "Rolls-Royce",
                "Land Rover",
                "Daimler",
                "Mercedes-Benz",
                "Alvis",
                "Morgan",
                "MG",
                "Nash",
                "Triumph",
                "Lotus",
                "Morris",
                "Austin"
            })
        {}
        
        public MakeParser(IEnumerable<string> models)
        {
            _models = models.ToDictionary(x => x, x=> x);
        }

        public string Parse(string description)
        {
            foreach (var key in _models.Keys.Where(description.Contains))
            {
                return _models[key];
            }
            return string.Empty;
        }
    }
}