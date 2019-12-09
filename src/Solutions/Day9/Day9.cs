using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAoC2019.Utilities;

namespace MyAoC2019.Solutions.Day9
{
    public class Day9 : DayBase
    {
        protected override int Day => 9;

        protected override string PartOne(string input)
        {
            var computer = new IntcodeComputer(input.SplitInputs().ConvertInputsToIntegers().ToArray(), true);
            computer.RunProgram(UnitTestMode ? new string[] { "1" } : Array.Empty<string>());

            return computer.Outputs.Last().ToString();
        }

        protected override string PartTwo(string input)
        {
            var computer = new IntcodeComputer(input.SplitInputs().ConvertInputsToIntegers().ToArray(), true);
            computer.RunProgram(UnitTestMode ? new string[] { "2" } : Array.Empty<string>());

            return computer.Outputs.Last().ToString();
        }
    }
}
