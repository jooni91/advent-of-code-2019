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
            return Calculate(input).Sum().ToString();
        }
        protected override string PartTwo(string input)
        {
            return Calculate(input, true).Sum().ToString();
        }

        private IEnumerable<int> Calculate(string input, bool calclateFuelForFuel = false)
        {
            foreach (var mass in input.ReadInputLines())
            {
                var requiredFuel = CalculateRequiredFuel(Convert.ToInt32(mass));

                if (calclateFuelForFuel)
                {
                    requiredFuel += CalculateRequiredFuelForFuel(requiredFuel);
                }

                yield return requiredFuel;
            }
        }
        private int CalculateRequiredFuel(int mass)
        {
            return (mass / 3) - 2;
        }
        private int CalculateRequiredFuelForFuel(int fuel)
        {
            int fuelRequired = 0;

            while (fuel > 0)
            {
                var fuelOfFuel = (fuel / 3) - 2;

                if (fuelOfFuel > 0)
                {
                    fuelRequired += fuelOfFuel;
                    fuel = fuelOfFuel;
                }
                else
                {
                    break;
                }
            }

            return fuelRequired;
        }
    }
}
