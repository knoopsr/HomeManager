using HomeManager.Common;
using HomeManager.Model.Security;

namespace HomeManager.DAL.Security
{
    /// <summary>
    /// Interface voor het beheren van geblokkeerde accounts.
    /// </summary>
    public interface ILockedAccountRepository : IRepository<clsLockedAccountModel>
    {
        #region Methoden

        /// <summary>
        /// Deblokkeert een lijst van gebruikers op basis van de geselecteerde accounts.
        /// </summary>
        /// <param name="AccountsIds">Model dat de te deblokkeren accounts bevat.</param>
        /// <returns>True indien succesvol, anders false.</returns>
        bool UnLockUsers(clsLockedAccountModel AccountsIds);

        #endregion
    }
}
