using System;

namespace Puzzle1
{
    public class PasswordCracker
    {
        public int GetPossibleCombinationsPartOne(string[] passwordRanges)
        {
            var passwordMaxValue = Convert.ToInt32(passwordRanges[1]);
            int possibleCombinationCount = 0;

            for (int i = Convert.ToInt32(passwordRanges[0]); i < passwordMaxValue; i++)
            {
                if (HasValidDigits(i.ToString()) && HasAdjacents(i.ToString()))
                {
                    possibleCombinationCount++;
                }
            }

            return possibleCombinationCount;
        }
        public int GetPossibleCombinationsPartTwo(string[] passwordRanges)
        {
            var passwordMaxValue = Convert.ToInt32(passwordRanges[1]);
            int possibleCombinationCount = 0;

            for (int i = Convert.ToInt32(passwordRanges[0]); i < passwordMaxValue; i++)
            {
                if (HasValidDigits(i.ToString()) && HasAdjacents(i.ToString(), 2))
                {
                    possibleCombinationCount++;
                }
            }

            return possibleCombinationCount;
        }

        private bool HasValidDigits(string password)
        {
            for (int i = 0; i < password.Length - 1; i++)
            {
                if (Convert.ToInt32(password[i]) > Convert.ToInt32(password[i + 1]))
                {
                    return false;
                }
            }

            return true;
        }
        private bool HasAdjacents(string password, int? exactNumberOfAdjacentAllowed = null)
        {
            int adjacentCount = 0;
            char? previousChar = null;

            for (int i = 0; i < password.Length; i++)
            {
                if (adjacentCount == 0 || password[i] == previousChar)
                {
                    adjacentCount++;
                }
                else
                {
                    if (exactNumberOfAdjacentAllowed == null
                        ? adjacentCount > 1
                        : adjacentCount == exactNumberOfAdjacentAllowed)
                    {
                        break;
                    }

                    adjacentCount = 1;
                }

                previousChar = password[i];
            }

            return exactNumberOfAdjacentAllowed == null 
                ? adjacentCount > 1 
                : adjacentCount == exactNumberOfAdjacentAllowed;
        }
    }
}
