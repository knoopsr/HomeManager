using HomeManager.DAL.Security;
using HomeManager.Model.Security;
using System.Collections.ObjectModel;


namespace HomeManager.DataService.Security
{
    /// <summary>
    /// Dataservice voor het beheren van accounts binnen de applicatie.
    /// Maakt gebruik van een repository om databasebewerkingen uit te voeren.
    /// </summary>
    public class clsAccountDataService : IAccountDataService
    {
        private readonly IAccountRepository _repo = new clsAccountRepository();

        /// <summary>
        /// Verwijdert een account.
        /// </summary>
        /// <param name="entity">Het accountmodel dat verwijderd moet worden.</param>
        /// <returns><c>true</c> indien succesvol verwijderd; anders <c>false</c>.</returns>
        public bool Delete(clsAccountModel entity)
        {
            return _repo.Delete(entity);
        }

        /// <summary>
        /// Niet geïmplementeerd. (Geeft standaard een <see cref="NotImplementedException"/>).
        /// </summary>
        /// <returns>Wordt niet geretourneerd.</returns>
        /// <exception cref="NotImplementedException">Deze methode is niet geïmplementeerd.</exception>
        public clsAccountModel Find()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Haalt alle accounts op.
        /// </summary>
        /// <returns><see cref="ObservableCollection{clsAccountModel}"/> met alle accounts.</returns>
        public ObservableCollection<clsAccountModel> GetAll()
        {
            return _repo.GetAll();
        }

        /// <summary>
        /// Haalt een account op aan de hand van zijn ID.
        /// </summary>
        /// <param name="id">De ID van het account.</param>
        /// <returns>Het overeenkomstige <see cref="clsAccountModel"/>.</returns>
        public clsAccountModel GetById(int id)
        {
            return _repo.GetById(id);
        }

        /// <summary>
        /// Haalt het eerste beschikbare account op.
        /// </summary>
        /// <returns>Het eerste <see cref="clsAccountModel"/> uit de lijst.</returns>
        public clsAccountModel GetFirst()
        {
            return _repo.GetFirst();
        }

        /// <summary>
        /// Voegt een nieuw account toe.
        /// </summary>
        /// <param name="entity">Het accountmodel dat toegevoegd moet worden.</param>
        /// <returns><c>true</c> indien succesvol toegevoegd; anders <c>false</c>.</returns>
        public bool Insert(clsAccountModel entity)
        {
            return _repo.Insert(entity);
        }

        /// <summary>
        /// Werkt een bestaand account bij.
        /// </summary>
        /// <param name="entity">Het accountmodel met geüpdatete gegevens.</param>
        /// <returns><c>true</c> indien succesvol geüpdatet; anders <c>false</c>.</returns>
        public bool Update(clsAccountModel entity)
        {
            return _repo.Update(entity);
        }
    }
}
