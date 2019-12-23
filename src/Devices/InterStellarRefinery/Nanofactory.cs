using System;
using System.Collections.Generic;
using System.Linq;

namespace MyAoC2019.Devices.InterStellarRefinery
{
    public class Nanofactory
    {
        private readonly Dictionary<(string Name, int Produced), List<(string Name, int Required)>> _reactions = new Dictionary<(string, int), List<(string, int)>>();
        private readonly Dictionary<string, decimal> _reactionCost = new Dictionary<string, decimal>();
        private readonly Dictionary<string, long> _inventory = new Dictionary<string, long>();

        private long _rawMaterialDepth = 0;

        public void Initialize(string[] reactionsFormula)
        {
            ParseReactionsFormula(reactionsFormula);
        }

        public long GetMaterialCost(string[] reactionsFormula, string materialToCount, string materialToProduce, long amountToProduce)
        {
            Initialize(reactionsFormula);

            CalcProductionRequirementsForMaterial(materialToCount, materialToProduce, ref amountToProduce);

            return _rawMaterialDepth;
        }
        public long GetFuelAmountForOreCargo(string materialToCount, string materialToProduce, long oreCargoQuantity)
        {
            long max = 1;

            CalcProductionRequirementsForMaterial(materialToCount, materialToProduce, ref max);

            while (_rawMaterialDepth < oreCargoQuantity)
            {
                max *= 2;

                _rawMaterialDepth = 0;
                CalcProductionRequirementsForMaterial(materialToCount, materialToProduce, ref max);
            }

            // converge on correct answer using binary chop
            long min = 0;
            while (min < max - 1)
            {
                long mid = (min + max) / 2;

                _rawMaterialDepth = 0;
                CalcProductionRequirementsForMaterial(materialToCount, materialToProduce, ref mid);

                if (_rawMaterialDepth < oreCargoQuantity)
                {
                    // too low, chop to the higher half
                    min = mid;
                }
                else if (_rawMaterialDepth > oreCargoQuantity)
                {
                    // too high, chop to the lower half
                    max = mid;
                }
            }

            return min;
        }

        private long CalcProductionRequirementsForMaterial(string materialToCount, string materialToProduce, ref long amountToProduce)
        {
            var materialProductionFormula = _reactions.First(react => react.Key.Name == materialToProduce);
            var inventoryCount = _inventory.ContainsKey(materialProductionFormula.Key.Name) ? _inventory[materialProductionFormula.Key.Name] : 0L;

            if (inventoryCount != 0 && inventoryCount >= amountToProduce)
            {
                return 0;
            }
            else if (inventoryCount > 0)
            {
                amountToProduce -= inventoryCount;
                _inventory[materialProductionFormula.Key.Name] = 0;
            }

            var produceMultiplier = (long)Math.Ceiling(amountToProduce / (decimal)materialProductionFormula.Key.Produced);

            foreach (var (Name, Required) in materialProductionFormula.Value)
            {
                var requiredMultiplied = Required * produceMultiplier;

                if (Name == materialToCount)
                {
                    _rawMaterialDepth += requiredMultiplied;
                    continue;
                }

                var produced = CalcProductionRequirementsForMaterial(materialToCount, Name, ref requiredMultiplied);

                if (_inventory.ContainsKey(Name))
                {
                    _inventory[Name] += (produced - requiredMultiplied);
                }
                else
                {
                    _inventory.Add(Name, (produced - requiredMultiplied));
                }
            }

            return materialProductionFormula.Key.Produced * produceMultiplier;
        }
        private void ParseReactionsFormula(string[] reactionsFormula)
        {
            foreach(var reaction in reactionsFormula)
            {
                var reactionInOut = reaction.Split("=>");
                var materialRequieredForOutput = new List<(string, int)>();

                foreach(var material in reactionInOut[0].Split(','))
                {
                    var materialAndQuantitiy = material.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    materialRequieredForOutput.Add((materialAndQuantitiy[1], Convert.ToInt32(materialAndQuantitiy[0])));
                }

                var produces = reactionInOut[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                _reactions.Add((produces[1], Convert.ToInt32(produces[0])), materialRequieredForOutput);
            }
        }
    }
}
