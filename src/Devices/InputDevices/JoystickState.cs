using System;

namespace MyAoC2019.Devices.InputDevices
{
    [Flags]
    public enum JoystickState
    {
        Neutral = 0,
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8
    }
}
