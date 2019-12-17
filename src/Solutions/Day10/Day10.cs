using System;
using System.Collections.Generic;
using System.Linq;
using MyAoC2019.Utilities;

using static MoreLinq.Extensions.RepeatExtension;

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
            var asteroidMap = CreateAsteroidMap(input);
            var detectionRates = CalcDetectionRateForAsteroids(asteroidMap);

            return GroupByAngle(AsteroidMapToList(asteroidMap).ToList(), detectionRates.Where(x => x.Value == detectionRates.Max(v => v.Value)).Select(x => (x.Key.Item1, x.Key.Item2)).First());
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
        private IEnumerable<(int, int)> AsteroidMapToList(int[,] asteroidMap)
        {
            for (int y = 0; y < asteroidMap.GetLength(1); y++)
            {
                for (int x = 0; x < asteroidMap.GetLength(0); x++)
                {
                    if (asteroidMap[x, y] == 1)
                    {
                        yield return (x, y);
                    }
                }
            }
        }
        private Dictionary<(int,int), int> CalcDetectionRateForAsteroids(int[,] asteroidMap)
        {
            var detectionRates = new Dictionary<(int, int), int>();

            for (int y = 0; y < asteroidMap.GetLength(1); y++)
            {
                for (int x = 0; x < asteroidMap.GetLength(0); x++)
                {
                    if (asteroidMap[x, y] == 1)
                    {
                        detectionRates.Add((x, y), CountAsteroidsInDirectSight(x, y, asteroidMap));
                    }
                }
            }

            return detectionRates;
        }
        private int CountAsteroidsInDirectSight(int positionX, int positionY, int[,] asteroidMap)
        {
            int count = 0;

            for (int y = 0; y < asteroidMap.GetLength(1); y++)
            {
                for (int x = 0; x < asteroidMap.GetLength(0); x++)
                {
                    if (positionX == x && positionY == y)
                    {
                        continue;
                    }

                    if (asteroidMap[x, y] == 1 && !HasObstacleInItsWay((x, y), (positionX, positionY), asteroidMap))
                    {
                        count++;
                    }
                }
            }

            return count;
        }
        private bool HasObstacleInItsWay((int X, int Y) asteroidPos, (int X, int Y) stationPos, int[,] map)
        {
            var distanceX = asteroidPos.X - stationPos.X;
            var distanceY = asteroidPos.Y - stationPos.Y;

            if (distanceX == 0)
            {
                distanceY = Math.Sign(distanceY);
            }
            else if (distanceY == 0)
            {
                distanceX = Math.Sign(distanceX);
            }
            else
            {
                var distancePrime = MathExtensions.GreatestCommonDivider(Math.Abs(distanceX), Math.Abs(distanceY));

                if (distancePrime == 1)
                {
                    return false;
                }

                distanceX /= distancePrime;
                distanceY /= distancePrime;
            }

            int _x = stationPos.X + distanceX, _y = stationPos.Y + distanceY;

            do
            {
                if (!(_x == asteroidPos.X && _y == asteroidPos.Y) && map[_x, _y] == 1)
                {
                    return true;
                }

                _x += distanceX;
                _y += distanceY;
            }
            while (_x >= 0 && _y >= 0 && _x < map.GetLength(0) && _y < map.GetLength(1)
                && ((Math.Sign(distanceX) <= 0 && _x >= asteroidPos.X) || (Math.Sign(distanceX) >= 0 && _x <= asteroidPos.X))
                && ((Math.Sign(distanceY) <= 0 && _y >= asteroidPos.Y) || (Math.Sign(distanceY) >= 0 && _y <= asteroidPos.Y)));

            return false;
        }
        private string GroupByAngle(List<(int x, int y)> asteroidMap, (int x, int y) stationPos)
        {
            var queues = asteroidMap
                .Where(a => a != stationPos)
                .Select(a =>
                {
                    var xDist = a.x - stationPos.x;
                    var yDist = a.y - stationPos.y;

                    var angle = Math.Atan2(xDist, yDist);
                    return (a.x, a.y, angle, dist: Math.Sqrt((xDist * xDist) + (yDist * yDist)));
                })
                .ToLookup(a => a.angle)
                .OrderByDescending(a => a.Key)
                .Select(a => new Queue<(int x, int y, double angle, double dist)>(a.OrderBy(b => b.dist)))
                .ToList();

            static IEnumerable<(int x, int y, double angle, double dist)> GetValue(
                Queue<(int x, int y, double angle, double dist)> q)
            {
                if (q.Count > 0)
                {
                    yield return q.Dequeue();
                }
            }

            return queues.Repeat()
                .SelectMany(GetValue)
                .Skip(199)
                .Take(1)
                .Select(a => (a.x * 100) + a.y)
                .Single()
                .ToString();
        }
    }
}
