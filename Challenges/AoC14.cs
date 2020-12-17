using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using AoCCore;

namespace Challenges
{
    public class AoC14 : AoCTool
    {
        public AoC14() : base(14)
        {
        }

        public override object CalculateSimple()
        {
            var input = GetInputArr();
            var regex = new Regex("((?<cmd>mask)|((?<cmd>mem)\\[(?<num>\\d+)\\])) = (?<val>[\\dX]+)");
            var d = new Dictionary<int, (string bin, long val)>();
            var cMask = "";
            foreach (var line in input.Select(x => regex.Match(x)))
            {
                switch (line.Groups["cmd"].Value)
                {
                    case "mask":
                        cMask = line.Groups["val"].Value;
                        break;
                    case "mem":
                        d[int.Parse(line.Groups["num"].Value)] = StoreBit(long.Parse(line.Groups["val"].Value), cMask);
                        break;
                }
            }

            return d.Values.Select(x => Convert.ToInt64(x.bin, 2)).Sum() + "";
        }

        private (string, long) StoreBit(long val, string mask)
        {
            var bin = ToBinary(val);
            var res = string.Concat(bin.Zip(mask, (a, b) => b == 'X' ? a : b));
            return (res, Convert.ToInt64(res, 2));
        }

        private string ToBinary(long val)
        {
            return Convert.ToString(val, 2).PadLeft(36, '0');
        }

        public override object CalculateExtended()
        {
            var input = GetInputArr();
            var regex = new Regex("((?<cmd>mask)|((?<cmd>mem)\\[(?<num>\\d+)\\])) = (?<val>[\\dX]+)");
            var d = new Dictionary<string, (long num, long index)>();
            var cMask = "";
            var i = 0;
            foreach (var line in input.Select(x => regex.Match(x)))
            {
                switch (line.Groups["cmd"].Value)
                {
                    case "mask":
                        cMask = line.Groups["val"].Value;
                        break;
                    case "mem":
                        var adress = MaskAdress(int.Parse(line.Groups["num"].Value), cMask);
                        d[adress] = (long.Parse(line.Groups["val"].Value), i++);
                        break;
                }
            }

            var res = 0L;
            foreach (var m in d.Keys)
            {
                var num = PossibleNums(m);
                var matches = d.Keys.Where(x => d[x].index > d[m].index)
                    .Select(x => MatchCount(m, x)).Sum();
                res += Math.Max(num-matches, 0L)*d[m].num;
            }
            
            return res;
        }

        private long MatchCount(string maskA, string maskB)
        {
            var mergeStr = maskA.Zip(maskB, (a, b) =>
            {
                if (a == b)
                    return a;
                if (a == 'X')
                    return b;
                if (b == 'X')
                    return a;
                return 'F';
            }).ToList();

            if (mergeStr.Any(x => x == 'F'))
                return 0L;
            else
                return PossibleNums(string.Concat(mergeStr));
        }


        private long PossibleNums(string fluffyAdress)
        {
            return (long)Math.Pow(2, fluffyAdress.Count(x => x == 'X'));
        }
        
        private string MaskAdress(int adress, string mask)
        {
            var bin = ToBinary(adress);
            return string.Concat(bin.Zip(mask, (a, b) => b == '0' ? a : b));
        }
    }
}