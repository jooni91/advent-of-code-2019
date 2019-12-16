using System;
using System.Linq;
using System.Numerics;
using MyAoC2019.Utilities;

namespace MyAoC2019.Devices.Robots
{
    public abstract class Robot
    {
        protected readonly IntcodeComputer _cpu;

        protected Direction _facingDirection = Direction.Up;
        protected Vector2 _robotPos = new Vector2(0, 0);

        protected Robot(long[] robotSoftware)
        {
            _cpu = new IntcodeComputer(robotSoftware);
        }
        protected Robot(string[] robotSoftware)
        {
            _cpu = new IntcodeComputer(robotSoftware.ConvertInputsToLongs().ToArray());
        }
        protected Robot(string robotSoftware)
        {
            _cpu = new IntcodeComputer(robotSoftware.SplitInputs().ConvertInputsToLongs().ToArray());
        }

        protected virtual void Turn(bool turnLeft)
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
        protected virtual void MoveOneStepForward()
        {
            var step = _facingDirection switch
            {
                Direction.Up => new Vector2(0, 1),
                Direction.Down => new Vector2(0, -1),
                Direction.Left => new Vector2(-1, 0),
                Direction.Right => new Vector2(1, 0),
                _ => throw new InvalidOperationException()
            };

            _robotPos += step;
        }
    }
}
