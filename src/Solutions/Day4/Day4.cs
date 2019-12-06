using MyAoC2019.Utilities;
using System;

namespace MyAoC2019.Solutions.Day4
{
    public class Day4 : DayBase
    {
        protected override int Day => 4;

        protected override string PartOne(string input)
        {
            var splittedInput = input.SplitInputs(new char[] { '-' });

            Console.WriteLine("Starting calculation of possible combinations for password range.");

            return new PasswordCracker().GetPossibleCombinationsPartOne(splittedInput).ToString();
        }
        protected override string PartTwo(string input)
        {
            var splittedInput = input.SplitInputs(new char[] { '-' });

            Console.WriteLine("Starting calculation of possible combinations for password range.");

            return new PasswordCracker().GetPossibleCombinationsPartTwo(splittedInput).ToString();
        }
    }
}
