using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using MyAoC2019.Computer;
using MyAoC2019.Utilities;

namespace MyAoC2019.Devices.Robots
{
    /// <summary>
    /// A super cool painting robot. Don't expect it to talk to you.
    /// </summary>
    public class PaintingRobot
    {
        private readonly IntcodeComputer _cpu;

        private Direction _facingDirection = Direction.Up;
        private Vector2 _robotPos = new Vector2(0, 0);

        public Color BackgroundDefaultColor { get; }

        /// <summary>
        /// Constructs a new painting robot. Unfortunatly the robot can be programmed only once. Capitalism!
        /// </summary>
        /// <param name="paintingInstructions">The program that this robot will be running.</param>
        /// <param name="backgroundColor">The initial background of the robots space.</param>
        public PaintingRobot(long[] paintingInstructions, Color? backgroundColor = null)
        {
            _cpu = new IntcodeComputer(paintingInstructions);

            BackgroundDefaultColor = backgroundColor ?? Color.Black;
        }

        public Dictionary<Vector2, Color> Grid { get; } = new Dictionary<Vector2, Color>();

        /// <summary>
        /// Call this to paint the painting.
        /// </summary>
        public void Paint()
        {
            var outputCount = 0;

            _cpu.RunProgram();

            do
            {
                _cpu.Signal(this, GetColorOfCurrentGrid() == Color.Black ? "0" : "1");

                if (_cpu.Outputs.Count > outputCount)
                {
                    outputCount = _cpu.Outputs.Count;

                    UpdateCurrentPositionColor(_cpu.Outputs[_cpu.Outputs.Count - 2] == 0 ? Color.Black : Color.White);
                    Turn(_cpu.Outputs.Last() == 0);
                    MoveOneStepForward();
                }
            }
            while (_cpu.State != IntcodeThreadState.Halt);
        }

        /// <summary>
        /// Get the count of how many fields the robot painted at least once. Does not count
        /// fields, which were painted multiple times.
        /// </summary>
        /// <returns></returns>
        public int GetUniquePaintingSteps()
        {
            return Grid.Count;
        }

        private void MoveOneStepForward()
        {
            var step = _facingDirection switch
            {
                Direction.Up => new Vector2(0, -1),
                Direction.Down => new Vector2(0, 1),
                Direction.Left => new Vector2(-1, 0),
                Direction.Right => new Vector2(1, 0),
                _ => throw new InvalidOperationException()
            };

            _robotPos += step;
        }
        private void Turn(bool turnLeft)
        {
            _facingDirection = _facingDirection switch
            {
                Direction.Left => turnLeft ? Direction.Down : Direction.Up,
                Direction.Right => turnLeft ? Direction.Up : Direction.Down,
                Direction.Up => turnLeft ? Direction.Left : Direction.Right,
                Direction.Down => turnLeft ? Direction.Right : Direction.Left,
                _ => throw new InvalidOperationException()
            };
        }
        private void UpdateCurrentPositionColor(Color color)
        {
            if (Grid.ContainsKey(_robotPos))
            {
                Grid[_robotPos] = color;
            }
            else
            {
                Grid.Add(_robotPos, color);
            }
        }
        private Color GetColorOfCurrentGrid()
        {
            return Grid.ContainsKey(_robotPos) ? Grid[_robotPos] : BackgroundDefaultColor;
        }
    }
}
