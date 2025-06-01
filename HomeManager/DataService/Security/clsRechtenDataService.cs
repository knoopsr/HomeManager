using HomeManager.DAL.Security;
using HomeManager.Model.Security;
using System.Collections.ObjectModel;

namespace HomeManager.DataService.Security
{
    /// <summary>
    /// Dataservice voor het beheren van individuele rechten (rechten binnen categorieën).
    /// Deze service voert CRUD-operaties uit via de bijhorende repository.
    /// </summary>
    public class clsRechtenDataService : IRechtenDataService
    {
        private readonly IRechtenRepository _repo = new clsRechtenRepository();

        /// <summary>
        /// Verwijdert een specifiek recht.
        /// </summary>
        /// <param name="entity">Het model van het recht dat verwijderd moet worden.</param>
        /// <returns><c>true</c> als succesvol verwijderd; anders <c>false</c>.</returns>
        public bool Delete(clsRechtenModel entity)
        {
            return _repo.Delete(entity);
        }

        /// <summary>
        /// Haalt het eerste gevonden recht op.
        /// </summary>
        /// <returns>Een <see cref="clsRechtenModel"/> instantie.</returns>
        public clsRechtenModel Find()
        {
            return _repo.Find();
        }

        /// <summary>
        /// Haalt alle rechten op.
        /// </summary>
        /// <returns>Een lijst met <see cref="clsRechtenModel"/> objecten.</returns>
        public ObservableCollection<clsRechtenModel> GetAll()
        {
            return _repo.GetAll();
        }

        /// <summary>
        /// Haalt een specifiek recht op op basis van ID.
        /// </summary>
        /// <param name="id">De unieke ID van het recht.</param>
        /// <returns>Een <see cref="clsRechtenModel"/> object of <c>null</c> als niet gevonden.</returns>
        public clsRechtenModel GetById(int id)
        {
            return _repo.GetById(id);
        }

        /// <summary>
        /// Haalt een recht op dat gekoppeld is aan een specifieke categorie-ID.
        /// </summary>
        /// <param name="id">De ID van de rechtencategorie.</param>
        /// <returns>Een <see cref="clsRechtenModel"/> object of <c>null</c> als niet gevonden.</returns>
        public clsRechtenModel GetByCatogorieID(int id)
        {
            return _repo.GetByCatogorieID(id);
        }

        /// <summary>
        /// Haalt het eerste recht op uit de lijst.
        /// </summary>
        /// <returns>Het eerste <see cref="clsRechtenModel"/> item.</returns>
        public clsRechtenModel GetFirst()
        {
            return _repo.GetFirst();
        }

        /// <summary>
        /// Voegt een nieuw recht toe.
        /// </summary>
        /// <param name="entity">Het recht dat toegevoegd moet worden.</param>
        /// <returns><c>true</c> als succesvol toegevoegd; anders <c>false</c>.</returns>
        public bool Insert(clsRechtenModel entity)
        {
            return _repo.Insert(entity);
        }

        /// <summary>
        /// Werkt een bestaand recht bij.
        /// </summary>
        /// <param name="entity">Het bijgewerkte <see cref="clsRechtenModel"/> object.</param>
        /// <returns><c>true</c> als succesvol geüpdatet; anders <c>false</c>.</returns>
        public bool Update(clsRechtenModel entity)
        {
            return _repo.Update(entity);
        }
    }
}
