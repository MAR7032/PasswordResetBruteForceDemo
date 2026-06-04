namespace PasswordResetBruteForceDemo
{
    public class PasswordValidator
    {
        private HashService hashService = new HashService();

        public bool IsPasswordCorrect(string guessedPassword, string targetHash)
        {
            string guessedHash = hashService.ComputeSha256Hash(guessedPassword);

            if (guessedHash == targetHash)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
