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
        private readonly Dictionary<Vector2, Color> _grid = new Dictionary<Vector2, Color>();
        private readonly IntcodeComputer _cpu;

        private Direction _facingDirection = Direction.Up;
        private Vector2 _robotPos = new Vector2(0, 0);

        private Color BackgroundDefaultColor { get; }

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
            return _grid.Count;
        }

        /// <summary>
        /// Draw a bitmap of the painting.
        /// </summary>
        /// <param name="size">The size of the bitmap.</param>
        /// <param name="scale">Scale the bitmap size by this factor.</param>
        /// <param name="_padding">The padding to apply to the bitmap.</param>
        /// <param name="borderColor">If a padding is specified you can define the color for the padding. The default is <see cref="Color.Transparent"/>.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the <paramref name="size"/> width or height is zero or less.</exception>
        public Bitmap DrawBitmap(Size size, float scale = 1.0f, Size? _padding = null, Color? borderColor = null)
        {
            if (size.Width <= 0 || size.Height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "The size can not contain a zero or negative number.");
            }

            var width = (int)(size.Width * scale);
            var height = (int)(size.Height * scale);
            var padding = _padding ?? new Size(0, 0);

            var bitmap = new Bitmap(width + (padding.Width * 2), height + (padding.Height * 2));

            // Draw the border
            for (var x = 0; x < bitmap.Width; x++)
            {
                for (var y = 0; y < bitmap.Height; y++)
                {
                    if (y < padding.Height || y > bitmap.Height - 1 - padding.Height || x < padding.Width || x > bitmap.Width - 1 - padding.Width)
                    {
                        bitmap.SetPixel(x, y, borderColor ?? Color.Transparent);
                    }
                }
            }

            // Draw the image
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var color = _grid.ContainsKey(new Vector2((int)(x / scale), (int)(y / scale)))
                        ? _grid[new Vector2((int)(x / scale), (int)(y / scale))]
                        : BackgroundDefaultColor == Color.Black ? Color.White : Color.Black;

                    bitmap.SetPixel(x + padding.Width, y + padding.Height, color);
                }
            }

            return bitmap;
        }

        /// <summary>
        /// Get the dimensions of the painting. Make sure you have called <see cref="Paint"/>, before calling this one.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Thrown when nothing was painted yet.</exception>
        public Size GetGridDimensions()
        {
            if (_grid.Count < 2)
            {
                throw new InvalidOperationException("There was no painting to get dimensions of. Make sure to call Paint first.");
            }

            var rect = new Rectangle((int)_grid.Keys.Min(vect => vect.X), (int)_grid.Keys.Min(vect => vect.Y),
                (int)_grid.Keys.Max(vect => vect.X), (int)_grid.Keys.Max(vect => vect.Y));

            return new Size(rect.Width, rect.Height);
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
            if (_grid.ContainsKey(_robotPos))
            {
                _grid[_robotPos] = color;
            }
            else
            {
                _grid.Add(_robotPos, color);
            }
        }
        private Color GetColorOfCurrentGrid()
        {
            return _grid.ContainsKey(_robotPos) ? _grid[_robotPos] : BackgroundDefaultColor;
        }
    }
}
