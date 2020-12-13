using System.Collections.Generic;
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
            var next = data.Select(x => GetMultiples(int.Parse(x)).First(y => y > pos)).Min();

            return next * (next - pos)+"";
        }

        public IEnumerable<int> GetMultiples(int val)
        {
            var v = val;
            while (v < int.MaxValue - val)
            {
                yield return v;
                v += val;
            }
        }

        public override string CalculateExtended()
        {
            return base.CalculateExtended();
        }
    }
}