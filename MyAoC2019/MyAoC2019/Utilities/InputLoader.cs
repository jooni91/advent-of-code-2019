using System;
using System.Collections.Generic;
using System.IO;

namespace MyAoC2019.Utilities
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

            Console.WriteLine($"Loaded inputs from file {filePath}.");

            return result;
        }

        /// <summary>
        /// Split inputs by an specified seperator.
        /// </summary>
        /// <param name="inputs">Inputs to split.</param>
        /// <param name="seperator">The seperator to use. Commas are used as a default seperator if not specified.</param>
        /// <returns></returns>
        public static string[] SplitInputs(this string inputs, params char[] seperator)
        {
            if (seperator.Length == 0)
            {
                seperator = new char[] { ',' };
            }

            return inputs.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Converts an array of strings to numbers.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static IEnumerable<int> ConvertInputsToIntegers(this string[] inputs)
        {
            foreach (var s in inputs)
            {
                yield return Convert.ToInt32(s);
            }

            Console.WriteLine("Converted inputs to integer values.");
        }

        /// <summary>
        /// Use to enumerate through a string which contains seperated values, each on a new line.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static IEnumerable<string> ReadInputLines(this string inputs)
        {
            using var sr = new StringReader(inputs);

            while (sr.Peek() >= 0)
            {
                yield return sr.ReadLine()!;
            }
        }
    }
}
