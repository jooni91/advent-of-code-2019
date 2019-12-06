using System;
using System.Collections.Generic;
using System.Linq;
using MyAoC2019.Utilities;

namespace MyAoC2019.Solutions.Day1
{
    public class Day1 : DayBase
    {
        protected override int Day => 1;

        protected override string PartOne(string input)
        {
            Console.WriteLine("Welcome to the fuel calculator!");
            Console.WriteLine("If you want to enter manual mode, type 'y' and press enter. By just pressing enter you will enter the " +
                "automatic mode where you can import masses from the web or a file.");

            var calculatorMode = Console.ReadLine();

            if (calculatorMode == "y")
            {
                return ConsoleManualLoop().ToString();
            }
            else
            {
                return ConsoleAutomaticLoop(input).Sum().ToString();
            }
        }
        protected override string PartTwo(string input)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<int> ConsoleAutomaticLoop(string input)
        {
            foreach (var mass in input.ReadInputLines())
            {
                var requiredFuel = CalculateRequiredFuel(Convert.ToInt32(mass));

                yield return requiredFuel;
            }
        }
        private int ConsoleManualLoop()
        {
            Console.WriteLine("Please enter the mass of your module: ");

            var massInput = Console.ReadLine();
            return  CalculateRequiredFuel(Convert.ToInt32(massInput));
        }
        private void GetNextUserCommand()
        {
            Console.WriteLine("If you want to calculate the required fuel for another mass, type 'm' for more. " +
                "If you want to get the sum of all previously calculated results, type 's'. If you want to exit, just press enter.");

            var finalInput = Console.ReadLine();

            if (finalInput == "m")
            {
                Console.WriteLine($"The required fuel for the mass {finalInput} is {ConsoleManualLoop()}");
                GetNextUserCommand();
            }
        }
        private int CalculateRequiredFuel(int mass)
        {
            return (mass / 3) - 2;
        }
    }
}
