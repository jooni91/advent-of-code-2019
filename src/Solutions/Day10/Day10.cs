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
            var detectionRates = CalcDetectionRateForAsteroids(asteroidMap);

            return detectionRates.Values.Max().ToString();
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
                    detectionRates.Add((x, y), CountAsteroidsInDirectSight(x, y, asteroidMap));
                }
            }

            return detectionRates;
        }
        private int CountAsteroidsInDirectSight(int positionX, int positionY, int[,] asteroidMap)
        {
            int count = 0;

            for (int y = positionY; y >= 0; y--)
            {
                for (int x = positionX; x >= 0; x--)
                {
                    if (asteroidMap[x, y] == 1 && !HasObstacleInItsWayNeg((x, y), (positionX, positionY), asteroidMap))
                    {
                        count++;
                    }
                }
            }

            for (int y = positionY; y < asteroidMap.GetLength(1); y++)
            {
                for (int x = positionX; x < asteroidMap.GetLength(0); x++)
                {
                    if (asteroidMap[x, y] == 1 && !HasObstacleInItsWayPos((x, y), (positionX, positionY), asteroidMap))
                    {
                        count++;
                    }
                }
            }

            return count;
        }
        private bool HasObstacleInItsWayNeg((int x, int y) asteroidPos, (int x, int y) stationPos, int[,] map)
        {
            bool skip = true;

            do
            {
                if (!skip)
                {
                    if (map[asteroidPos.x, asteroidPos.y] == 1)
                    {
                        return true;
                    }
                }

                skip = false;

                asteroidPos.x += (int)Math.Sqrt(stationPos.x - asteroidPos.x);
                asteroidPos.y += (int)Math.Sqrt(stationPos.y - asteroidPos.y);
            }
            while (asteroidPos.x < stationPos.x || asteroidPos.y < stationPos.y);

            return false;
        }
        private bool HasObstacleInItsWayPos((int x, int y) asteroidPos, (int x, int y) stationPos, int[,] map)
        {
            bool skip = true;

            if (Math.Sqrt(asteroidPos.x - stationPos.x) == asteroidPos.x - stationPos.x)
            {
                return false;
            }
            if (Math.Sqrt(asteroidPos.y - stationPos.y) == asteroidPos.y - stationPos.y)
            {
                return false;
            }

            do
            {
                if (!skip)
                {
                    if (map[asteroidPos.x, asteroidPos.y] == 1)
                    {
                        return true;
                    }
                }

                skip = false;

                asteroidPos.x -= (int)Math.Sqrt(asteroidPos.x - stationPos.x);
                asteroidPos.y -= (int)Math.Sqrt(asteroidPos.y - stationPos.y);
            }
            while (asteroidPos.x > stationPos.x || asteroidPos.y > stationPos.y);

            return false;
        }
    }
}
