using System.Linq;
using System.Text.RegularExpressions;
using AoCCore;

namespace Challenges
{
    public class AoC2 : AoCTool
    {
        public AoC2() : base(2)
        {
        }

        public override string CalculateSimple()
        {
            var input = GetInputArr();
            var regex = new Regex("(\\d+)-(\\d+)\\s([a-z]):\\s([a-z]+)");
            return ""+input.Count(x =>
            {
                var m = regex.Match(x);
                var count = m.Groups[4].Value.Count(c => c == m.Groups[3].Value.Single());
                return count >= int.Parse(m.Groups[1].Value) && count <= int.Parse(m.Groups[2].Value);
            });
        }

        public override string CalculateExtended()
        {
            var input = GetInputArr();
            var regex = new Regex("(\\d+)-(\\d+)\\s([a-z]):\\s([a-z]+)");
            return "" + input.Count(x =>
            {
                var m = regex.Match(x);
                var p1 = int.Parse(m.Groups[1].Value);
                var p2 = int.Parse(m.Groups[2].Value);
                var c = m.Groups[3].Value.Single();
                var pc1 = m.Groups[4].Value[p1 - 1] == c;
                var pc2 = m.Groups[4].Value[p2 - 1] == c;
                return pc1 ^ pc2;
            });
        }
    }
}