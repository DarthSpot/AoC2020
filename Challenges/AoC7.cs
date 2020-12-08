using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AoCCore;

namespace Challenges
{
    public class AoC7 : AoCTool
    {
        public AoC7() : base(7)
        {
        }

        public override string CalculateSimple()
        {
            var input = GetInput();
            var regex = new Regex("(?<root>[a-z ]+) bags contain ((\\s?(?<cnt>\\d+) (?<col>[a-z ]+) bags?[,\\. ]?)+|(no other bags))");
            var map = regex.Matches(input)
                .OrderBy(x => x.Groups["root"].Value)
                .ToDictionary(x => x.Groups["root"].Value,
                    x => x.Groups["cnt"].Captures
                        .Zip(x.Groups["col"].Captures, (a, b) => (int.Parse(a.Value), b.Value)).ToList());
            var result = 0;
            foreach (var l in map.Keys)
            {
                if (CanContain(map, l, "shiny gold"))
                    result += 1;
            }

            return result+"";
        }

        private bool CanContain(Dictionary<string, List<(int count, string color)>> map, string leaf, string search)
        {
            var entry = map[leaf];
            if (entry.Any(x => string.Equals(x.color, search)))
                return true;

            return entry.Any(x => CanContain(map, x.color, search));
        }


        public override string CalculateExtended()
        {
            var input = GetInput();
            var regex = new Regex("(?<root>[a-z ]+) bags contain ((\\s?(?<cnt>\\d+) (?<col>[a-z ]+) bags?[,\\. ]?)+|(no other bags))");
            var map = regex.Matches(input)
                .OrderBy(x => x.Groups["root"].Value)
                .ToDictionary(x => x.Groups["root"].Value,
                    x => x.Groups["cnt"].Captures
                        .Zip(x.Groups["col"].Captures, (a, b) => (int.Parse(a.Value), b.Value)).ToList());

            return CountDeep(map, "shiny gold")+"";
        }

        private int CountDeep(Dictionary<string, List<(int count, string color)>> map, string leaf)
        {
            var entry = map[leaf];
            if (!entry.Any())
                return 0;

            return entry.Select(x => CountDeep(map, x.color) * x.count + x.count).Sum();
        }
    }
}