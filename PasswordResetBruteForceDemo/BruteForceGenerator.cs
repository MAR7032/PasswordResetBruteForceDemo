using System.Collections.Generic;

namespace PasswordResetBruteForceDemo
{
    public class BruteForceGenerator
    {
        // Same character set as PasswordGenerator.
        private const string Characters = "abc123";

        public List<string> GenerateCombinations(int length)
        {
            List<string> results = new List<string>();

            GenerateRecursive("", length, results);

            return results;
        }

        private void GenerateRecursive(string currentText, int targetLength, List<string> results)
        {
            if (currentText.Length == targetLength)
            {
                results.Add(currentText);
                return;
            }

            for (int i = 0; i < Characters.Length; i++)
            {
                GenerateRecursive(currentText + Characters[i], targetLength, results);
            }
        }
    }
}