using System;

namespace PasswordResetBruteForceDemo
{
    public class PerformanceLogger
    {
        public string CreateComparisonLog(AttackResult singleThreadResult, AttackResult multiThreadResult)
        {
            TimeSpan singleTime = singleThreadResult.ElapsedTime;
            TimeSpan multiTime = multiThreadResult.ElapsedTime;

            TimeSpan difference;

            if (singleTime > multiTime)
            {
                difference = singleTime - multiTime;
            }
            else
            {
                difference = multiTime - singleTime;
            }

            string log =
                "Performance Comparison" +
                "\nSingle-thread time: " + singleTime +
                "\nMulti-thread time: " + multiTime +
                "\nTime difference: " + difference +
                "\nSingle-thread attempts: " + singleThreadResult.Attempts +
                "\nMulti-thread attempts: " + multiThreadResult.Attempts;

            return log;
        }
    }
}