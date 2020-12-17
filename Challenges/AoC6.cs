using System.Linq;
using AoCCore;

namespace Challenges
{
    public class AoC6 : AoCTool
    {
        public AoC6() : base(6)
        {
        }

        public override object CalculateSimple()
        {
            var input = GetInput();
            var groups = input.Split("\n\n");

            return groups.Select(x => x.ToCharArray().Where(c => c >= 'a' && c <= 'z').Distinct().Count()).Sum() + "";
        }


        public override object CalculateExtended()
        {
            var input = GetInput().Trim();
            var groups = input.Split("\n\n");

            var res = 0;

            foreach (var group in groups)
            {
                var chars = group.Split('\n')
                    .Select(s => s.ToCharArray().Distinct()).Aggregate((a, b) => a.Intersect(b).ToArray())
                    .Where(c => c >= 'a' && c <= 'z').ToList();

                res += chars.Count;
            }


            return res + "";
        }
    }
}