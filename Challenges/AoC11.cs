using System.Collections.Generic;
using System.Linq;
using AoCCore;

namespace Challenges
{
    public class AoC11 : AoCTool
    {
        public AoC11() : base(11)
        {
        }

        public override object CalculateSimple()
        {
            var map = ToMap(GetInputArr());
            var maxY = map.Keys.Max(x => x.y);
            var maxX = map.Keys.Max(x => x.x);
            var changes = 1;
            var tmpMap = new Dictionary<(int x, int y), char>(map);
            while (changes != 0)
            {
                changes = 0;
                for (var y = 0; y <= maxY; ++y)
                {
                    for (var x = 0; x <= maxX; x++)
                    {
                        var neighbors = GetNeighborValues(x, y).Where(p => map.ContainsKey((p.x, p.y)))
                            .Select(p => map[(p.x, p.y)]).ToList();
                        if (map[(x, y)] == 'L' && neighbors.All(n => n != '#'))
                        {
                            tmpMap[(x, y)] = '#';
                            changes++;
                        }
                        else if (map[(x, y)] == '#' && neighbors.Count(n => n == '#') >= 4)
                        {
                            tmpMap[(x, y)] = 'L';
                            changes++;
                        }
                    }
                }

                map = tmpMap;
                tmpMap = new Dictionary<(int x, int y), char>(map);
            }

            return map.Values.Count(c => c == '#') + "";
        }

        private Dictionary<(int x, int y), char> ToMap(string[] data)
        {
            var result = new Dictionary<(int x, int y), char>();
            for (var y = 0; y < data.Length; y++)
            {
                for (var x = 0; x < data[0].Length; x++)
                {
                    result.Add((x,y), data[y][x]);
                }
            }

            return result;
        }

        private char GetValue(string[] data, int x, int y)
        {
            if (x < 0 || y < 0 || y >= data.Length || x >= data[0].Length)
                return '.';
            return data[y][x];
        }


        private IEnumerable<(int x, int y)> GetNeighborValues(int x, int y)
        {
            yield return (x + 1, y);
            yield return (x + 1, y + 1);
            yield return (x, y + 1);
            yield return (x - 1, y + 1);
            yield return (x - 1, y);
            yield return (x - 1, y - 1);
            yield return (x, y - 1);
            yield return (x + 1, y - 1);
        }

        private IEnumerable<(int x, int y, char c)> GetNeighborValues2(Dictionary<(int x, int y), char> map, int x, int y)
        {
            (int x, int y)[] positions = new[] { (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0), (-1, -1), (0, -1), (1, -1) };
            var res = new HashSet<(int x, int y)>();
            var factor = 1;

            while (!positions.All(p => res.Contains(p)))
            {
                foreach (var pos in positions.Where(p => !res.Contains(p)).ToList())
                {
                    (int x, int y) rP = (pos.x * factor + x, pos.y * factor + y);
                    if (!map.ContainsKey(rP))
                        res.Add(pos);
                    else
                    {
                        var val = map[rP];
                        if (val == 'L' || val == '#')
                        {
                            res.Add(pos);
                            yield return (rP.x, rP.y, val);
                        }
                    }
                }

                factor++;
            }
        }

        public override object CalculateExtended()
        {
            var map = ToMap(GetInputArr());
            var maxY = map.Keys.Max(x => x.y);
            var maxX = map.Keys.Max(x => x.x);
            var changes = 1;
            var tmpMap = new Dictionary<(int x, int y), char>(map);
            while (changes != 0)
            {
                changes = 0;
                for (var y = 0; y <= maxY; ++y)
                {
                    for (var x = 0; x <= maxX; x++)
                    {
                        var neighbors = GetNeighborValues2(map, x, y).ToList();

                        if (map[(x, y)] == 'L' && neighbors.All(n => n.c != '#'))
                        {
                            tmpMap[(x, y)] = '#';
                            changes++;
                        }
                        else if (map[(x, y)] == '#' && neighbors.Count(n => n.c == '#') >= 5)
                        {
                            tmpMap[(x, y)] = 'L';
                            changes++;
                        }
                    }
                }

                map = tmpMap;
                tmpMap = new Dictionary<(int x, int y), char>(map);
            }

            return map.Values.Count(c => c == '#') + "";
        }
    }
}