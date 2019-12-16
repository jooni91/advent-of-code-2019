using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace MyAoC2019.Devices.Robots
{
    public class RepairDroid : Robot
    {
        private readonly Dictionary<Vector2, TileType> _map = new Dictionary<Vector2, TileType>();

        public RepairDroid(string program)
            : base(program)
        {

        }

        public int GetFastesWayToOxygenSystem()
        {
            _map.Add(_robotPos, TileType.Start);
            _cpu.RunProgram();
            var count = 0;

            while(count < 100000)
            {
                _facingDirection = GetNextBestDirectionToExplore();

                _cpu.Signal(this, DirectionAsOpcode(_facingDirection).ToString());

                if (_cpu.Outputs.Last() == 0)
                {
                    if (!_map.ContainsKey(_robotPos + GetCoordinatesOfDirection(_facingDirection)))
                    {
                        _map.Add(_robotPos + GetCoordinatesOfDirection(_facingDirection), TileType.Wall);
                    }

                    CheckAndMarkDeadEnd();

                    continue;
                }

                MoveOneStepForward();

                if (!_map.ContainsKey(_robotPos))
                {
                    _map.Add(_robotPos, _cpu.Outputs.Last() == 1 ? TileType.Empty : TileType.OxygenSystem);
                }

                CheckAndMarkDeadEnd();

                if (_cpu.Outputs.Last() == 2)
                {
                    _map[_robotPos] = TileType.OxygenSystem;
                }

                count++;
            }

            return 0;
        }

        private Direction GetNextBestDirectionToExplore()
        {
            var directions = GetDirectionRobotCanMove().ToList();

            for (int i = 0; i < directions.Count; i++)
            {
                if (ShouldNotGoInDirection(directions[i]))
                {
                    directions.RemoveAt(i);
                    i--;
                }
            }

            // Try to prefer directions which are unexplored and also disable dead end mode, 
            // because we have multiple directions to go
            if (directions.Count > 1)
            {
                directions = PrioritizeDirections(directions, 25);
            }

            // Prefer to go forward and not back, if possible
            if (directions.Count > 1 && directions.Contains(GetOppositeDirection(_facingDirection)))
            {
                directions.Remove(GetOppositeDirection(_facingDirection));
            }

            if (directions.Count == 1)
            {
                return directions.Single();
            }

            return directions[new Random().Next(0, directions.Count - 1)];
        }
        private bool ShouldNotGoInDirection(Direction direction)
        {
            var coordinalDirection = GetCoordinatesOfDirection(direction);
            var relativeLeft = GetCoordinatesOfDirection(GetRelativeLeft(direction));
            var relativeRight = GetCoordinatesOfDirection(GetRelativeRight(direction));
            var calculationPosition = _robotPos;

            if (!_map.ContainsKey(calculationPosition + coordinalDirection))
            {
                return false;
            }

            var hadPossibleWaysToGo = false;

            while (true)
            {
                calculationPosition += coordinalDirection;

                if (!_map.ContainsKey(calculationPosition + relativeLeft) || 
                    _map[calculationPosition + relativeLeft] == TileType.Empty ||
                    _map[calculationPosition + relativeLeft] == TileType.OxygenSystem)
                {
                    hadPossibleWaysToGo = true;
                }

                if (!_map.ContainsKey(calculationPosition + relativeRight) ||
                    _map[calculationPosition + relativeRight] == TileType.Empty ||
                    _map[calculationPosition + relativeRight] == TileType.OxygenSystem)
                {
                    hadPossibleWaysToGo = true;
                }

                if (!_map.ContainsKey(calculationPosition + coordinalDirection) ||
                    _map[calculationPosition + coordinalDirection] == TileType.OxygenSystem)
                {
                    hadPossibleWaysToGo = true;
                    break;
                }
                else if (_map[calculationPosition + coordinalDirection] == TileType.Wall ||
                    _map[calculationPosition + coordinalDirection] == TileType.DeadEnd)
                {
                    break;
                }
            }

            return !hadPossibleWaysToGo;
        }
        private IEnumerable<Direction> GetDirectionRobotCanMove()
        {
            for (int x = -1; x < 2; x += 2)
            {
                var potentialNextPos = new Vector2(_robotPos.X + x, _robotPos.Y);

                if (_map.ContainsKey(potentialNextPos) && (_map[potentialNextPos] == TileType.Wall || _map[potentialNextPos] == TileType.DeadEnd))
                {
                    continue;
                }

                yield return x == -1 ? Direction.Left : Direction.Right;
            }
            for (int y = -1; y < 2; y += 2)
            {
                var potentialNextPos = new Vector2(_robotPos.X, _robotPos.Y + y);

                if (_map.ContainsKey(potentialNextPos) && (_map[potentialNextPos] == TileType.Wall || _map[potentialNextPos] == TileType.DeadEnd))
                {
                    continue;
                }

                yield return y == -1 ? Direction.Down : Direction.Up;
            }
        }
        private Direction GetRelativeLeft(Direction direction)
        {
            return direction switch
            {
                Direction.Up => Direction.Left,
                Direction.Down => Direction.Right,
                Direction.Left => Direction.Down,
                Direction.Right => Direction.Up,
                _ => throw new InvalidOperationException()
            };
        }
        private Direction GetRelativeRight(Direction direction)
        {
            return direction switch
            {
                Direction.Up => Direction.Right,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
                Direction.Right => Direction.Down,
                _ => throw new InvalidOperationException()
            };
        }
        private Direction GetOppositeDirection(Direction direction)
        {
            return direction switch
            {
                Direction.Up => Direction.Down,
                Direction.Down => Direction.Up,
                Direction.Left => Direction.Right,
                Direction.Right => Direction.Left,
                _ => throw new InvalidOperationException()
            };
        }
        private List<Vector2> GetAllDirectionsAsCoordinates()
        {
            return new List<Vector2>()
            {
                new Vector2(0, 1),
                new Vector2(0, -1),
                new Vector2(1, 0),
                new Vector2(-1, 0)
            };
        }
        private List<Direction> PrioritizeDirections(List<Direction> directions, int sightOfView)
        {
            var priority = new List<Direction>();

            foreach (var direction in directions)
            {
                var position = _robotPos;

                for (int i = 0; i < sightOfView; i++)
                {
                    position += GetCoordinatesOfDirection(direction);

                    if (!_map.ContainsKey(position))
                    {
                        priority.Add(direction);
                        break;
                    }
                    else if (_map[position] == TileType.Wall)
                    {
                        break;
                    }
                }
            }

            if (priority.Count == 0)
            {
                return directions;
            }
            else
            {
                return priority;
            }
        }
        private Vector2 GetCoordinatesOfDirection(Direction direction)
        {
            return direction switch
            {
                Direction.Up => new Vector2(0, 1),
                Direction.Down => new Vector2(0, -1),
                Direction.Left => new Vector2(-1, 0),
                Direction.Right => new Vector2(1, 0),
                _ => throw new InvalidOperationException()
            };
        }
        private void CheckAndMarkDeadEnd()
        {
            var wallAndDeadEndCount = 0;

            foreach (var coord in GetAllDirectionsAsCoordinates())
            {
                if (_map.ContainsKey(_robotPos + coord) && (_map[_robotPos + coord] == TileType.Wall || 
                    _map[_robotPos + coord] == TileType.DeadEnd))
                {
                    wallAndDeadEndCount++;
                }
            }

            if (wallAndDeadEndCount == 3)
            {
                _map[_robotPos] = TileType.DeadEnd;
            }
        }
        private int DirectionAsOpcode(Direction direction)
        {
            return direction switch
            {
                Direction.Left => 3,
                Direction.Right => 4,
                Direction.Up => 1,
                Direction.Down => 2,
                _ => throw new InvalidOperationException("Unsupportd input state.")
            };
        }

        public Dictionary<Vector2, Color> ConvertToDrawableGrid()
        {
            var normalizedGrid = new Dictionary<Vector2, TileType>();
            var smallestX = Math.Abs(_map.Min(tile => tile.Key.X));
            var smallestY = Math.Abs(_map.Min(tile => tile.Key.Y));
            var drawableGrid = new Dictionary<Vector2, Color>();

            foreach(var tile in _map)
            {
                normalizedGrid.Add(new Vector2(tile.Key.X + smallestX, tile.Key.Y + smallestY), tile.Value);
            }

            foreach (var tile in normalizedGrid)
            {
                var color = tile.Value switch
                {
                    TileType.Wall => Color.Black,
                    TileType.Empty => Color.AntiqueWhite,
                    TileType.Start => Color.Green,
                    TileType.OxygenSystem => Color.AntiqueWhite,
                    _ => Color.White
                };

                drawableGrid.Add(tile.Key, color);
            }

            drawableGrid[new Vector2(_robotPos.X + smallestX, _robotPos.Y + smallestY)] = Color.Red;

            return drawableGrid;
        }
    }

    public enum TileType
    {
        Start,
        Empty,
        Wall,
        DeadEnd,
        OxygenSystem
    }
}
