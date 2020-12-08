using System;
using System.Linq;
using AoCCore;

namespace Challenges
{
    public class AoC3 : AoCTool
    {
        public AoC3() : base(3)
        {
        }

        public override string CalculateSimple()
        {
            var input = GetInputArr();
            var width = input[0].Length;
            //
            var x = 0;
            var trees = 0;
            for (var y = 0; y < input.Length; ++y)
            {
                if (input[y][x % width] == '#')
                    trees++;
                x += 3;
            }

            return "" + trees;
        }

        public override string CalculateExtended()
        {
            var input = GetInputArr();
            var width = input[0].Length;
            (int x, int y)[] moves = new[] {(1, 1), (3, 1), (5, 1), (7, 1), (1, 2)};
            return ""+moves.Select(x => RunSlope(input, width, x.x, x.y)).Aggregate((x, y) => x * y);
        }

        private int RunSlope(string[] input, int width, int xMove, int yMove)
        {
            var x = 0;
            var trees = 0;
            for (var y = 0; y < input.Length; y += yMove)
            {
                if (input[y][x % width] == '#')
                    trees++;
                x += xMove;
            }
            return trees;
        }
    }
}