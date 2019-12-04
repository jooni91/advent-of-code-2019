using System;
using System.Collections.Generic;
using System.IO;

namespace Utilities
{
    public static class InputLoader
    {
        /// <summary>
        /// Get the content of a file as string.
        /// </summary>
        /// <param name="filePath">The relative path to a file.</param>
        /// <returns></returns>
        public static string LoadInputsFromFileAsString(string filePath)
        {
            Console.WriteLine("Started loading inputs from file.");

            var result = File.ReadAllText(filePath);

            Console.WriteLine("Loading inputs from file was successful.");

            return result;
        }

        /// <summary>
        /// Split inputs by an specified seperator.
        /// </summary>
        /// <param name="inputs">Inputs to split.</param>
        /// <param name="seperator">The seperator to use. Commas are used as a default seperator if not specified.</param>
        /// <returns></returns>
        public static string[] SplitInputs(this string inputs, char[]? seperator = null)
        {
            if (seperator == null)
            {
                seperator = new char[] { ',' };
            }

            Console.WriteLine($"Splitting input by the following seperators {string.Concat(seperator).PadRight(1)}");

            return inputs.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Converts an array of strings to numbers.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static IEnumerable<int> ConvertInputsToIntegers(this string[] inputs)
        {
            Console.WriteLine("Converting inputs to integer values.");

            foreach (var s in inputs)
            {
                yield return Convert.ToInt32(s);
            }

            Console.WriteLine("Finnished converting inputs to integer values.");
        }

        /// <summary>
        /// Use to enumerate through a string which contains seperated values, each on a new line.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static IEnumerable<string> ReadInputLines(this string inputs)
        {
            Console.WriteLine("Start reading each line of input as own string value.");

            using var sr = new StringReader(inputs);

            while (sr.Peek() >= 0)
            {
                yield return sr.ReadLine()!;
            }

            Console.WriteLine("Finnished reading each line of input as own string value.");
        }
    }
}
