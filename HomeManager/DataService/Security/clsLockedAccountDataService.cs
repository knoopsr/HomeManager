using HomeManager.DAL.Security;
using HomeManager.Model.Security;
using System.Collections.ObjectModel;


namespace HomeManager.DataService.Security
{
    /// <summary>
    /// Dataservice voor het beheren van gelockte gebruikersaccounts in de applicatie.
    /// Verzorgt CRUD-operaties en het deblokkeren van gebruikers.
    /// </summary>
    public class clsLockedAccountDataService : ILockedAccountDataService
    {
        private readonly ILockedAccountRepository _repo = new clsLockedAccountRepository();

        /// <summary>
        /// Verwijdert een gelockt account.
        /// </summary>
        /// <param name="entity">Het model van het gelockte account dat verwijderd moet worden.</param>
        /// <returns><c>true</c> indien succesvol verwijderd; anders <c>false</c>.</returns>
        public bool Delete(clsLockedAccountModel entity)
        {
            return _repo.Delete(entity);
        }

        /// <summary>
        /// Haalt één specifiek gelockt account op. Methode-implementatie afhankelijk van repo-logica.
        /// </summary>
        /// <returns>Een <see cref="clsLockedAccountModel"/> instantie.</returns>
        public clsLockedAccountModel Find()
        {
            return _repo.Find();
        }

        /// <summary>
        /// Haalt alle gelockte accounts op.
        /// </summary>
        /// <returns>Een <see cref="ObservableCollection{clsLockedAccountModel}"/> met alle gelockte accounts.</returns>
        public ObservableCollection<clsLockedAccountModel> GetAll()
        {
            return _repo.GetAll();
        }

        /// <summary>
        /// Haalt een gelockt account op op basis van ID.
        /// </summary>
        /// <param name="id">De ID van het account.</param>
        /// <returns>Het gevonden <see cref="clsLockedAccountModel"/> of null.</returns>
        public clsLockedAccountModel GetById(int id)
        {
            return _repo.GetById(id);
        }

        /// <summary>
        /// Haalt het eerste gelockte account op.
        /// </summary>
        /// <returns>Het eerste <see cref="clsLockedAccountModel"/> item in de lijst.</returns>
        public clsLockedAccountModel GetFirst()
        {
            return _repo.GetFirst();
        }

        /// <summary>
        /// Voegt een nieuw gelockt account toe.
        /// </summary>
        /// <param name="entity">Het model dat toegevoegd moet worden.</param>
        /// <returns><c>true</c> indien succesvol toegevoegd; anders <c>false</c>.</returns>
        public bool Insert(clsLockedAccountModel entity)
        {
            return _repo.Insert(entity);
        }

        /// <summary>
        /// Deblokkeert één of meerdere gebruikers op basis van hun ID's.
        /// </summary>
        /// <param name="AccountsIds">Een model met de gebruikers die gedeblokkeerd moeten worden.</param>
        /// <returns><c>true</c> als de gebruikers succesvol gedeblokkeerd zijn; anders <c>false</c>.</returns>
        public bool UnLockUsers(clsLockedAccountModel AccountsIds)
        {
            return _repo.UnLockUsers(AccountsIds);
        }

        /// <summary>
        /// Werkt een gelockt account bij.
        /// </summary>
        /// <param name="entity">Het model met gewijzigde informatie.</param>
        /// <returns><c>true</c> indien succesvol geüpdatet; anders <c>false</c>.</returns>
        public bool Update(clsLockedAccountModel entity)
        {
            return _repo.Update(entity);
        }
    }
}
