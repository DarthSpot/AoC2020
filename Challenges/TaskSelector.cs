using System;
using System.Collections.Generic;
using System.Linq;
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
                //new AoC9(),
                //new AoC10(),
                //new AoC11(),
                //new AoC12(),
                //new AoC13(),
                //new AoC14(),
                //new AoC15(),
                //new AoC16(),
                //new AoC17(),
            };
        }

        public AoCTool this[int task] => _tools.Length < task  || task < 0 ? null : _tools[task-1];
    }

    public class AoC8 : AoCTool
    {
        public AoC8() : base(8)
        {

        }

        public override string CalculateSimple()
        {
            var input = GetInputArr();

            var pos = 0;
            var acc = 0;
            var visited = new HashSet<int>();
            while (true)
            {
                var instruction = input[pos].Split(' ');
                var cmd = instruction[0];
                var val = int.Parse(instruction[1]);
                if (visited.Add(pos))
                {
                    switch (cmd)
                    {
                        case "nop":
                            pos++;
                            break;
                        case "jmp":
                            pos += val;
                            break;
                        case "acc":
                            acc += val;
                            pos++;
                            break;
                    }
                }
                else
                {
                    return acc + "";
                }
            }
        }


        public override string CalculateExtended()
        {
            var input = GetInputArr();
            var i = 0;
            var poss = input.Select(x => (i++, x)).Where(x => x.x.StartsWith("jmp") ||
                                                              x.x.StartsWith("nop"));
            foreach (var pos in poss)
            {
                string[] code = input.ToArray();
                if (pos.x.StartsWith("nop"))
                {
                    code[pos.Item1] = pos.x.Replace("nop", "jmp");
                }
                else
                {
                    code[pos.Item1] = pos.x.Replace("jmp", "nop");
                }

                var res = Run(code);
                if (res.Item2)
                    return res.Item1 + "";
            }

            return "";
        }

        private (int, bool) Run(string[] code)
        {
            var pos = 0;
            var acc = 0;
            var visited = new HashSet<int>();
            while (pos < code.Length)
            {
                var instruction = code[pos].Split(' ');
                var cmd = instruction[0];
                var val = int.Parse(instruction[1]);
                if (visited.Add(pos))
                {
                    switch (cmd)
                    {
                        case "nop":
                            pos++;
                            break;
                        case "jmp":
                            pos += val;
                            break;
                        case "acc":
                            acc += val;
                            pos++;
                            break;
                    }
                }
                else
                {
                    return (acc, false);
                }
            }

            return (acc, true);
        }
    }
}
