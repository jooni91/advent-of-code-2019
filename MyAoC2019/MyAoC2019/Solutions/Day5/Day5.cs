using MyAoC2019.Utilities;
using System.Linq;

namespace MyAoC2019.Solutions.Day5
{
    public class Day5 : DayBase
    {
        protected override int Day => 5;

        protected override string PartOne(string input)
        {
            new IntcodeComputer(input.SplitInputs().ConvertInputsToIntegers().ToArray()).RunProgram();

            return "(see result above)";
        }
        protected override string PartTwo(string input)
        {
            new IntcodeComputer(input.SplitInputs().ConvertInputsToIntegers().ToArray()).RunProgram();

            return "(see result above)";
        }
    }
}
