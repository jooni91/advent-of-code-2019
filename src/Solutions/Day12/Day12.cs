using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using MyAoC2019.SpaceObjects;
using MyAoC2019.Utilities;

namespace MyAoC2019.Solutions.Day12
{
    public class Day12 : DayBase
    {
        protected override string Day => "12";

        protected override string PartOne(string input)
        {
            return TotalEnergyInSystem(ParseMoons(input.ReadInputLines()).ToList()).ToString();
        }

        protected override string PartTwo(string input)
        {
            return StepsNeededUntilRepeat(ParseMoons(input.ReadInputLines()).ToList()).Result.ToString();
        }

        private async Task<long> StepsNeededUntilRepeat(List<Moon> moons)
        {
            long stepsX = 0, stepsY = 0, stepsZ = 0;

            Task.WaitAll(Task.Factory.StartNew(async () => { stepsX = await CountCycleLengthForDimension(moons.ConvertAll(moon => (moon.Clone() as Moon)!), Dimension.X); }),
                Task.Factory.StartNew(async () => { stepsY = await CountCycleLengthForDimension(moons.ConvertAll(moon => (moon.Clone() as Moon)!), Dimension.Y); }),
                Task.Factory.StartNew(async () => { stepsZ = await CountCycleLengthForDimension(moons.ConvertAll(moon => (moon.Clone() as Moon)!), Dimension.Z); }));

            while (stepsX == 0 || stepsY == 0 || stepsZ == 0)
            {
                await Task.Delay(500);
            }

            return MathExtensions.LowestCommonMultiple(MathExtensions.LowestCommonMultiple(stepsX, stepsY), stepsZ);
        }
        private int TotalEnergyInSystem(List<Moon> moons)
        {
            for (int i = 0; i < 1000; i++)
            {
                CalculateVelocityByGravity(moons);

                foreach (var moon in moons)
                {
                    moon.UpdatePosition();
                }
            }

            return moons.Sum(moon => moon.CurrentTotalEnergy);
        }
        private void CalculateVelocityByGravity(List<Moon> moons)
        {
            var excludedMoons = new List<Moon>();

            foreach (var moon in moons)
            {
                foreach (var otherMoon in moons)
                {
                    if (moon == otherMoon || excludedMoons.Contains(otherMoon))
                    {
                        continue;
                    }

                    moon.UpdateVelocity(new Vector3(
                        GetVelocityChangeForAxis(moon.Position.X, otherMoon.Position.X),
                        GetVelocityChangeForAxis(moon.Position.Y, otherMoon.Position.Y),
                        GetVelocityChangeForAxis(moon.Position.Z, otherMoon.Position.Z)));

                    otherMoon.UpdateVelocity(new Vector3(
                        GetVelocityChangeForAxis(otherMoon.Position.X, moon.Position.X),
                        GetVelocityChangeForAxis(otherMoon.Position.Y, moon.Position.Y),
                        GetVelocityChangeForAxis(otherMoon.Position.Z, moon.Position.Z)));
                }

                excludedMoons.Add(moon);
            }
        }
        private int GetVelocityChangeForAxis(float pointA, float pointB)
        {
            if (pointA == pointB)
            {
                return 0;
            }
            else if (pointA > pointB)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
        private async Task<long> CountCycleLengthForDimension(List<Moon> moons, Dimension dimension)
        {
            return await Task.Run(() =>
            {
                var states = new HashSet<(int, int, int, int, int, int, int, int)>();

                states.Add((moons[0].GetPositionByDimension(dimension), moons[0].GetVelocityByDimension(dimension),
                    moons[1].GetPositionByDimension(dimension), moons[1].GetVelocityByDimension(dimension),
                    moons[2].GetPositionByDimension(dimension), moons[2].GetVelocityByDimension(dimension),
                    moons[3].GetPositionByDimension(dimension), moons[3].GetVelocityByDimension(dimension)));

                long count = 0;

                do
                {
                    count++;

                    CalculateVelocityByGravity(moons);

                    foreach (var moon in moons)
                    {
                        moon.UpdatePosition(dimension);
                    }

                    var current = (moons[0].GetPositionByDimension(dimension), moons[0].GetVelocityByDimension(dimension),
                        moons[1].GetPositionByDimension(dimension), moons[1].GetVelocityByDimension(dimension),
                        moons[2].GetPositionByDimension(dimension), moons[2].GetVelocityByDimension(dimension),
                        moons[3].GetPositionByDimension(dimension), moons[3].GetVelocityByDimension(dimension));

                    if (states.Contains(current))
                    {
                        break;
                    }
                    else
                    {
                        states.Add(current);
                    }
                }
                while (true);

                return count;
            });
        }
        private IEnumerable<Moon> ParseMoons(IEnumerable<string> input)
        {
            foreach (var row in input)
            {
                var position = row.Trim('<', '>').Replace(" ", "").Split(',');
                int x = 0, y = 0, z = 0;

                foreach (var dimen in position)
                {
                    var splittedDimen = dimen.Split('=');

                    if (splittedDimen[0] == "x")
                    {
                        x = Convert.ToInt32(splittedDimen[1]);
                    }
                    else if (splittedDimen[0] == "y")
                    {
                        y = Convert.ToInt32(splittedDimen[1]);
                    }
                    else
                    {
                        z = Convert.ToInt32(splittedDimen[1]);
                    }
                }

                yield return new Moon(new Vector3(x, y, z));
            }
        }
    }
}
