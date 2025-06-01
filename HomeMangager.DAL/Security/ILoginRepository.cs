using HomeManager.Common;
using HomeManager.Model.Security;

namespace HomeManager.DAL.Security
{
    /// <summary>
    /// Interface voor login-gerelateerde databasebewerkingen.
    /// </summary>
    public interface ILoginRepository : IRepository<clsLoginModel>
    {
        #region Methoden

        /// <summary>
        /// Haalt een login op op basis van gebruikersnaam en wachtwoord.
        /// </summary>
        /// <param name="login">De gebruikersnaam.</param>
        /// <param name="wachtwoord">Het wachtwoord.</param>
        /// <returns>Een instantie van <see cref="clsLoginModel"/> indien succesvol; anders null of lege model.</returns>
        clsLoginModel GetByLogin(string login, string wachtwoord);

        /// <summary>
        /// Werkt het wachtwoord van een loginrecord bij.
        /// </summary>
        /// <param name="entity">Het loginmodel waarvan het wachtwoord moet worden gewijzigd.</param>
        /// <param name="Pass">Het nieuwe wachtwoord.</param>
        /// <returns>True indien succesvol, anders false.</returns>
        bool UpdatePassWord(clsLoginModel entity, string Pass);

        #endregion
    }
}
