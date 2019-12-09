using System;
using System.Linq;
using MyAoC2019.Utilities;

namespace MyAoC2019.Solutions.Day09
{
    public class Day9 : DayBase
    {
        protected override string Day => "09";

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
