using System.Collections.Generic;
using System.Linq;

namespace Auctions.Import.Infrastructure.Parsers
{
    public class MakeParser
    {
        private readonly Dictionary<string, string> _models;

        //TODO: Hardcoded makes
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
                "Austin",
                "Gravetti",
                "Cadillac",
                "Ford",
                "MGC"
            })
        {}
        
        public MakeParser(IEnumerable<string> models)
        {
            _models = models.ToDictionary(x => x, x=> x);
        }

        public string Parse(string description)
        {
            foreach (var key in _models.Keys.OrderByDescending(x => x.Length).Where(description.Contains))
            {
                return _models[key];
            }
            return string.Empty;
        }
    }
}