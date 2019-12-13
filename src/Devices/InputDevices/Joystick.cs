using System;

namespace MyAoC2019.Devices.InputDevices
{
    /// <summary>
    /// Just a simple virtual physical input device.
    /// </summary>
    public class Joystick
    {
        public JoystickState State = JoystickState.Neutral;

        /// <summary>
        /// Call to wait for input from the console.
        /// </summary>
        public void WaitForInput()
        {
            Console.WriteLine("Waiting for your input. Type 'L' = left or 'R' = right and enter.");

            State = Console.ReadLine().ToUpper() switch
            {
                "L" => JoystickState.Left,
                "R" => JoystickState.Right,
                _ => JoystickState.Neutral
            };
        }

        /// <summary>
        /// Change the state of the joystick programmatically.
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(JoystickState state)
        {
            State = state;
        }

        /// <summary>
        /// Convert the state into language that our <see cref="IntcodeComputer"/> understands.
        /// </summary>
        /// <returns></returns>
        public int ConvertCurrentStateToOpcode()
        {
            return State switch
            {
                JoystickState.Left => -1,
                JoystickState.Right => 1,
                _ => 0
            };
        }
    }
}
