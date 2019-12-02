using System;

namespace Puzzle1
{
    public class IntcodeComputer
    {
        private int[] _opcode;

        public IntcodeComputer(int[] opcode)
        {
            _opcode = opcode;
        }

        public int this[int index]
        {
            get { return _opcode[index]; }
            set { _opcode[index] = value; }
        }

        public bool ProgramRunning { get; private set; } = false;

        public void RunProgram()
        {
            ProgramRunning = true;

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
        }

        public void SetOpcode(int[] opcode)
        {
            _opcode = opcode;
        }

        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Multiply(int a, int b)
        {
            return a * b;
        }
    }
}
