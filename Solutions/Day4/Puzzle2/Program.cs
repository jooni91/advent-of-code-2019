using Puzzle1;
using System;
using Utilities;

namespace Puzzle2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to puzzle 2 of day 4!");

            var input = InputLoader.LoadInputsFromFileAsString("Inputs.txt").SplitInputs(new char[] { '-' });

            Console.WriteLine("Starting calculation of possible combinations for password range.");

            var combinations = new PasswordCracker().GetPossibleCombinationsPartTwo(input);

            Console.WriteLine($"The possible combinations for the range {input[0]}-{input[1]} where at least " +
                $"1 group of exactly 2 adjacent digits exist is {combinations}.");

            Console.ReadLine();
        }
    }
}
