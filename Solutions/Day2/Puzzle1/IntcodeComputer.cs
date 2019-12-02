using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Puzzle1
{
    public class IntcodeComputer
    {
        private readonly int[] _initialOpcode;
        private int[] _opcode;

        public IntcodeComputer(string opcodeFilePath)
        {
            _initialOpcode = LoadInitialOpcodeFromFile(opcodeFilePath).ToArray();
            _initialOpcode.CopyTo(_opcode = new int[_initialOpcode.Length], 0);

            Console.WriteLine("Loaded the opcode from the file into memory.");
        }

        public int this[int index]
        {
            get { return _opcode[index]; }
            set { _opcode[index] = value; }
        }

        /// <summary>
        /// The current state of the computer.
        /// </summary>
        public bool ProgramRunning { get; private set; } = false;

        /// <summary>
        /// Start running the program.
        /// </summary>
        public void RunProgram()
        {
            ProgramRunning = true;

            Console.WriteLine($"Starting program!");

            for (int i = 0; i < _opcode.Length; i += 4)
            {
                switch (this[i])
                {
                    case 1:
                        this[this[i + 3]] = Add(this[this[i + 1]], this[this[i + 2]]);
                        break;
                    case 2:
                        this[this[i + 3]] = Multiply(this[this[i + 1]], this[this[i + 2]]);
                        break;
                    case 99:
                        ProgramRunning = false;
                        break;

                    default:
                        Console.WriteLine($"Encountered unknown opcode: {this[i]}");
                        break;
                }

                if (!ProgramRunning)
                {
                    break;
                }
            }

            Console.WriteLine($"Finnished running the program.");
        }
        /// <summary>
        /// Resets the program to it's initial state.
        /// </summary>
        public void ResetProgram()
        {
            _initialOpcode.CopyTo(_opcode, 0);
        }

        private int Add(int a, int b)
        {
            return a + b;
        }
        private int Multiply(int a, int b)
        {
            return a * b;
        }
        private IEnumerable<int> LoadInitialOpcodeFromFile(string path)
        {
            var opcodeStrings = File.ReadAllText(path).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var s in opcodeStrings)
            {
                yield return Convert.ToInt32(s);
            }
        }
    }
}
