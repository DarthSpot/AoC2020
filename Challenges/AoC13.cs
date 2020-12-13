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
            long res = data[0].Item1;
            foreach (var e in data.Skip(1))
            {
                res = SuperLCM(res, e.Item1, e.Item2);
            }
            
            
            return res+"";
        }

        private long SuperLCM(long x, long y, int offset)
        {
            var p = CombinePhasesRotation(x, 0, y, -offset % y);
            return -p.phase % p.peroid;
        }

        private (long peroid, long phase) CombinePhasesRotation(long x_per, long x_phase, long y_per, long y_phase)
        {
            var eGcd = EGCD(x_per, y_per);
            var pDiff = x_phase - y_phase;
            var mod = pDiff % eGcd.gcd;
            var div = pDiff / eGcd.gcd;

            var comPer = x_per / eGcd.gcd * y_per;
            var comPh = (x_phase - eGcd.s * div * x_per) % comPer;
            return (comPer, comPh);
        }

        private (long gcd, long s, long t) EGCD(long a, long b)
        {
            var old_r = a;
            var r = b;
            var old_s = 1L;
            var s = 0L;
            var old_t = 0L;
            var t = 1L;
            while (r > 0)
            {
                var quot = a / b;
                old_r = r;
                r = a % b;
                var tmp = old_s;
                old_s = s;
                s = tmp - quot * s;
                tmp = old_t;
                old_t = t;
                t = tmp - quot * t;
            }

            return (old_r, old_s, old_t);
        }
    }
}