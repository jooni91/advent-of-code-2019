using MyAoC2019.Utilities;

namespace MyAoC2019
{
    public abstract class DayBase
    {
        protected abstract string Day { get; }

        public bool UnitTestMode { get; set; } = false;

        public string GetResult(Part part)
        {
            return part == Part.One
                ? PartOne(GetInput())
                : PartTwo(GetInput());
        }

        protected abstract string PartOne(string input);
        protected abstract string PartTwo(string input);

        protected string GetInput()
        {
            return InputLoader.LoadInputsFromFileAsString($"InputFiles/Day{Day}.txt");
        }
    }

    public enum Part
    {
        One = 1,
        Two = 2
    }
}
