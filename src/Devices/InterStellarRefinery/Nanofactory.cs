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

        public long GetMaterialCost(string[] reactionsFormula, string materialToCount, string materialToProduce, int amountToProduce)
        {
            Initialize(reactionsFormula);

            CalcProductionRequirementsForMaterial(materialToCount, materialToProduce, amountToProduce);

            return _rawMaterialDepth;
        }
        public long GetFuelAmountForOreCargo(string materialToCount, string materialToProduce, long oreCargoQuantity)
        {
            //foreach (var reaction in _reactions.Where(react => react.Value.Any(dep => dep.Name == "ORE")))
            //{
            //    _rawMaterialDepth = 0;

            //    CalcProductionRequirementsForMaterial(materialToCount, reaction.Key.Name, reaction.Key.Produced);

            //    _reactionCost.Add(reaction.Key.Name, (decimal)_rawMaterialDepth / (decimal)reaction.Key.Produced);
            //}

            //while (_reactionCost.Count != _reactions.Count)
            //{
            //    foreach (var reaction in _reactions.Where(react => react.Value.All(dep => _reactionCost.ContainsKey(dep.Name)) && !_reactionCost.ContainsKey(react.Key.Name)))
            //    {
            //        decimal cost = 0;

            //        foreach (var (Name, Required) in reaction.Value)
            //        {
            //            cost += _reactionCost[Name] * Required;
            //        }

            //        _reactionCost.Add(reaction.Key.Name, (decimal)cost / (decimal)reaction.Key.Produced);
            //    }
            //}

            //var fuel = 0;

            //while (true)
            //{
            //    oreCargoQuantity -= (long)_reactionCost["FUEL"];

            //    if (oreCargoQuantity <= 0)
            //    {
            //        break;
            //    }

            //    fuel++;
            //}

            //var result = _reactionCost["FUEL"] * 200;

            long max = 1;

            CalcProductionRequirementsForMaterial(materialToCount, "FUEL", max);

            while (_rawMaterialDepth < oreCargoQuantity)
            {
                max *= 2;

                _rawMaterialDepth = 0;
                CalcProductionRequirementsForMaterial(materialToCount, "FUEL", max);
            }

            // converge on correct answer using binary chop
            long min = 0;
            while (min < max - 1)
            {
                long mid = (min + max) / 2;

                _rawMaterialDepth = 0;
                CalcProductionRequirementsForMaterial(materialToCount, "FUEL", mid);

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

        private void CalcProductionRequirementsForMaterial(string materialToCount, string materialToProduce, long amountToProduce)
        {
            var materialProductionFormula = _reactions.First(react => react.Key.Name == materialToProduce);
            var inventoryCount = _inventory.ContainsKey(materialProductionFormula.Key.Name) ? _inventory[materialProductionFormula.Key.Name] : 0L;

            for (long i = inventoryCount; i < amountToProduce; i += materialProductionFormula.Key.Produced)
            {
                foreach (var (Name, Required) in materialProductionFormula.Value)
                {
                    if (Name == materialToCount)
                    {
                        _rawMaterialDepth += Required;
                        continue;
                    }

                    if (_inventory.ContainsKey(Name) && _inventory[Name] >= Required)
                    {
                        _inventory[Name] -= Required;
                    }
                    else
                    {
                        CalcProductionRequirementsForMaterial(materialToCount, Name, Required);
                    }
                }

                if (_inventory.ContainsKey(materialProductionFormula.Key.Name))
                {
                    _inventory[materialProductionFormula.Key.Name] += materialProductionFormula.Key.Produced;
                }
                else
                {
                    _inventory.Add(materialProductionFormula.Key.Name, materialProductionFormula.Key.Produced);
                }
            }

            _inventory[materialProductionFormula.Key.Name] -= amountToProduce;
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
