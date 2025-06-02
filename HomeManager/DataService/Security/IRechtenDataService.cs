using HomeManager.Common;
using HomeManager.Model.Security;

namespace HomeManager.DataService.Security
{
    /// <summary>
    /// Interface voor het beheren van individuele rechten binnen de applicatie.
    /// Bevat extra methoden naast de standaard CRUD-operaties.
    /// </summary>
    public interface IRechtenDataService : IDataService<clsRechtenModel>
    {
        /// <summary>
        /// Haalt een recht op dat gekoppeld is aan een specifieke rechten categorie.
        /// </summary>
        /// <param name="id">De ID van de rechten categorie.</param>
        /// <returns>Een <see cref="clsRechtenModel"/> indien gevonden; anders <c>null</c>.</returns>
        clsRechtenModel GetByCatogorieID(int id);
    }
}
