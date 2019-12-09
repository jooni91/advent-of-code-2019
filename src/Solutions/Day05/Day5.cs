using MyAoC2019.Utilities;
using System;
using System.Linq;

namespace MyAoC2019.Solutions.Day05
{
    public class Day5 : DayBase
    {
        protected override string Day => "05";

        protected override string PartOne(string input)
        {
            var computer = new IntcodeComputer(input.SplitInputs().ConvertInputsToIntegers().ToArray(), true);
            computer.RunProgram(UnitTestMode ? new string[] { "1" } : Array.Empty<string>());

            return computer.Outputs.Last().ToString();
        }
        protected override string PartTwo(string input)
        {
            var computer = new IntcodeComputer(input.SplitInputs().ConvertInputsToIntegers().ToArray(), true);
            computer.RunProgram(UnitTestMode ? new string[] { "5" } : Array.Empty<string>());

            return computer.Outputs.Last().ToString();
        }
    }
}
