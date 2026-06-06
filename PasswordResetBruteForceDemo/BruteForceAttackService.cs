using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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

        public AttackResult RunMultiThreadAttack(string targetHash)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            long attempts = 0;
            bool isFound = false;
            string foundPassword = "";

            int maxThreads = Math.Max(1, Environment.ProcessorCount - 1);

            ParallelOptions options = new ParallelOptions
            {
                MaxDegreeOfParallelism = maxThreads
            };

            for (int length = 1; length <= 6; length++)
            {
                if (isFound)
                {
                    break;
                }

                List<string> combinations = generator.GenerateCombinations(length);

                Parallel.ForEach(combinations, options, (guess, loopState) =>
                {
                    if (isFound)
                    {
                        loopState.Stop();
                        return;
                    }

                    Interlocked.Increment(ref attempts);

                    if (validator.IsPasswordCorrect(guess, targetHash))
                    {
                        foundPassword = guess;
                        isFound = true;
                        loopState.Stop();
                    }
                });
            }

            stopwatch.Stop();

            return new AttackResult
            {
                IsFound = isFound,
                FoundPassword = foundPassword,
                Attempts = attempts,
                ElapsedTime = stopwatch.Elapsed
            };
        }
    }
}