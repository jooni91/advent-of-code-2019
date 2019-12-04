using System;
using Utilities;

namespace Puzzle2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to puzzle 2 of day 5!");

            var input = InputLoader.LoadInputsFromFileAsString("Inputs.txt").SplitInputs(new char[] { '-' });



            Console.ReadLine();
        }
    }
}
