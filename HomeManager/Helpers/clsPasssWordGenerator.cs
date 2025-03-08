using System;
using System.Security.Cryptography;

namespace HomeManager.Helpers
{
    public class PasswordGenerator
    {
        private const string ValidCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";

        /// <summary>
        /// Generates a password of the specified length.
        /// </summary>
        /// <param name="length">The desired length of the password.</param>
        /// <returns>A randomly generated password.</returns>
        public string GeneratePassword(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Length must be greater than 0.", nameof(length));
            }

            char[] password = new char[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);
                for (int i = 0; i < length; i++)
                {
                    password[i] = ValidCharacters[randomBytes[i] % ValidCharacters.Length];
                }
            }
            return new string(password);
        }
    }
}
