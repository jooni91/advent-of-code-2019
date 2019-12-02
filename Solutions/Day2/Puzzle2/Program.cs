using Puzzle1;
using System;
using System.Collections.Generic;
using System.IO;

namespace Puzzle2
{
    class Program
    {
        public static List<int> Opcode = new List<int>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to puzzle 1 of day 2!");

            var opcodeStrings = File.ReadAllText("Opcode.txt").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Opcode = new List<int>();

            foreach (var s in opcodeStrings)
            {
                Opcode.Add(Convert.ToInt32(s));
            }

            Console.WriteLine("Loaded the opcode from the file into memory.");

            Console.WriteLine("What is the output you are looking for?");

            GetNounAndVerbOfOutput(Convert.ToInt32(Console.ReadLine()));
        }

        public static void GetNounAndVerbOfOutput(int output)
        {
            var computer = new IntcodeComputer(Opcode.ToArray());

            Console.WriteLine($"Starting program!");

            for (int i = 0; i < 100; i++)
            {
                for (int k = 0; k < 100; k++)
                {
                    computer.SetOpcode(Opcode.ToArray());
                    computer[1] = i;
                    computer[2] = k;
                    computer.RunProgram();

                    if (computer[0] == output)
                    {
                        break;
                    }
                }

                if (computer[0] == output)
                {
                    break;
                }
            }

            Console.WriteLine($"Finnished running the program.");

            ReadProgramValuesLoop(computer);
        }
        public static void ReplacmentLoop(IntcodeComputer computer)
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

                ReplacmentLoop(computer);
            }
        }
        public static void ReadProgramValuesLoop(IntcodeComputer computer)
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
    }
}
