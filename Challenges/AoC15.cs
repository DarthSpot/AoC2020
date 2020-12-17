using System;
using System.Collections.Generic;
using System.Linq;
using AoCCore;

namespace Challenges
{
    public class AoC15 : AoCTool
    {
        public AoC15() : base(15)
        {
        }

        public override object CalculateSimple()
        {
            //var input = "0,14,6,20,1,4";
            var input = "0,14,6,20,1,4".Split(',')
                .Select(int.Parse).ToArray();

            return Memory(input).Skip(2020 - 1).First() + "";
        }

        public override object CalculateExtended()
        {
            var input = "0,14,6,20,1,4".Split(',')
                .Select(int.Parse).ToArray();

            return Memory(input).Skip(30000000 - 1).First() + "";
        }

        private IEnumerable<long> Memory(int[] data)
        {
            var map = new Dictionary<long, long>();
            var i = 1L;
            var current = -1L;
            foreach (var e in data)
            {
                if (current >= 0)
                    map.Add(current, i++);
                current = e;
                yield return e;
            }

            while (true)
            {
                if (!map.ContainsKey(current))
                {
                    map[current] = i++;
                    current = 0;
                }
                else
                {
                    var diff = i - map[current];
                    map[current] = i++;
                    current = diff;
                }

                yield return current;
            }
        }

        
    }
}