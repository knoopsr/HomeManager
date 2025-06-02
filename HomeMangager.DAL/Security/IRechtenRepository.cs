using HomeManager.Common;
using HomeManager.Model.Security;

namespace HomeManager.DAL.Security
{
    /// <summary>
    /// Interface voor repository-acties op rechten (permissies).
    /// </summary>
    public interface IRechtenRepository : IRepository<clsRechtenModel>
    {
        #region Methoden

        /// <summary>
        /// Haalt een recht op op basis van een categorie-ID.
        /// </summary>
        /// <param name="id">De ID van de rechtencategorie.</param>
        /// <returns>Een <see cref="clsRechtenModel"/> object indien gevonden, anders null.</returns>
        clsRechtenModel GetByCatogorieID(int id);

        #endregion
    }
}
