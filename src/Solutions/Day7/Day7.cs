using System;
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

            foreach (var combination in GeneratePermutations(phaseModes, null))
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

        private IList<T[]> GeneratePermutations<T>(T[] objs, long? limit)
        {
            var result = new List<T[]>();
            long n = Factorial(objs.Length);
            n = (!limit.HasValue || limit.Value > n) ? n : limit.Value;

            for (long k = 0; k < n; k++)
            {
                var kperm = GenerateKthPermutation(k, objs);
                result.Add(kperm);
            }

            return result;
        }
        private T[] GenerateKthPermutation<T>(long k, T[] objs)
        {
            var permutedObjs = new T[objs.Length];

            for (int i = 0; i < objs.Length; i++)
            {
                permutedObjs[i] = objs[i];
            }
            for (int j = 2; j < objs.Length + 1; j++)
            {
                k /= j - 1;                      // integer division cuts off the remainder
                long i1 = k % j;
                long i2 = j - 1;
                if (i1 != i2)
                {
                    var tmpObj1 = permutedObjs[i1];
                    var tmpObj2 = permutedObjs[i2];
                    permutedObjs[i1] = tmpObj2;
                    permutedObjs[i2] = tmpObj1;
                }
            }
            return permutedObjs;
        }
        private long Factorial(int n)
        {
            if (n < 0) { throw new Exception("Unaccepted input for factorial"); }    //error result - undefined
            if (n > 256) { throw new Exception("Input too big for factorial"); }  //error result - input is too big

            if (n == 0) { return 1; }

            // Calculate the factorial iteratively rather than recursively:

            long tempResult = 1;
            for (int i = 1; i <= n; i++)
            {
                tempResult *= i;
            }
            return tempResult;
        }
    }
}
