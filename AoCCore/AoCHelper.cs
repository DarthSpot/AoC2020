using System.IO;
using System.Text;

namespace AoCCore
{
    public class AoCHelper
    {
        private static readonly string _path = @"D:\Git\AoC\2020\AoC2020\AoCCore\Input";
        public static string ReadFile(int num)
        {
            return File.ReadAllText(_path + $@"\aoc{num}.txt");
        }

        public static string[] ReadFileArr(int num)
        {
            return File.ReadAllLines(_path + $@"\aoc{num}.txt");
        }
    }
}