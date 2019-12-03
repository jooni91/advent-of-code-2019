using Puzzle1;
using System;
using System.Linq;

namespace Puzzle2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to puzzle 2 of day 3!");

            var wires = InputLoader.LoadInputsFromFileAsString("Inputs.txt").ReadInputLines().ToArray();

            var calculator = new TaxicabCalculator();
            Console.WriteLine($"The distance to the closest intersection from the base is: " +
                $"{calculator.CalculateMinRequiredStepsToIntersection(wires[0].SplitInputs(), wires[1].SplitInputs())}");
            Console.ReadLine();
        }
    }
}
