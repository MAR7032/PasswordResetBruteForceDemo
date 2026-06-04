using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordResetBruteForceDemo
{
    public class HashService
    {
        // Constant static salt required by the assignment.
        private const string Salt = "StaticSalt2026";

        public string ComputeSha256Hash(string password)
        {
            // Password and salt are joined before hashing.
            string passwordWithSalt = password + Salt;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(passwordWithSalt);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                string hashText = "";

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    hashText = hashText + hashBytes[i].ToString("x2");
                }

                return hashText;
            }
        }
    }
}
