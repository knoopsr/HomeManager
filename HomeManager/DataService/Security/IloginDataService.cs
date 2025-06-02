using HomeManager.Common;
using HomeManager.Model.Security;

namespace HomeManager.DataService.Security
{
    /// <summary>
    /// Interface voor het beheren van loginfunctionaliteit in de applicatie.
    /// Biedt extra methoden naast de standaard CRUD-operaties via <see cref="IDataService{clsLoginModel}"/>.
    /// </summary>
    public interface IloginDataService : IDataService<clsLoginModel>
    {
        /// <summary>
        /// Haalt een loginmodel op op basis van gebruikersnaam en wachtwoord.
        /// </summary>
        /// <param name="login">De gebruikersnaam of loginnaam.</param>
        /// <param name="wachtwoord">Het bijhorende wachtwoord.</param>
        /// <returns>Een <see cref="clsLoginModel"/> indien gevonden; anders <c>null</c>.</returns>
        clsLoginModel GetByLogin(string login, string wachtwoord);

        /// <summary>
        /// Wijzigt het wachtwoord van een bestaande gebruiker.
        /// </summary>
        /// <param name="entity">Het <see cref="clsLoginModel"/> waarvoor het wachtwoord gewijzigd moet worden.</param>
        /// <param name="Pass">Het nieuwe wachtwoord (geëncrypteerd of plain afhankelijk van implementatie).</param>
        /// <returns><c>true</c> indien succesvol gewijzigd; anders <c>false</c>.</returns>
        bool UpdatePassWord(clsLoginModel entity, string Pass);
    }
}
