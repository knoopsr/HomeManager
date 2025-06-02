using System.Security.Cryptography;

namespace HomeManager.Helpers
{
    /// <summary>
    /// Genereert willekeurige wachtwoorden met letters, cijfers en symbolen.
    /// </summary>
    public class PasswordGenerator
    {
        /// <summary>
        /// De set van geldige karakters die gebruikt worden in het gegenereerde wachtwoord.
        /// </summary>
        private const string ValidCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";

        /// <summary>
        /// Genereert een willekeurig wachtwoord van opgegeven lengte.
        /// </summary>
        /// <param name="length">De gewenste lengte van het wachtwoord (moet groter zijn dan 0).</param>
        /// <returns>Een gegenereerd wachtwoord als string.</returns>
        /// <exception cref="ArgumentException">Wanneer de lengte kleiner dan of gelijk aan 0 is.</exception>
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
