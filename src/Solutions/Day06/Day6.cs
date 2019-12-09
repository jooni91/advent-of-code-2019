using MyAoC2019.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyAoC2019.Solutions.Day06
{
    public class Day6 : DayBase
    {
        protected override string Day => "06";

        protected override string PartOne(string input)
        {
            return VerifyOrbitMap(input).Count().ToString();
        }
        protected override string PartTwo(string input)
        {
            return GetShortestJumpCountToSanta(input).ToString();
        }

        private IEnumerable<int> VerifyOrbitMap(string input)
        {
            var orbitMap = GetOrbitMapOfObjects(input);

            foreach (var mass in orbitMap)
            {
                var next = mass;

                while (next.Value != null)
                {
                    yield return 1;

                    next = orbitMap.First(map => map.Key == next.Value);
                }
            }
        }
        private int GetShortestJumpCountToSanta(string input)
        {
            var orbitMap = GetOrbitMapOfObjects(input);

            var orbitMapOfYou = GetMapToComOfObject(orbitMap, "YOU");
            var orbitMapOfSanta = GetMapToComOfObject(orbitMap, "SAN");
            orbitMapOfYou.Remove("YOU");
            orbitMapOfSanta.Remove("SAN");

            foreach (var mass in orbitMapOfYou)
            {
                var meetingPoint = orbitMapOfSanta.FirstOrDefault(orbit => orbit.Key == mass.Key).Key ?? null;

                if (!string.IsNullOrEmpty(meetingPoint))
                {
                    return GetJumpCountToTarget(orbitMapOfYou, meetingPoint).Count() - 1 + GetJumpCountToTarget(orbitMapOfSanta, meetingPoint).Count() - 1;
                }
            }

            throw new InvalidOperationException("No meeting point was found.");
        }
        private Dictionary<string, string?> GetOrbitMapOfObjects(string data)
        {
            var orbitMap = new Dictionary<string, string?>
            {
                { "COM", null }
            };

            foreach (var massOrbitMap in data.ReadInputLines().SplitInputs(false, ')'))
            {
                orbitMap.Add(massOrbitMap[1], massOrbitMap[0]);
            }

            return orbitMap;
        }
        private Dictionary<string, string?> GetMapToComOfObject(Dictionary<string, string?> globalOrbitMap, string objectName)
        {
            var objectMapToCom = new Dictionary<string, string?>
            {
                { objectName, globalOrbitMap[objectName]! }
            };

            var next = objectMapToCom.First();

            while (next.Value != null)
            {
                next = globalOrbitMap.First(map => map.Key == next.Value);
                objectMapToCom.Add(next.Key, next.Value);
            }

            return objectMapToCom;
        }
        private IEnumerable<int> GetJumpCountToTarget(Dictionary<string, string?> orbitMap, string targetName)
        {
            var next = orbitMap.First();

            while (next.Value != null)
            {
                yield return 1;

                if (next.Key == targetName)
                {
                    break;
                }

                next = orbitMap.First(map => map.Key == next.Value);
            }
        }
    }
}
