using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAoC2019.Utilities;

namespace MyAoC2019.Solutions.Day10
{
    public class Day10 : DayBase
    {
        protected override string Day => "10";

        protected override string PartOne(string input)
        {
            var asteroidMap = CreateAsteroidMap(input);

            return "";
        }

        protected override string PartTwo(string input)
        {
            throw new NotImplementedException();
        }

        private int[,] CreateAsteroidMap(string input)
        {
            var inputLines = input.ReadInputLines().ToArray();
            var map = new int[inputLines.First().Length, inputLines.Length];

            for (int y = 0; y < inputLines.Length; y++)
            {
                for (int x = 0; x < inputLines[y].Length; x++)
                {
                    map[x, y] = inputLines[y][x] == '#' ? 1 : 0;
                }
            }

            return map;
        }
        private Dictionary<(int,int), int> CalcDetectionRateForAsteroids(int[,] asteroidMap)
        {
            var detectionRates = new Dictionary<(int, int), int>();

            for (int y = 0; y < asteroidMap.GetLength(1); y++)
            {
                for (int x = 0; x < asteroidMap.GetLength(0); x++)
                {
                    
                }
            }
        }
    }
}
