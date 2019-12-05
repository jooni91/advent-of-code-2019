using System;
using Utilities;

namespace Puzzle1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to puzzle 1 of day 5!");

            var input = InputLoader.LoadInputsFromFileAsString("Inputs.txt").SplitInputs(new char[] { '-' });

            var computer = new IntcodeComputer("Inputs.txt");
            computer.RunProgram();

            Console.ReadLine();
        }
    }
}
