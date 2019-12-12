using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace MyAoC2019.Solutions.Day12
{
    public class Moon : IEquatable<Moon>
    {
        public Moon(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Velocity { get; private set; }
        public Vector3 Position { get; private set; }
        public int CurrentPotentialEnergy => (int)(Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z));
        public int CurrentKineticEnergy => (int)(Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z));
        public int CurrentTotalEnergy => CurrentPotentialEnergy * CurrentKineticEnergy;

        public void UpdateVelocity(Vector3 velocity)
        {
            Velocity += velocity;
        }

        public void UpdatePosition()
        {
            Position += Velocity;
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
