using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoCCore
{
    public abstract class AoCTool
    {
        protected AoCTool(int num)
        {
            Num = num;
        }

        public int Num { get; }

        public virtual object CalculateSimple()
        {
            return "";
        }

        public virtual object CalculateExtended()
        {
            return "";
        }

        protected string[] GetInputArr()
        {
            return AoCHelper.ReadFileArr(Num);
        }

        protected string GetInput()
        {
            return AoCHelper.ReadFile(Num);
        }

        protected virtual string[] GetTestInputArr()
        {
            return null;
        }
    }
}
