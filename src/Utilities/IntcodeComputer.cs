﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MyAoC2019.IcComputer;

namespace MyAoC2019.Utilities
{
    public class IntcodeComputer
    {
        private readonly bool _debugMode = false;
        private readonly int[] _initialOpcode;
        private int[] _opcode;
        private int _lastInstructionPoint = 0;

        public event EventHandler<string>? Pulse;

        public IntcodeComputer(int[] initialOpcode)
        {
            _initialOpcode = initialOpcode;
            _initialOpcode.CopyTo(_opcode = new int[_initialOpcode.Length], 0);
        }
        public IntcodeComputer(int[] initialOpcode, bool debugMode)
        {
            _debugMode = debugMode;
            _initialOpcode = initialOpcode;
            _initialOpcode.CopyTo(_opcode = new int[_initialOpcode.Length], 0);
        }

        public int this[int index]
        {
            get { return _opcode[index]; }
            set { _opcode[index] = value; }
        }

        /// <summary>
        /// All list that contains all the outputs from the program that was running.
        /// </summary>
        public List<int> Outputs { get; private set; } = new List<int>();
        public IntcodeThreadState State { get; private set; } = IntcodeThreadState.Halt;

        /// <summary>
        /// Start running the program.
        /// </summary>
        public void RunProgram(params string[] args)
        {
            State = IntcodeThreadState.Running;

            int argsIndex = 0;
            int skipCount = 4;
            bool sendSignalOnExit = false;

            for (int i = _lastInstructionPoint; i < _opcode.Length; i += skipCount)
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
                        if (GetInput(args.Length >= argsIndex + 1 ? args[argsIndex] : null, out int? input))
                        {
                            this[this[i + 1]] = (int)input;
                        }
                        skipCount = 2;
                        argsIndex++;
                        break;
                    case "04":
                        Outputs.Add(GetValue(i + 1, GetMode(this[i], 1)));
                        Console.WriteLine(Outputs.Last());
                        sendSignalOnExit = true;
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
                        State = IntcodeThreadState.Halt;
                        break;
                    default:
                        Console.WriteLine($"Encountered unknown opcode: {this[i]}");
                        skipCount = 1;
                        break;
                }

                if (State != IntcodeThreadState.Running)
                {
                    _lastInstructionPoint = i;

                    if (sendSignalOnExit)
                    {
                        Pulse?.Invoke(this, Outputs.Last().ToString());
                    }

                    break;
                }
            }
        }
        /// <summary>
        /// Resets the program to it's initial state.
        /// </summary>
        public void ResetProgram()
        {
            _initialOpcode.CopyTo(_opcode, 0);
            Outputs = new List<int>();
            _lastInstructionPoint = 0;
        }

        /// <summary>
        /// Signal the thread waiting for input
        /// </summary>
        /// <param name="args">The input that this thread is waiting for.</param>
        public void Signal(object? sender, string args)
        {
            if (State == IntcodeThreadState.Wait)
            {
                RunProgram(args);
            }
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
        private int GetValue(int pointer, int mode)
        {
            return mode == 0 ? this[this[pointer]] : this[pointer];
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
        private bool GetInput(string? args, [NotNullWhen(true)] out int? input)
        {
            if (_debugMode && string.IsNullOrEmpty(args))
            {
                Console.WriteLine("Input value:");
                input = Convert.ToInt32(Console.ReadLine());
                return true;
            }
            else
            {
                if (string.IsNullOrEmpty(args))
                {
                    State = IntcodeThreadState.Wait;
                    input = null;
                    return false;
                }
                else
                {
                    input = Convert.ToInt32(args);
                    return true;
                }
            }
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
