using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using AoCCore;

namespace Challenges
{
    public class TaskSelector
    {
        private AoCTool[] _tools { get; set; }
        public int Recent => _tools.Length;

        public TaskSelector()
        {
            _tools = new AoCTool[]
            {
                new AoC1(),
                new AoC2(),
                new AoC3(),
                new AoC4(),
                new AoC5(),
                new AoC6(),
                new AoC7(),
                new AoC8(),
                new AoC9(),
                new AoC10(),
                //new AoC11(),
                //new AoC12(),
                //new AoC13(),
                //new AoC14(),
                //new AoC15(),
                //new AoC16(),
                //new AoC17(),
            };
        }

        public AoCTool this[int task] => _tools.Length < task  || task < 0 ? null : _tools[task-1];
    }

    public class AoC10 : AoCTool
    {
        public AoC10() : base(10)
        {
        }

        public override string CalculateSimple()
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

        public override string CalculateExtended()
        {
            var input = GetInputArr().Select(int.Parse).ToArray();
            var max = input.Max();
            var map = new Dictionary<int, long> {{0, 1}};
            return GetWays(map, input.Prepend(0).Append(max + 3).ToList(), max + 3)+"";
        }

        private long GetWays(Dictionary<int, long> map, List<int> input, int value)
        {
            if (map.ContainsKey(value))
                return map[value];
            var sum = input.Where(i => i < value && i >= value - 3).Select(p => GetWays(map, input, p)).Sum();
            map.Add(value, sum);
            return sum;
        }
    }
}

