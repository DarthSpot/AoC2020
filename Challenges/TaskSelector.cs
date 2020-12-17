using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using AoCCore;

namespace Challenges
{
    public class TaskSelector
    {
        private AoCTool[] _tools { get; set; }
        public int Recent => _tools.Length;

        public TaskSelector()
        {
            _tools = new AoCTool[]
            {
                new AoC1(),
                new AoC2(),
                new AoC3(),
                new AoC4(),
                new AoC5(),
                new AoC6(),
                new AoC7(),
                new AoC8(),
                new AoC9(),
                new AoC10(),
                new AoC11(),
                new AoC12(),
                new AoC13(),
                new AoC14(),
                new AoC15(),
                new AoC16(),
                new AoC17(),
            };
        }

        public AoCTool this[int task] => _tools.Length < task  || task < 0 ? null : _tools[task-1];
    }

    public class AoC17 : AoCTool
    {
        public AoC17() : base(17)
        {
        }

        protected override string[] GetTestInputArr()
        {
            return @".#.
..#
###".Split("\r\n");
        }

        public override object CalculateSimple()
        {
            var input = GetInputArr();
            var map = new Dictionary<(int x, int y, int z), bool>();
            var tmpMap = new Dictionary<(int x, int y, int z), bool>();
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[0].Length; x++)
                {
                    map.Add((x,y,0), input[y][x] == '#');
                }
            }

            for (var i = 0; i < 6; ++i)
            {
                tmpMap = new Dictionary<(int x, int y, int z), bool>(map);
                (int xMin, int xMax, int yMin, int yMax, int zMin, int zMax) borders = 
                (tmpMap.Keys.Select(x => x.x).Min()-1,
                    tmpMap.Keys.Select(x => x.x).Max()+1,
                    tmpMap.Keys.Select(x => x.y).Min()-1,
                    tmpMap.Keys.Select(x => x.y).Max()+1,
                    tmpMap.Keys.Select(x => x.z).Min()-1,
                    tmpMap.Keys.Select(x => x.z).Max()+1);
                
                for (var x = borders.xMin; x <= borders.xMax; ++x)
                {
                    for (var y = borders.yMin; y <= borders.yMax; ++y)
                    {
                        for (var z = borders.zMin; z <= borders.zMax; ++z)
                        {
                            var status = GetPosition(tmpMap, x, y, z);
                            var nPos = GetNeighbors(x, y, z).ToList();
                            var neighbors = nPos.Select(p => GetPosition(tmpMap, p.x, p.y, p.z)).Count(n => n);
                            if (status && !(neighbors == 3 || neighbors == 2))
                                map[(x, y, z)] = false;
                            else if (!status && neighbors == 3)
                                map[(x, y, z)] = true;
                        }
                    }
                }
            }

            return map.Values.Count(x => x);
        }

        private IEnumerable<(int x, int y, int z)> GetNeighbors(int x, int y, int z)
        {
            for (var rX = x - 1; rX <= x + 1; rX++)
            {
                for (var rY = y - 1; rY <= y + 1; rY++)
                {
                    for (var rZ = z - 1; rZ <= z + 1; rZ++)
                    {
                        if (rX == x && rY == y && rZ == z)
                            continue;
                        yield return (rX, rY, rZ);
                    }
                }
            }
        }

        private bool GetPosition(Dictionary<(int x, int y, int z), bool> map, int x, int y, int z)
        {
            if (!map.ContainsKey((x, y, z)))
            {
                map.Add((x,y,z), false);
            }

            return map[(x, y, z)];
        }

        public override object CalculateExtended()
        {
            var input = GetInputArr();
            var map = new Dictionary<(int x, int y, int z, int w), bool>();
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[0].Length; x++)
                {
                    map.Add((x, y, 0, 0), input[y][x] == '#');
                }
            }

            for (var i = 0; i < 6; ++i)
            {
                var tmpMap = new Dictionary<(int x, int y, int z, int w), bool>(map);
                (int xMin, int xMax, int yMin, int yMax, int zMin, int zMax, int wMin, int wMax) borders =
                    (tmpMap.Keys.Select(x => x.x).Min() - 1,
                        tmpMap.Keys.Select(x => x.x).Max() + 1,
                        tmpMap.Keys.Select(x => x.y).Min() - 1,
                        tmpMap.Keys.Select(x => x.y).Max() + 1,
                        tmpMap.Keys.Select(x => x.z).Min() - 1,
                        tmpMap.Keys.Select(x => x.z).Max() + 1,
                        tmpMap.Keys.Select(x => x.w).Min() - 1,
                        tmpMap.Keys.Select(x => x.w).Max() + 1);

                for (var x = borders.xMin; x <= borders.xMax; ++x)
                {
                    for (var y = borders.yMin; y <= borders.yMax; ++y)
                    {
                        for (var z = borders.zMin; z <= borders.zMax; ++z)
                        {
                            for (var w = borders.wMin; w <= borders.wMax; ++w)
                            {
                                var status = GetPosition(tmpMap, x, y, z, w);
                                var nPos = GetNeighbors(x, y, z, w).ToList();
                                var neighbors = nPos.Select(p => GetPosition(tmpMap, p.x, p.y, p.z, p.w)).Count(n => n);
                                if (status && !(neighbors == 3 || neighbors == 2))
                                    map[(x, y, z, w)] = false;
                                else if (!status && neighbors == 3)
                                    map[(x, y, z, w)] = true;
                            }
                        }
                    }
                }
            }

            return map.Values.Count(x => x);
        }

        private IEnumerable<(int x, int y, int z, int w)> GetNeighbors(int x, int y, int z, int w)
        {
            for (var rX = x - 1; rX <= x + 1; rX++)
            {
                for (var rY = y - 1; rY <= y + 1; rY++)
                {
                    for (var rZ = z - 1; rZ <= z + 1; rZ++)
                    {
                        for (var rW = w - 1; rW <= w + 1; rW++)
                        {
                            if (rX == x && rY == y && rZ == z && rW == w)
                                continue;
                            yield return (rX, rY, rZ, rW);
                        }
                    }
                }
            }
        }

        private bool GetPosition(Dictionary<(int x, int y, int z, int w), bool> map, int x, int y, int z, int w)
        {
            if (!map.ContainsKey((x, y, z, w)))
            {
                map.Add((x, y, z, w), false);
            }

            return map[(x, y, z, w)];
        }
    }
}

