using MyAoC2019.Utilities;
using System;
using System.Linq;

namespace MyAoC2019.Solutions.Day2
{
    public class Day2 : DayBase
    {
        public bool UnitTestMode { get; set; } = false;

        protected override int Day => 2;

        protected override string PartOne(string input)
        {
            var computer = new IntcodeComputer(input.SplitInputs().ConvertInputsToIntegers().ToArray(), true);

            if (UnitTestMode)
            {
                computer[1] = 12;
                computer[2] = 2;

                computer.RunProgram();

                return computer[0].ToString();
            }
            else
            {
                ReplacementLoop(computer);

                computer.RunProgram();

                ReadProgramValuesLoop(computer);

                return string.Empty;
            }
        }
        protected override string PartTwo(string input)
        {
            int output;

            if (UnitTestMode)
            {
                output = 19690720;
            }
            else
            {
                Console.WriteLine("What is the output that you are looking for?");
                output = Convert.ToInt32(Console.ReadLine());
            }

            return GetNounAndVerbOfOutput(output, input);
        }

        private void ReplacementLoop(IntcodeComputer computer)
        {
            Console.WriteLine("Do you want to replace any values? y = yes, just enter = no");
            var input = Console.ReadLine();

            if (input == "y")
            {
                Console.WriteLine("Enter the position of the value: ");
                var position = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the value: ");
                var value = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine($"Replacing value on position {position} with the value {value}.");
                computer[position] = value;

                ReplacementLoop(computer);
            }
        }
        private void ReadProgramValuesLoop(IntcodeComputer computer)
        {
            Console.WriteLine("Enter the position of the value you want to read or just press enter to exit.");

            try
            {
                var position = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine($"The value of position {position} is {computer[position]}.");

                ReadProgramValuesLoop(computer);
            }
            catch
            {

            }
        }
        private string GetNounAndVerbOfOutput(int output, string input)
        {
            var computer = new IntcodeComputer(input.SplitInputs().ConvertInputsToIntegers().ToArray(), true);

            for (int i = 0; i < 100; i++)
            {
                for (int k = 0; k < 100; k++)
                {
                    computer.ResetProgram();
                    computer[1] = i;
                    computer[2] = k;
                    computer.RunProgram();

                    if (computer[0] == output)
                    {
                        break;
                    }

                    Console.WriteLine("No match. Reset and try next pair of values.");
                }

                if (computer[0] == output)
                {
                    break;
                }
            }

            Console.WriteLine($"The results for the specified output ({output}) are: Noun (position 1) = {computer[1]}, Verb (position 2) = {computer[2]}.");
            Console.WriteLine($"The result for puzzle 2: 100 * {computer[1]} + {computer[2]} = {(100 * computer[1]) + computer[2]}");

            return ((100 * computer[1]) + computer[2]).ToString();
        }
    }
}
