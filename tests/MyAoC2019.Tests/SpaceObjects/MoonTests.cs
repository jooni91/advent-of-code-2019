using System.Numerics;
using MyAoC2019.SpaceObjects;
using MyAoC2019.Utilities;
using Xunit;

namespace MyAoC2019.Tests.Solutions.SpaceObjects
{
    public class MoonTests
    {
        [Fact]
        public void Constructor_ShouldCreate_NewMoonWithPosition()
        {
            // Arrange
            var initPos = new Vector3(3, 8, -2);

            // Act
            var moon = new Moon(initPos);

            // Assert
            Assert.NotNull(moon);
            Assert.Equal(initPos, moon.Position);
        }

        [Fact]
        public void UpdateVelocity_ShouldAddTo_ExistingVelocity()
        {
            // Arrange
            var moon = new Moon(new Vector3(3, 8, -2));

            // Act
            moon.UpdateVelocity(new Vector3(-2, 3, 8));
            moon.UpdateVelocity(new Vector3(4, -5, -2));

            // Assert
            Assert.Equal(new Vector3(2, -2, 6), moon.Velocity);
        }

        [Fact]
        public void UpdatePosition_ShouldAdd_VelocityToPosition()
        {
            // Arrange
            var moon = new Moon(new Vector3(3, 8, -2));

            // Act
            moon.UpdateVelocity(new Vector3(-2, 3, 8));
            moon.UpdatePosition();

            // Assert
            Assert.Equal(new Vector3(1, 11, 6), moon.Position);
        }

        [Fact]
        public void UpdatePosition_ShouldAdd_VelocityOnlyTo_PositionXAxis()
        {
            // Arrange
            var moon = new Moon(new Vector3(3, 8, -2));

            // Act
            moon.UpdateVelocity(new Vector3(-2, 3, 8));
            moon.UpdatePosition(Dimension.X);

            // Assert
            Assert.Equal(new Vector3(1, 8, -2), moon.Position);
        }

        [Fact]
        public void UpdatePosition_ShouldAdd_VelocityOnlyTo_PositionYAxis()
        {
            // Arrange
            var moon = new Moon(new Vector3(3, 8, -2));

            // Act
            moon.UpdateVelocity(new Vector3(-2, 3, 8));
            moon.UpdatePosition(Dimension.Y);

            // Assert
            Assert.Equal(new Vector3(3, 11, -2), moon.Position);
        }

        [Fact]
        public void UpdatePosition_ShouldAdd_VelocityOnlyTo_PositionZAxis()
        {
            // Arrange
            var moon = new Moon(new Vector3(3, 8, -2));

            // Act
            moon.UpdateVelocity(new Vector3(-2, 3, 8));
            moon.UpdatePosition(Dimension.Z);

            // Assert
            Assert.Equal(new Vector3(3, 8, 6), moon.Position);
        }

        [Theory]
        [InlineData(Dimension.X, 1)]
        [InlineData(Dimension.Y, 11)]
        [InlineData(Dimension.Z, 6)]
        public void GetPositionByDimension_ShouldReturn_ExpectedValue(Dimension dimension, int expected)
        {
            // Arrange
            var moon = new Moon(new Vector3(3, 8, -2));

            // Act
            moon.UpdateVelocity(new Vector3(-2, 3, 8));
            moon.UpdatePosition();
            var result = moon.GetPositionByDimension(dimension);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(Dimension.X, -2)]
        [InlineData(Dimension.Y, 3)]
        [InlineData(Dimension.Z, 8)]
        public void GetVelocityByDimension_ShouldReturn_ExpectedValue(Dimension dimension, int expected)
        {
            // Arrange
            var moon = new Moon(new Vector3(3, 8, -2));

            // Act
            moon.UpdateVelocity(new Vector3(-2, 3, 8));
            var result = moon.GetVelocityByDimension(dimension);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CurrentPotentialEnergy_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var moon = new Moon(new Vector3(3, 8, -2));

            // Act
            moon.UpdateVelocity(new Vector3(-2, 3, -5));
            moon.UpdatePosition();

            // Assert
            Assert.Equal(19, moon.CurrentPotentialEnergy);
        }

        [Fact]
        public void CurrentKineticEnergy_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var moon = new Moon(new Vector3(3, 8, -2));

            // Act
            moon.UpdateVelocity(new Vector3(-2, 3, -5));

            // Assert
            Assert.Equal(10, moon.CurrentKineticEnergy);
        }

        [Fact]
        public void CurrentTotalEnergy_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var moon = new Moon(new Vector3(3, 8, -2));

            // Act
            moon.UpdateVelocity(new Vector3(-2, 3, -5));
            moon.UpdatePosition();

            // Assert
            Assert.Equal(190, moon.CurrentTotalEnergy);
        }

        [Fact]
        public void Equals_ShouldReturn_False()
        {
            // Arrange
            var moon1 = new Moon(new Vector3(3, 8, -2));
            var moon2 = new Moon(new Vector3(3, 8, -2));

            // Act
            moon1.UpdateVelocity(new Vector3(-2, 3, -5));
            moon1.UpdatePosition();

            // Assert
            Assert.False(moon1.Equals(moon2));
        }

        [Fact]
        public void Equals_ShouldReturn_True()
        {
            // Arrange
            var moon1 = new Moon(new Vector3(3, 8, -2));
            var moon2 = new Moon(new Vector3(3, 8, -2));

            // Act

            // Assert
            Assert.True(moon1.Equals(moon2));
        }

        [Fact]
        public void Clone_ShouldReturn_CopyOfObject()
        {
            // Arrange
            var moon1 = new Moon(new Vector3(3, 8, -2));

            // Act
            var moon2 = moon1.Clone() as Moon;

            // Assert
            Assert.Equal(moon1, moon2);
        }

        [Fact]
        public void EqualityOperator_ShouldReturn_True()
        {
            // Arrange
            var moon1 = new Moon(new Vector3(3, 8, -2));
            var moon2 = new Moon(new Vector3(3, 8, -2));

            // Act

            // Assert
            Assert.True(moon1 == moon2);
        }

        [Fact]
        public void EqualityOperator_ShouldReturn_False()
        {
            // Arrange
            var moon1 = new Moon(new Vector3(3, 8, -2));
            var moon2 = new Moon(new Vector3(4, 8, -2));

            // Act

            // Assert
            Assert.False(moon1 == moon2);
        }

        [Fact]
        public void NegationEqualityOperator_ShouldReturn_True()
        {
            // Arrange
            var moon1 = new Moon(new Vector3(3, 8, -2));
            var moon2 = new Moon(new Vector3(4, 8, -2));

            // Act

            // Assert
            Assert.True(moon1 != moon2);
        }

        [Fact]
        public void NegationEqualityOperator_ShouldReturn_False()
        {
            // Arrange
            var moon1 = new Moon(new Vector3(3, 8, -2));
            var moon2 = new Moon(new Vector3(3, 8, -2));

            // Act

            // Assert
            Assert.False(moon1 != moon2);
        }
    }
}
