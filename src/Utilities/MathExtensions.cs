using System;
using System.Collections.Generic;
using System.Drawing;

namespace MyAoC2019.Utilities
{
    public static class MathExtensions
    {
        /// <summary>
        /// Generate permutations for a specified sequence of objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        /// <remarks>
        /// Thanks for the permutation methods to digEmAll! His answer can be found here: 
        /// https://stackoverflow.com/questions/3307529/permutation-in-c-sharp
        /// </remarks>
        public static IList<T[]> GeneratePermutations<T>(T[] objs, long? limit)
        {
            var result = new List<T[]>();
            long n = Factorial(objs.Length);
            n = (!limit.HasValue || limit.Value > n) ? n : limit.Value;

            for (long k = 0; k < n; k++)
            {
                var kperm = GenerateKthPermutation(k, objs);
                result.Add(kperm);
            }

            return result;
        }

        /// <summary>
        /// Check wheter the given number is a prime number or not.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsPrime(int number)
        {
            if (number == 2)
            {
                return true;
            }

            if (number <= 1 || number % 2 == 0)
            {
                return false;
            }

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Calculate the manhatten distance of two points in 2 dimensional space.
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <returns></returns>
        public static int ManhattanDistance((int X, int Y) pointA, (int X, int Y) pointB)
        {
            return Math.Abs(pointA.X - pointB.X) + Math.Abs(pointA.Y - pointB.Y);
        }

        /// <summary>
        /// Calculate the manhatten distance of two points in 2 dimensional space.
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <returns></returns>
        public static int ManhattanDistance(Point pointA, Point pointB)
        {
            return ManhattanDistance((pointA.X, pointA.Y), (pointB.X, pointB.Y));
        }

        private static T[] GenerateKthPermutation<T>(long k, T[] objs)
        {
            var permutedObjs = new T[objs.Length];

            for (int i = 0; i < objs.Length; i++)
            {
                permutedObjs[i] = objs[i];
            }
            for (int j = 2; j < objs.Length + 1; j++)
            {
                k /= j - 1;                      // integer division cuts off the remainder
                long i1 = k % j;
                long i2 = j - 1;
                if (i1 != i2)
                {
                    var tmpObj1 = permutedObjs[i1];
                    var tmpObj2 = permutedObjs[i2];
                    permutedObjs[i1] = tmpObj2;
                    permutedObjs[i2] = tmpObj1;
                }
            }
            return permutedObjs;
        }
        private static long Factorial(int n)
        {
            if (n < 0) { throw new ArgumentOutOfRangeException("Unaccepted input for factorial"); }    //error result - undefined
            if (n > 256) { throw new ArgumentOutOfRangeException("Input too big for factorial"); }  //error result - input is too big

            if (n == 0) { return 1; }

            // Calculate the factorial iteratively rather than recursively:

            long tempResult = 1;
            for (int i = 1; i <= n; i++)
            {
                tempResult *= i;
            }
            return tempResult;
        }
    }
}
