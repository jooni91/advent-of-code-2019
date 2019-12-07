using MyAoC2019.Utilities;
using System;
using System.Linq;

namespace MyAoC2019.Solutions.Day5
{
    public class Day5 : DayBase
    {
        public bool UnitTestMode { get; set; } = false;

        protected override int Day => 5;

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
