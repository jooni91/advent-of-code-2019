using System;
using Utilities;

namespace Puzzle1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to puzzle 1 of day 4!");

            var input = InputLoader.LoadInputsFromFileAsString("Inputs.txt").SplitInputs(new char[] { '-' });

            Console.WriteLine("Starting calculation of possible combinations for password range.");

            var combinations = new PasswordCracker().GetPossibleCombinationsPartOne(input);

            Console.WriteLine($"The possible combinations for the range {input[0]}-{input[1]} is {combinations}.");

            Console.ReadLine();
        }
    }
}
