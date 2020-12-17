using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using AoCCore;

namespace Challenges
{
    public class AoC1 : AoCTool
    {
        public AoC1() : base(1)
        {
        }

        public override object CalculateSimple()
        {
            var values = GetInputArr().Select(int.Parse).ToList();
            var (a, b) = CrossLists(values).First(x => x.Item1 + x.Item2 == 2020);
            return ""+(a * b);
        }

        private IEnumerable<(int, int)> CrossLists(List<int> a)
        {
            for (var i = 0; i < a.Count; ++i)
            {
                for (var j = i+1; j < a.Count; ++j)
                {
                    yield return (a[i], a[j]);
                }
            }
        }

        public override object CalculateExtended()
        {
            var values = GetInputArr().Select(int.Parse).ToList();
            var (a, b, c) = CrossLists3(values).First(x => x.a + x.b + x.c == 2020);
            return "" + (a * b * c);
        }

        private IEnumerable<(int a, int b, int c)> CrossLists3(List<int> a)
        {
            for (var i = 0; i < a.Count; ++i)
            {
                for (var j = i+1; j < a.Count; ++j)
                {
                    for (var k = j+1; k < a.Count; ++k)
                    {
                        yield return (a[i], a[j], a[k]);
                    }
                }
            }
        }
    }
}