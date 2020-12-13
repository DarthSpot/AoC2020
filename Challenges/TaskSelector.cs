using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
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
                //new AoC11(),
                new AoC12(),
                new AoC13(),
                //new AoC14(),
                //new AoC15(),
                //new AoC16(),
                //new AoC17(),
            };
        }

        public AoCTool this[int task] => _tools.Length < task  || task < 0 ? null : _tools[task-1];
    }
}

