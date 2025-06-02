using HomeManager.Common;
using HomeManager.Model.Security;

namespace HomeManager.DataService.Security
{
    /// <summary>
    /// Interface voor het beheren van gelokte gebruikersaccounts.
    /// Bevat methoden voor het ontgrendelen van accounts naast de standaard CRUD-operaties.
    /// </summary>
    public interface ILockedAccountDataService : IDataService<clsLockedAccountModel>
    {
        /// <summary>
        /// Ontgrendelt één of meerdere gebruikersaccounts.
        /// </summary>
        /// <param name="AccountsIds">
        /// Een <see cref="clsLockedAccountModel"/> met de ID's of selectie van gebruikers om te ontgrendelen.
        /// </param>
        /// <returns><c>true</c> als de gebruikers succesvol zijn ontgrendeld; anders <c>false</c>.</returns>
        bool UnLockUsers(clsLockedAccountModel AccountsIds);
    }
}
