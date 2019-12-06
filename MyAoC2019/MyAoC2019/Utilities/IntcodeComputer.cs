using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyAoC2019.Utilities
{
    public class IntcodeComputer
    {
        private readonly int[] _initialOpcode;
        private int[] _opcode;

        public IntcodeComputer(int[] initialOpcode)
        {
            _initialOpcode = initialOpcode;
            _initialOpcode.CopyTo(_opcode = new int[_initialOpcode.Length], 0);
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

            int skipCount = 4;

            for (int i = 0; i < _opcode.Length; i += skipCount)
            {
                switch (GetOpcode(this[i]))
                {
                    case "01":
                        this[GetValue(i + 3, 1)] = Add(GetValue(i + 1, GetMode(this[i], 1)), GetValue(i + 2, GetMode(this[i], 2)));
                        skipCount = 4;
                        break;
                    case "02":
                        this[GetValue(i + 3, 1)] = Multiply(GetValue(i + 1, GetMode(this[i], 1)), GetValue(i + 2, GetMode(this[i], 2)));
                        skipCount = 4;
                        break;
                    case "03":
                        Console.WriteLine("Input value:");
                        this[this[i + 1]] = Convert.ToInt32(Console.ReadLine());
                        skipCount = 2;
                        break;
                    case "04":
                        Console.WriteLine(GetValue(i + 1, GetMode(this[i], 1)));
                        skipCount = 2;
                        break;
                    case "05":
                        if (GetValue(i + 1, GetMode(this[i], 1)) != 0)
                        {
                            i = GetValue(i + 2, GetMode(this[i], 2)) - skipCount;
                            break;
                        }

                        skipCount = 3;
                        break;
                    case "06":
                        if (GetValue(i + 1, GetMode(this[i], 1)) == 0)
                        {
                            i = GetValue(i + 2, GetMode(this[i], 2)) - skipCount;
                            break;
                        }

                        skipCount = 3;
                        break;
                    case "07":
                        this[GetValue(i + 3, 1)] = GetValue(i + 1, GetMode(this[i], 1)) < GetValue(i + 2, GetMode(this[i], 2)) ? 1 : 0;
                        skipCount = 4;
                        break;
                    case "08":
                        this[GetValue(i + 3, 1)] = GetValue(i + 1, GetMode(this[i], 1)) == GetValue(i + 2, GetMode(this[i], 2)) ? 1 : 0;
                        skipCount = 4;
                        break;
                    case "99":
                        ProgramRunning = false;
                        break;
                    default:
                        Console.WriteLine($"Encountered unknown opcode: {this[i]}");
                        skipCount = 1;
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

        private string GetOpcode(int value)
        {
            var valueString = value.ToString();
            var returnValue = string.Empty;

            for (int i = valueString.Length - 1; i > -1; i--)
            {
                returnValue = returnValue.Insert(0, valueString[i].ToString());

                if (returnValue.Length == 2)
                {
                    break;
                }
            }

            if (returnValue.Length < 2)
            {
                returnValue = returnValue.Insert(0, "0");
            }

            return returnValue;
        }
        private int GetValue(int index, int mode)
        {
            if (mode == 0)
            {
                return this[this[index]];
            }
            else
            {
                return this[index];
            }
        }
        private int GetMode(int value, int index)
        {
            if (index == 3)
            {
                return 0;
            }

            var valueString = value.ToString();

            if (valueString.Length < 2 + index)
            {
                return 0;
            }

            return valueString[valueString.Length - 2 - index].ToString() == "0" ? 0 : 1;
        }
        private int Add(int a, int b)
        {
            return a + b;
        }
        private int Multiply(int a, int b)
        {
            return a * b;
        }
    }
}
