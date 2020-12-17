using AoCCore;
using System;
using System.Collections.Generic;

namespace Challenges
{
    internal class AoC12 : AoCTool
    {
        public AoC12() : base(12)
        {
        }

        public override object CalculateSimple()
        {
            (int x, int y)[] dir = new[] { (1, 0), (0, -1), (-1, 0), (0, 1) };
            var h = new Dictionary<char, int>() { { 'N', 3 }, { 'E', 0 }, { 'S', 1 }, { 'W', 2 } };
            (int x, int y) p = (0, 0);
            var cd = 0;
            foreach (var command in GetInputArr())
            {
                var cmd = command[0];
                var val = int.Parse(command.Substring(1));
                switch (cmd)
                {
                    case 'N':
                    case 'S':
                    case 'E':
                    case 'W':
                        p = (p.x + dir[h[cmd]].x * val, p.y + dir[h[cmd]].y * val);
                        break;
                    case 'F':
                        p = (p.x + dir[cd].x * val, p.y + dir[cd].y * val);
                        break;
                    case 'L':
                        cd = (cd+4 - (val / 90)) % 4;
                        break;
                    case 'R':
                        cd = (cd + (val / 90)) % 4;
                        break;
                }

            }
            return (Math.Abs(p.x) + Math.Abs(p.y))+"";
        }

        public override object CalculateExtended()
        {
            (int x, int y)[] dir = new[] { (1, 0), (0, -1), (-1, 0), (0, 1) };
            var h = new Dictionary<char, int>() { { 'N', 3 }, { 'E', 0 }, { 'S', 1 }, { 'W', 2 } };
            (int x, int y) wp = (10, 1);
            (int x, int y) ship = (0, 0);
            foreach (var command in GetInputArr())
            {
                var cmd = command[0];
                var val = int.Parse(command.Substring(1));
                switch (cmd)
                {
                    case 'N':
                    case 'S':
                    case 'E':
                    case 'W':
                        wp = (wp.x + dir[h[cmd]].x * val, wp.y + dir[h[cmd]].y * val);
                        break;
                    case 'F':
                        ship = (ship.x + wp.x * val, ship.y + wp.y * val);
                        break;
                    case 'R':
                        for (var i = 0; i < (val/90); ++i)
                            wp = (wp.y, -wp.x);
                        break;
                    case 'L':
                        for (var i = 0; i < (val/90); ++i)
                            wp = (-wp.y, wp.x);
                        break;
                }

            }
            
            return (Math.Abs(ship.x) + Math.Abs(ship.y))+"";
        }
    }
}