using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AoCCore;

namespace Challenges
{
    public class AoC13 : AoCTool
    {
        public AoC13() : base(13)
        {
        }

        public override string CalculateSimple()
        {
            var input = GetInput().Split("\n");
            var pos = int.Parse(input[0]);
            var data = input[1].Split(",").Where(x => !string.Equals(x, "x")).ToList();
            var next = data.Select(x => (x, GetMultiples(int.Parse(x)).First(y => y > pos))).OrderBy(x => x.Item2).First();

            return int.Parse(next.x) * (next.Item2 - pos)+"";
        }

        public IEnumerable<long> GetMultiples(long val)
        {
            var v = val;
            while (v < long.MaxValue - val)
            {
                yield return v;
                v += val;
            }
        }

        public override string CalculateExtended()
        {
            var input = GetInput().Split("\n");
            var index = 0;
            var data = input[1].Split(",").Select(x => (x, index++)).Where(x => !string.Equals(x.x, "x")).Select(x => (int.Parse(x.x), x.Item2)).ToList();
            var high = data.Last();
            var res = GetMultiples(high.Item1).First(x => Valid(x, (high.Item1, high.Item2), data));
            
            return res+"";
        }

        private bool Valid(long t, (int val, int off) high, List<(int val, int off)> data)
        {
            foreach (var v in data)
            {
                if (v.val == high.val)
                    continue;
                if (((t-(high.off-v.off)) % v.val) != 0)
                    return false;
            }

            return true;
        }
    }
}