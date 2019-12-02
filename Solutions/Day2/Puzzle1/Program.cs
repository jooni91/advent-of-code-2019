using System;

namespace Puzzle1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to puzzle 1 of day 2!");

            var computer = new IntcodeComputer("Opcode.txt");

            ReplacmentLoop(computer);

            computer.RunProgram();

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
