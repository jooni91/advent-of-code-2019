using DayOnePuzzleOne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DayOnePuzzleTwo
{
    class Program
    {
        private static List<int> calculationResults = new List<int>();

        private static ImportMasses? ImportMasses { get; set; }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the fuel calculator EXTENDED edition!");
            Console.WriteLine("If you want to enter manual mode, type 'y' and press enter. By just pressing enter you will enter the " +
                "automatic mode where you can import masses from the web or a file.");

            var calculatorMode = Console.ReadLine();

            if (calculatorMode == "y")
            {
                ConsoleManualLoop();
            }
            else
            {
                ImportMasses = new ImportMasses();

                await ConsoleAutomaticLoop();
            }
        }

        private async static Task ConsoleAutomaticLoop()
        {
            if (ImportMasses == null)
            {
                Console.WriteLine("Fatal error. Actually we should never hit this point. :D Just getting rid of the null warning below.");
                return;
            }

            //Console.WriteLine("Enter the URL for the import job: ");
            //var url = Console.ReadLine();
            //await foreach (var mass in ImportMasses.GetMassesFromWeb(url))
            //{
            //    var requiredFuel = CalculateRequiredFuel(mass);
            //    requiredFuel += CalculateRequiredFuelForFuel(requiredFuel);

            //    calculationResults.Add(requiredFuel);

            //    Console.WriteLine($"Imported mass: {mass} - The required fuel for the mass is {mass}");
            //}

            await foreach (var mass in ImportMasses.GetMassesFromEmbeddedFile())
            {
                var requiredFuel = CalculateRequiredFuel(mass);
                requiredFuel += CalculateRequiredFuelForFuel(requiredFuel);

                calculationResults.Add(requiredFuel);

                Console.WriteLine($"Imported mass: {mass} - The required fuel for the mass is {mass}");
            }

            Console.WriteLine($"In total {calculationResults.Count} masses were imported. " +
                $"The sum of required fuel for the masses is {calculationResults.Sum()}.");

            Console.ReadLine();
        }
        private static void ConsoleManualLoop()
        {
            Console.WriteLine("Please enter the mass of your module: ");

            var massInput = Console.ReadLine();
            var fuelRequired = CalculateRequiredFuel(Convert.ToInt32(massInput));
            fuelRequired += CalculateRequiredFuelForFuel(fuelRequired);

            calculationResults.Add(fuelRequired);

            Console.WriteLine($"For a mass of {massInput} you require {fuelRequired} of fuel.");

            GetNextUserCommand();
        }
        private static void GetNextUserCommand()
        {
            Console.WriteLine("If you want to calculate the required fuel for another mass, type 'm' for more. " +
                "If you want to get the sum of all previously calculated results, type 's'. If you want to exit, just press enter.");

            var finalInput = Console.ReadLine();

            if (finalInput == "m")
            {
                ConsoleManualLoop();
            }
            else if (finalInput == "s")
            {
                Console.WriteLine($"The required fuel for all the previously entered masses is {calculationResults.Sum()}.");
                GetNextUserCommand();
            }
        }
        private static int CalculateRequiredFuel(int mass)
        {
            return (mass / 3) - 2;
        }
        private static int CalculateRequiredFuelForFuel(int fuel)
        {
            int fuelRequired = 0;

            while (fuel > 0)
            {
                var fuelOfFuel = (fuel / 3) - 2;

                if (fuelOfFuel > 0)
                {
                    fuelRequired += fuelOfFuel;
                    fuel = fuelOfFuel;
                }
                else
                {
                    break;
                }
            }

            return fuelRequired;
        }
    }
}
