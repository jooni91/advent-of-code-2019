using System;
using System.Collections.Generic;
using System.Linq;

namespace MyAoC2019.Devices.InterStellarRefinery
{
    public class Nanofactory
    {
        private readonly Dictionary<(string Name, int Produced), List<(string Name, int Required)>> _reactions = new Dictionary<(string, int), List<(string, int)>>();
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
            long fuelCount = 0;

            while (_rawMaterialDepth < oreCargoQuantity)
            {
                CalcProductionRequirementsForMaterial(materialToCount, materialToProduce, 1);

                if (_rawMaterialDepth <= oreCargoQuantity)
                {
                    fuelCount++;
                }
            }

            return _rawMaterialDepth;
        }

        private void CalcProductionRequirementsForMaterial(string materialToCount, string materialToProduce, int amountToProduce)
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
