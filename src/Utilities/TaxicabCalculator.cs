using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MyAoC2019.Utilities
{
    /// <summary>
    /// Calculator for day 3 of AoC. Making use of some Taxicab geometry for that.
    /// </summary>
    public class TaxicabCalculator
    {
        /// <summary>
        /// Calculate the Manhatten distance to the closest intersection of two paths to the base (starting point) on a 2D vector space.
        /// </summary>
        /// <param name="pathA">The first path.</param>
        /// <param name="pathB">The second path.</param>
        /// <returns></returns>
        public int CalculateDistanceToClosestIntersection(string[] pathA, string[] pathB)
        {
            return GetClosestDistanceToBase(StepsIn2DVectorSpace(pathA).ToHashSet(), StepsIn2DVectorSpace(pathB).ToHashSet());
        }

        /// <summary>
        /// Calculate the shortest way to an intersection from the base (starting point) and return the required steps to reach that point.
        /// </summary>
        /// <param name="pathA">The first path.</param>
        /// <param name="pathB">The second path.</param>
        /// <returns></returns>
        public int CalculateMinRequiredStepsToIntersection(string[] pathA, string[] pathB)
        {
            return GetStepsToClosestIntersection(StepsIn2DVectorSpace(pathA), StepsIn2DVectorSpace(pathB).ToHashSet()).Min();
        }

        private IEnumerable<int> GetStepsToClosestIntersection(IEnumerable<Point> coordsA, IEnumerable<Point> coordsB)
        {
            int index = 0;

            foreach (var point in coordsA)
            {
                if (coordsB.Contains(point))
                {
                    var steps = 2 + index + coordsB.ToList().FindIndex(0, pointB => pointB == point);

                    yield return steps;
                }

                index++;
            }
        }
        private IEnumerable<Point> StepsIn2DVectorSpace(string[] path)
        {
            var currentPoint = new Point(0, 0);

            foreach (var s in path)
            {
                for (int i = 0; i < RemoveFirstCharAndConvertToInt(s); i++)
                {
                    var nextPoint = s[0] switch
                    {
                        'U' => new Point(0, 1),
                        'D' => new Point(0, -1),
                        'R' => new Point(1, 0),
                        'L' => new Point(-1, 0),
                        _ => throw new ArgumentOutOfRangeException()
                    };

                    currentPoint = new Point(currentPoint.X + nextPoint.X, currentPoint.Y + nextPoint.Y);

                    yield return currentPoint;
                }
            }
        }
        private int RemoveFirstCharAndConvertToInt(string coord)
        {
            return Convert.ToInt32(coord.Substring(1));
        }
        private int GetClosestDistanceToBase(IEnumerable<Point> coordsA, IEnumerable<Point> coordsB)
        {
            return coordsA.Where(point => coordsB.Contains(point))
                .Select(point => MathExtensions.ManhattanDistance(new Point(0, 0), point))
                .Min();
        }
    }
}
