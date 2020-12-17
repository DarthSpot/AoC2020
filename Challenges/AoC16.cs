using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AoCCore;

namespace Challenges
{
    public class AoC16 : AoCTool
    {
        public AoC16() : base(16)
        {
        }

        public override object CalculateSimple()
        {
            var input = GetInput().Split("\n\n");
            var regex = new Regex("([^:]+): (\\d+)-(\\d+) or (\\d+)-(\\d+)");
            var data = input[0].Split("\n").Select(x => regex.Match(x));
            var ticket = input[1].Split("\n")[1].Split(",");
            var tickets = input[2].Split("\n").Skip(1).Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Split(",").Select(int.Parse).ToArray()).ToList();
            var numRanges = new List<(int min, int max)>();
            foreach (var e in data)
            {
                numRanges.Add((int.Parse(e.Groups[2].Value), int.Parse(e.Groups[3].Value)));
                numRanges.Add((int.Parse(e.Groups[4].Value), int.Parse(e.Groups[5].Value)));
            }

            return tickets.SelectMany(x => x).Where(x => !numRanges.Any(n => n.min <= x && n.max >= x)).Sum();
        }

        public override object CalculateExtended()
        {
            var input = GetInput().Split("\n\n");
            var regex = new Regex("([^:]+): (\\d+)-(\\d+) or (\\d+)-(\\d+)");
            var data = input[0].Split("\n").Select(x => regex.Match(x));
            var ticket = input[1].Split("\n")[1].Split(",");
            var tickets = input[2].Split("\n").Skip(1).Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Split(",").Select(int.Parse).ToArray()).ToList();
            var ticketData = new List<(string name, int minA, int maxA, int minB, int maxB)>();
            foreach (var e in data)
            {
                ticketData.Add((e.Groups[1].Value, int.Parse(e.Groups[2].Value), int.Parse(e.Groups[3].Value), int.Parse(e.Groups[4].Value), int.Parse(e.Groups[5].Value)));
            }
            var validTickets = tickets.Where(x => x.All(v => ticketData.Any(t => (t.minA <= v && t.maxA >= v) || (t.minB <= v && t.maxB >= v)))).ToList();
            var positions = ticketData.ToDictionary(x => x.name, x => -1);

            while (positions.Any(x => x.Value < 0))
            {
                var ticketPositions = new List<(int index, List<string> possibleMatches)>();
                for (var i = 0; i < validTickets[0].Length; i++)
                {
                    var group = validTickets.Select(x => x[i]).ToList();
                    var matches = ticketData
                        .Where(x => positions[x.name] < 0 &&
                                    group.All(g => (x.minA <= g && x.maxA >= g) || (x.minB <= g && x.maxB >= g)))
                        .Select(x => x.name).ToList();
                    ticketPositions.Add((i, matches));
                }

                foreach (var t in ticketPositions.Where(x => x.possibleMatches.Count == 1))
                {
                    positions[t.possibleMatches.Single()] = t.index;
                }
            }

            return positions.Where(x => x.Key.StartsWith("departure")).Select(x => long.Parse(ticket[x.Value])).Aggregate((x,y) => x*y);
        }
    }
}