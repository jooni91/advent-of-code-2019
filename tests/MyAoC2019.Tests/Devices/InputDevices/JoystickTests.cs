using MyAoC2019.Devices.InputDevices;
using Xunit;

namespace MyAoC2019.Tests.Devices.InputDevices
{
    public class JoystickTests
    {
        [Fact]
        public void ChangeState_Should_ChangeState()
        {
            // Arrange
            var joystick = new Joystick();

            // Act
            joystick.ChangeState(JoystickState.Down);

            // Assert
            Assert.Equal(JoystickState.Down, joystick.State);
        }

        [Theory]
        [InlineData(JoystickState.Neutral, 0)]
        [InlineData(JoystickState.Left, -1)]
        [InlineData(JoystickState.Right, 1)]
        [InlineData(JoystickState.Up, 0)]
        [InlineData(JoystickState.Down, 0)]
        public void ConvertCurrentStateToOpcode_ShouldReturn_ExpectedValue(JoystickState state, int expected)
        {
            // Arrange
            var joystick = new Joystick();

            // Act
            joystick.ChangeState(state);

            var opcode = joystick.ConvertCurrentStateToOpcode();

            // Assert
            Assert.Equal(expected, opcode);
        }
    }
}
