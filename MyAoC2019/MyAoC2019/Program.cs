using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyAoC2019
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = AskForArguments().ToArray();
            }

            if (args == null || args.Length != 2)
            {
                throw new ArgumentException();
            }

            Console.WriteLine($"Starting part {args[1]} of day {args[0]}.");

            var solutionForDay = Assembly.GetCallingAssembly().CreateInstance($"MyAoC2019.Solutions.Day{args[0]}.Day{args[0]}") as DayBase;

            if (solutionForDay == null)
            {
                throw new NullReferenceException("Couln't find solution for specified day.");
            }

            Console.WriteLine($"The result for day {args[0]} part {args[1]} is {solutionForDay.GetResult((Part)Enum.Parse(typeof(Part), args[1]))}.");

            Console.ReadLine();
        }

        private static IEnumerable<string> AskForArguments()
        {
            Console.WriteLine("You didn't pass the day and puzzle arguments to the program. What day do you want to run?");
            yield return Console.ReadLine();
            Console.WriteLine("Which part of the day, 1 or 2?");
            yield return Console.ReadLine();
        }
    }
}
