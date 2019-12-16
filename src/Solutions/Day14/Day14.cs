using System.Linq;
using MyAoC2019.Devices.InterStellarRefinery;
using MyAoC2019.Utilities;

namespace MyAoC2019.Solutions.Day14
{
    public class Day14 : DayBase
    {
        protected override string Day => "14";

        protected override string PartOne(string input)
        {
            var nanofactory = new Nanofactory();

            return nanofactory.GetMaterialCost(input.ReadInputLines().ToArray(), "ORE", "FUEL", 1).ToString();
        }

        protected override string PartTwo(string input)
        {
            var nanofactory = new Nanofactory();
            nanofactory.Initialize(input.ReadInputLines().ToArray());

            return nanofactory.GetFuelAmountForOreCargo("ORE", "FUEL", 1000000000000).ToString();
        }
    }
}
