using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

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
        /// Split inputs by an specified separator.
        /// </summary>
        /// <param name="inputs">Inputs to split.</param>
        /// <param name="separator">The separator to use. Commas are used as a default separator if not specified.</param>
        /// <returns></returns>
        public static string[] SplitInputs(this string inputs, params char[] separator)
        {
            if (separator.Length == 0)
            {
                separator = new char[] { ',' };
            }

            return inputs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Split inputs by an specified separator.
        /// </summary>
        /// <param name="inputs">Inputs to split.</param>
        /// <param name="keepSeparator">Include the separator in the returned result.</param>
        /// <param name="separator">The separator to use. Commas are used as a default separator if not specified.</param>
        /// <returns></returns>
        public static IEnumerable<string[]> SplitInputs(this IEnumerable<string> inputs, bool keepSeparator, params char[] separator)
        {
            if (separator.Length == 0)
            {
                separator = new char[] { ',' };
            }

            foreach (var s in inputs)
            {
                if (keepSeparator)
                {
                    yield return Regex.Split(s, $@"({string.Concat(separator)})");
                }
                else
                {
                    yield return s.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                }
            }
        }

        /// <summary>
        /// Converts a string to an int array.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static IEnumerable<int> ConvertInputsToIntegers(this string inputs)
        {
            foreach (var s in inputs)
            {
                yield return Convert.ToInt32(s.ToString());
            }

            Console.WriteLine("Converted inputs to integer values.");
        }

        /// <summary>
        /// Converts an array of strings to an int array.
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
        /// Converts an array of chars to an int array.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static IEnumerable<int> ConvertInputsToIntegers(this char[] inputs)
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
