using System;
using System.Collections.Generic;
using System.Linq;
using AoCCore;

namespace Challenges
{
    public class AoC9 : AoCTool
    {
        public AoC9() : base(9)
        {
        }

        public override object CalculateSimple()
        {
            var input = GetInputArr().Select(long.Parse).ToArray();
            var pos = 0;
            for (var i = 25; i < input.Length-1; i++)
            {
                var pre = input.Skip(i-25).Take(25).ToArray();
                var check = input[i];
                if (!Values(pre).Any(x => x == check))
                {
                    return check + "";
                }
            }

            return "";
        }

        private IEnumerable<long> Values(long[] input)
        {
            for (var i = 0; i < 24; i++)
            {
                for (var j = i + 1; j < 25; j++)
                {
                    yield return input[i] + input[j];
                }
            }
        }

        public override object CalculateExtended()
        {
            var input = GetInputArr().Select(long.Parse).ToArray();
            var res = long.Parse(CalculateSimple().ToString());

            for (var i = 0; i < input.Length; i++)
            {
                var sum = 0L;
                var small = input[i];
                var large = input[i];
                for (var j = i; sum <= res && j < input.Length; j++)
                {
                    sum += input[j];
                    small = Math.Min(input[j], small);
                    large = Math.Max(input[j], large);
                    
                    if (sum == res)
                        return (small + large)+"";
                }

            }

            return "";
        }
    }
}