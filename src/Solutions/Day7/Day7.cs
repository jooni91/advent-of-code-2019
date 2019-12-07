using System.Collections.Generic;
using System.Linq;
using MyAoC2019.IcComputer;
using MyAoC2019.Utilities;

namespace MyAoC2019.Solutions.Day7
{
    public class Day7 : DayBase
    {
        protected override int Day => 7;

        protected override string PartOne(string input)
        {
            return RunProgram(input, new int[] { 0, 1, 2, 3, 4 }).Max().ToString();
        }
        protected override string PartTwo(string input)
        {
            return RunProgram(input, new int[] { 5, 6, 7, 8, 9 }, true).Max().ToString();
        }

        private IEnumerable<int> RunProgram(string input, int[] phaseModes, bool feedbackLoopMode = false)
        {
            var opcode = input.SplitInputs().ConvertInputsToIntegers().ToArray();
            var cpu = new List<IntcodeComputer>
                {
                    new IntcodeComputer(opcode),
                    new IntcodeComputer(opcode),
                    new IntcodeComputer(opcode),
                    new IntcodeComputer(opcode),
                    new IntcodeComputer(opcode)
                };

            for (int i = 0; i < cpu.Count; i++)
            {
                cpu[i].Pulse += cpu[i == cpu.Count - 1 ? 0 : i + 1].Signal;
            }

            foreach (var combination in MathExtensions.GeneratePermutations(phaseModes, null))
            {
                do
                {
                    cpu[0].RunProgram(combination[0].ToString(), "0");
                    cpu[1].RunProgram(combination[1].ToString(), cpu[0].Outputs.Last().ToString());
                    cpu[2].RunProgram(combination[2].ToString(), cpu[1].Outputs.Last().ToString());
                    cpu[3].RunProgram(combination[3].ToString(), cpu[2].Outputs.Last().ToString());
                    cpu[4].RunProgram(combination[4].ToString(), cpu[3].Outputs.Last().ToString());

                    if (!feedbackLoopMode)
                    {
                        yield return cpu[4].Outputs.Last();

                        foreach (var thread in cpu)
                        {
                            thread.ResetProgram();
                        }
                    }
                    else if (cpu.All(thread => thread.State == IntcodeThreadState.Halt))
                    {
                        yield return cpu[4].Outputs.Last();

                        foreach (var thread in cpu)
                        {
                            thread.ResetProgram();
                        }
                    }
                }
                while (!cpu.All(thread => thread.State == IntcodeThreadState.Halt));
            }
        }
    }
}
