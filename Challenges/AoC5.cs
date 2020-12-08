using System.Linq;
using AoCCore;

namespace Challenges
{
    public class AoC5 : AoCTool
    {
        public AoC5() : base(5)
        {
        }

        public override string CalculateSimple()
        {
            var input = GetInputArr().Select(x =>
                (GetPosition(127, 0, x.ToCharArray(0, 7)), 
                    GetPosition(7, 0, x.ToCharArray(7, 3)))).Select(x => x.Item1 * 8 + x.Item2).Max();

            return input + "";
        }

        private int GetPosition(int max, int pos, char[] dir)
        {
            if (dir.Length == 0 || max == pos)
                return pos;
            var c = dir.First();
            var m = pos+((max - pos) + 1)/2;

            if (c == 'F' || c == 'L')
                return GetPosition(m - 1, pos, dir.Skip(1).ToArray());
            else
                return GetPosition(max, m, dir.Skip(1).ToArray());
        }

        public override string CalculateExtended()
        {
            var seats = GetInputArr().Select(x =>
            {
                var col = GetPosition(7, 0, x.ToCharArray(7, 3));
                var row = GetPosition(127, 0, x.ToCharArray(0, 7));
                var id = row * 8 + col;
                return (row, col, id);
            }).ToDictionary(x => (x.row, x.col), x => x.id);

            for (var y = 0; y <= 127; y++)
            {
                for (var x = 0; x <= 7; x++)
                {
                    var id = y * 8 + x;
                    if (!seats.ContainsKey((y, x)))
                    {
                        var id1 = seats.Values.Contains(id + 1);
                        var id2 = seats.Values.Contains(id - 1);
                        if (id1 && id2)
                            return id + "";
                    }
                }
            }

            return "";
        }
    }
}