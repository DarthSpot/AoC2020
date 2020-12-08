using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AoCCore;

namespace Challenges
{
    public class AoC4 : AoCTool
    {
        public AoC4() : base(4)
        {
        }

        public override string CalculateSimple()
        {
            var regex = new Regex("([a-z]+):([^\\s]+)");
            var input = GetInput().Split("\n\n")
                .Select(x => regex.Matches(x).Select(g => (g.Groups[1].Value, g.Groups[2].Value)).ToList()).ToList();
            var fields = new[] {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};


            return input.Count(pp => fields.All(k => pp.Any(f => f.Item1 == k)))+"";
        }

        public override string CalculateExtended()
        {
            var regex = new Regex("([a-z]+):([^\\s]+)");
            var input = GetInput().Split("\n\n")
                .Select(x => regex.Matches(x).Select(g => (g.Groups[1].Value, g.Groups[2].Value)).ToList()).ToList();
            
            return input.Count(x => IsValid(x))+"";
        }

        private bool IsValid(List<(string key, string val)> passport)
        {
            (string key, string regex)[] fields = new[]
            {
                ("byr", "\\b(19[2-9][0-9])|(200[0-2])\\b"),
                ("iyr", "\\b20((1[0-9])|(20))\\b"),
                ("eyr", "\\b20((2[0-9])|(30))\\b"),
                ("hgt", "\\b(1(([5-8][0-9])|(9[0-3]))cm)|(((59)|(6[0-9])|(7[0-6]))in)\\b"),
                ("hcl", "^#[a-f0-9]{6}$"),
                ("ecl", "\\b(amb)|(blu)|(brn)|(gry)|(grn)|(hzl)|(oth)\\b"),
                ("pid", "\\b\\d{9}\\b")
            };

            foreach (var field in fields)
            {
                var val = GetVal(passport, field.key);
                if (val == null)
                    return false;
                var regex = new Regex(field.regex);
                if (!regex.IsMatch(val))
                    return false;
            }

            return true;
        }

        private string GetVal(List<(string key, string val)> passport, string key)
        {
            var vals = passport.Where(x => x.key == key).ToList();
            if (vals.Count != 1)
                return null;
            return vals.Single().val;
        }

        protected string GetTestInput()
        {
            return @"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in";
        }
    }
}