using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using MyAoC2019.Utilities;

namespace MyAoC2019.Solutions.Day19
{
    public class Day19 : DayBase
    {
        protected override string Day => "19";

        protected override string PartOne(string input)
        {
            var map = new Dictionary<Vector2, bool>();
            var computer = new IntcodeComputer(input.SplitInputs().ConvertInputsToLongs().ToArray());

            computer.RunProgram();

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    computer.Signal(this, x.ToString());
                    computer.Signal(this, y.ToString());

                    map.Add(new Vector2(x, y), computer.Outputs.Last() == 1);
                }
            }

            return map.Count(pair => pair.Value).ToString();
        }

        protected override string PartTwo(string input)
        {
            throw new NotImplementedException();
        }
    }
}
