using HomeManager.DAL.Security;
using HomeManager.Model.Security;
using System.Collections.ObjectModel;


namespace HomeManager.DataService.Security
{
    /// <summary>
    /// Dataservice voor het beheren van opgeslagen inloggegevens (credentials) binnen HomeManager.
    /// Deze service maakt gebruik van een repository om de data-operaties uit te voeren.
    /// </summary>
    public class clsCredentialManagementDataService : ICredentialManagementDataService
    {
        private readonly ICredentialManagementRepository _repo = new clsCredentialManagementRepository();

        /// <summary>
        /// Verwijdert een credential record.
        /// </summary>
        /// <param name="entity">Het credentialmodel dat verwijderd moet worden.</param>
        /// <returns><c>true</c> als het succesvol verwijderd werd; anders <c>false</c>.</returns>
        public bool Delete(clsCredentialManagementModel entity)
        {
            return _repo.Delete(entity);
        }

        /// <summary>
        /// Wordt momenteel niet geïmplementeerd.
        /// </summary>
        /// <returns>Geen waarde, altijd een <see cref="NotImplementedException"/>.</returns>
        /// <exception cref="NotImplementedException">Deze methode is nog niet geïmplementeerd.</exception>
        public clsCredentialManagementModel Find()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Haalt alle opgeslagen credentials op.
        /// </summary>
        /// <returns>Een lijst van alle <see cref="clsCredentialManagementModel"/> in een <see cref="ObservableCollection{T}"/>.</returns>
        public ObservableCollection<clsCredentialManagementModel> GetAll()
        {
            return _repo.GetAll();
        }

        /// <summary>
        /// Haalt een credential record op aan de hand van de ID.
        /// </summary>
        /// <param name="id">De ID van het gewenste credential record.</param>
        /// <returns>Het bijbehorende <see cref="clsCredentialManagementModel"/> record.</returns>
        public clsCredentialManagementModel GetById(int id)
        {
            return _repo.GetById(id);
        }

        /// <summary>
        /// Haalt het eerste credential record op uit de dataset.
        /// </summary>
        /// <returns>Het eerste <see cref="clsCredentialManagementModel"/> record.</returns>
        public clsCredentialManagementModel GetFirst()
        {
            return _repo.GetFirst();
        }

        /// <summary>
        /// Voegt een nieuw credential record toe.
        /// </summary>
        /// <param name="entity">Het credentialmodel dat toegevoegd moet worden.</param>
        /// <returns><c>true</c> als het succesvol toegevoegd werd; anders <c>false</c>.</returns>
        public bool Insert(clsCredentialManagementModel entity)
        {
            return _repo.Insert(entity);
        }

        /// <summary>
        /// Wijzigt een bestaand credential record.
        /// </summary>
        /// <param name="entity">Het aangepaste credentialmodel.</param>
        /// <returns><c>true</c> als de update succesvol was; anders <c>false</c>.</returns>
        public bool Update(clsCredentialManagementModel entity)
        {
            return _repo.Update(entity);
        }
    }
}
