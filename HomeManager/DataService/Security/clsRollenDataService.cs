using HomeManager.DAL.Security;
using HomeManager.Model.Security;
using System.Collections.ObjectModel;

namespace HomeManager.DataService.Security
{
    /// <summary>
    /// Dataservice voor het beheren van rollen binnen het beveiligingsgedeelte van HomeManager.
    /// Verzorgt de communicatie met de onderliggende repository.
    /// </summary>
    public class clsRollenDataService : IRollenDataService
    {
        private readonly IRollenRepository _repo = new clsRollenRepository();

        /// <summary>
        /// Verwijdert een rol uit de database.
        /// </summary>
        /// <param name="entity">Het <see cref="clsRollenModel"/> object dat verwijderd moet worden.</param>
        /// <returns><c>true</c> indien succesvol verwijderd; anders <c>false</c>.</returns>
        public bool Delete(clsRollenModel entity)
        {
            return _repo.Delete(entity);
        }

        /// <summary>
        /// Haalt één specifieke rol op (zoekmethode).
        /// </summary>
        /// <returns>Een <see cref="clsRollenModel"/> object.</returns>
        public clsRollenModel Find()
        {
            return _repo.Find();
        }

        /// <summary>
        /// Haalt alle rollen op uit de database.
        /// </summary>
        /// <returns>Een <see cref="ObservableCollection{clsRollenModel}"/> met alle beschikbare rollen.</returns>
        public ObservableCollection<clsRollenModel> GetAll()
        {
            return _repo.GetAll();
        }

        /// <summary>
        /// Haalt een rol op aan de hand van zijn unieke ID.
        /// </summary>
        /// <param name="id">De ID van de op te halen rol.</param>
        /// <returns>Het overeenkomstige <see cref="clsRollenModel"/> object.</returns>
        public clsRollenModel GetById(int id)
        {
            return _repo.GetById(id);
        }

        /// <summary>
        /// Haalt de eerste rol op uit de dataset.
        /// </summary>
        /// <returns>Het eerste <see cref="clsRollenModel"/> object.</returns>
        public clsRollenModel GetFirst()
        {
            return _repo.GetFirst();
        }

        /// <summary>
        /// Voegt een nieuwe rol toe aan de database.
        /// </summary>
        /// <param name="entity">Het <see cref="clsRollenModel"/> object dat toegevoegd moet worden.</param>
        /// <returns><c>true</c> indien succesvol toegevoegd; anders <c>false</c>.</returns>
        public bool Insert(clsRollenModel entity)
        {
            return _repo.Insert(entity);
        }

        /// <summary>
        /// Werkt een bestaande rol bij.
        /// </summary>
        /// <param name="entity">Het gewijzigde <see cref="clsRollenModel"/> object.</param>
        /// <returns><c>true</c> indien succesvol bijgewerkt; anders <c>false</c>.</returns>
        public bool Update(clsRollenModel entity)
        {
            return _repo.Update(entity);
        }
    }
}
