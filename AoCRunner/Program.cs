﻿using System;
using System.Diagnostics;
using Challenges;

namespace AoCRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new TaskSelector();

            var task = t.Recent;

            var sw = new Stopwatch();
            sw.Start();
            Console.WriteLine(t[task].CalculateSimple().ToString());
            Console.WriteLine(sw.Elapsed);
            sw.Restart();
            Console.WriteLine(t[task].CalculateExtended().ToString());
            Console.WriteLine(sw.Elapsed);
            sw.Stop();

            Console.ReadKey();
        }
    }


}
