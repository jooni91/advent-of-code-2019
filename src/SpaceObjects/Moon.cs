using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using MyAoC2019.Utilities;

namespace MyAoC2019.SpaceObjects
{
    /// <summary>
    /// A moon orbiting in space.
    /// </summary>
    public class Moon : IEquatable<Moon>, ICloneable
    {
        public Moon(Vector3 position)
        {
            Position = position;
        }

        /// <summary>
        /// The velocity / speed at which the moon is orbiting.
        /// </summary>
        public Vector3 Velocity { get; private set; }

        /// <summary>
        /// The 3 dimensional position of the moon in space.
        /// </summary>
        public Vector3 Position { get; private set; }

        /// <summary>
        /// The potential energy that the moon has.
        /// </summary>
        public int CurrentPotentialEnergy => (int)(Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z));

        /// <summary>
        /// The kinetic energy that the moon has.
        /// </summary>
        public int CurrentKineticEnergy => (int)(Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z));

        /// <summary>
        /// The total energy of the moon, which is <see cref="CurrentPotentialEnergy"/> multiplied by <see cref="CurrentKineticEnergy"/>.
        /// </summary>
        public int CurrentTotalEnergy => CurrentPotentialEnergy * CurrentKineticEnergy;

        /// <summary>
        /// Add an amount of velocity to the current velocity of the moon.
        /// </summary>
        /// <param name="velocity">The amount which will be added to the current velocity. The argument will not be assigned as the new velocity.</param>
        public void UpdateVelocity(Vector3 velocity)
        {
            Velocity += velocity;
        }

        /// <summary>
        /// This will update the position by going one step forward in space by the velocity of the moon.
        /// </summary>
        /// <param name="dimension">If specified only that dimension will be updated.</param>
        public void UpdatePosition(Dimension? dimension = null)
        {
            if (dimension == null)
            {
                Position += Velocity;
            }
            else if (dimension == Dimension.X)
            {
                Position += new Vector3(Velocity.X, 0, 0);
            }
            else if (dimension == Dimension.Y)
            {
                Position += new Vector3(0, Velocity.Y, 0);
            }
            else
            {
                Position += new Vector3(0, 0, Velocity.Z);
            }
        }

        /// <summary>
        /// Get the position value of a specific axis.
        /// </summary>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public int GetPositionByDimension(Dimension dimension)
        {
            return dimension == Dimension.X ? (int)Position.X : dimension == Dimension.Y ? (int)Position.Y : (int)Position.Z;
        }

        /// <summary>
        /// Get the velocity value of a specific axis.
        /// </summary>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public int GetVelocityByDimension(Dimension dimension)
        {
            return dimension == Dimension.X ? (int)Velocity.X : dimension == Dimension.Y ? (int)Velocity.Y : (int)Velocity.Z;
        }



        public override bool Equals(object? obj)
        {
            return Equals(obj as Moon);
        }
        public bool Equals([AllowNull] Moon other)
        {
            return other != null &&
                   Velocity.Equals(other.Velocity) &&
                   Position.Equals(other.Position);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Velocity, Position);
        }

        public object Clone()
        {
            return new Moon(Position)
            {
                Velocity = Velocity
            };
        }

        public static bool operator ==(Moon left, Moon right)
        {
            return EqualityComparer<Moon>.Default.Equals(left, right);
        }
        public static bool operator !=(Moon left, Moon right)
        {
            return !(left == right);
        }
    }
}
