﻿using MyAoC2019.Utilities;
using System.Linq;

namespace MyAoC2019.Solutions.Day3
{
    public class Day3 : DayBase
    {
        protected override int Day => 3;

        protected override string PartOne(string input)
        {
            var wires = input.ReadInputLines().ToArray();

            return new TaxicabCalculator()
                .CalculateDistanceToClosestIntersection(wires[0].SplitInputs(), wires[1].SplitInputs())
                .ToString();
        }
        protected override string PartTwo(string input)
        {
            var wires = input.ReadInputLines().ToArray();

            return  new TaxicabCalculator()
                .CalculateMinRequiredStepsToIntersection(wires[0].SplitInputs(), wires[1].SplitInputs())
                .ToString();
        }
    }
}
