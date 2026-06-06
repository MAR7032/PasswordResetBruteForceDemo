using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace PasswordResetBruteForceDemo
{
    public class BruteForceAttackService
    {
        private BruteForceGenerator generator = new BruteForceGenerator();
        private PasswordValidator validator = new PasswordValidator();

        public AttackResult RunSingleThreadAttack(string targetHash)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            long attempts = 0;

            // The attack starts from length 1 and goes up to length 6.
            // It does not know the real password length.
            for (int length = 1; length <= 6; length++)
            {
                List<string> combinations = generator.GenerateCombinations(length);

                for (int i = 0; i < combinations.Count; i++)
                {
                    attempts++;

                    string guess = combinations[i];

                    if (validator.IsPasswordCorrect(guess, targetHash))
                    {
                        stopwatch.Stop();

                        return new AttackResult
                        {
                            IsFound = true,
                            FoundPassword = guess,
                            Attempts = attempts,
                            ElapsedTime = stopwatch.Elapsed
                        };
                    }
                }
            }

            stopwatch.Stop();

            return new AttackResult
            {
                IsFound = false,
                FoundPassword = "",
                Attempts = attempts,
                ElapsedTime = stopwatch.Elapsed
            };
        }
    }
}