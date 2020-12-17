using System.Collections.Generic;
using System.Linq;
using AoCCore;

namespace Challenges
{
    public class AoC10 : AoCTool
    {
        public AoC10() : base(10)
        {
        }

        public override object CalculateSimple()
        {
            var input = GetInputArr().Select(int.Parse).ToArray();
            var max = input.Max();
            var cnt = new Dictionary<int, int>() {{1,0}, {2,0}, {3,0}};
            var val = 0;

            while (val < max)
            {
                var match = input.Where(x => x > val && x <= val + 3).OrderBy(x => x - val).First();
                var diff = match - val;
                cnt[diff] = cnt[diff] + 1;
                val = match;
            }

            return (cnt[1] * cnt[3]).ToString();
        }

        public override object CalculateExtended()
        {
            var input = GetInputArr().Select(int.Parse).ToArray();
            return GetWays(new Dictionary<int, long> { { 0, 1 } }, input.Prepend(0).Append(input.Max() + 3).ToList(), input.Max() + 3)+"";
        }

        private long GetWays(Dictionary<int, long> map, List<int> input, int value)
        {
            if (!map.ContainsKey(value))
                map.Add(value, input.Where(i => i < value && i >= value - 3).Select(p => GetWays(map, input, p)).Sum());
            return map[value];
        }
    }
}