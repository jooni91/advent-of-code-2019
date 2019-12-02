using Puzzle1;
using System;
using System.Collections.Generic;

namespace Puzzle2
{
    class Program
    {
        public static List<int> Opcode = new List<int>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to puzzle 2 of day 2!");

            Console.WriteLine("What is the output that you are looking for?");

            GetNounAndVerbOfOutput(Convert.ToInt32(Console.ReadLine()));
        }

        public static void GetNounAndVerbOfOutput(int output)
        {
            var computer = new IntcodeComputer("Opcode.txt");

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
            Console.WriteLine($"The result for puzzle 2: 100 * {computer[1]} + {computer[2]} = {100 * computer[1] + computer[2]}");
            Console.ReadLine();
        }
    }
}
