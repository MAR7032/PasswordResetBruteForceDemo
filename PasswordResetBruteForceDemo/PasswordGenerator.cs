using System;

namespace PasswordResetBruteForceDemo
{
    public class PasswordGenerator
    {
        // Small character set for testing.
        // This makes the brute force demo faster for viva.
        private const string Characters = "abc123";

        private readonly Random random = new Random();

        public string GeneratePassword()
        {
            // Random.Next(4, 6) gives 4 or 5.
            // 6 is not included, so it follows [4-6).
            int passwordLength = random.Next(4, 6);

            string password = "";

            for (int i = 0; i < passwordLength; i++)
            {
                int randomIndex = random.Next(Characters.Length);
                password = password + Characters[randomIndex];
            }

            return password;
        }
    }
}