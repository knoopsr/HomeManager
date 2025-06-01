using HomeManager.DAL.Security;
using HomeManager.Model.Security;
using System.Collections.ObjectModel;


namespace HomeManager.DataService.Security
{
    /// <summary>
    /// Dataservice voor het beheren van wachtwoordgroepen.
    /// Biedt CRUD-functionaliteit via de repositorylaag.
    /// </summary>
    public class clsWachtwoordGroepDataService : IWachtwoordGroepDataService
    {
        private readonly IWachtWoordGroepRepository _repo = new clsWachtWoordGroepRepository();

        /// <summary>
        /// Verwijdert een wachtwoordgroep.
        /// </summary>
        /// <param name="entity">De te verwijderen <see cref="clsWachtWoordGroepModel"/> instantie.</param>
        /// <returns><c>true</c> indien succesvol verwijderd; anders <c>false</c>.</returns>
        public bool Delete(clsWachtWoordGroepModel entity)
        {
            return _repo.Delete(entity);
        }

        /// <summary>
        /// Zoekt en retourneert een enkele wachtwoordgroep.
        /// </summary>
        /// <returns>Een <see cref="clsWachtWoordGroepModel"/> instantie.</returns>
        public clsWachtWoordGroepModel Find()
        {
            return _repo.Find();
        }

        /// <summary>
        /// Haalt alle wachtwoordgroepen op.
        /// </summary>
        /// <returns>Een <see cref="ObservableCollection{clsWachtWoordGroepModel}"/> met alle groepen.</returns>
        public ObservableCollection<clsWachtWoordGroepModel> GetAll()
        {
            return _repo.GetAll();
        }

        /// <summary>
        /// Haalt een specifieke wachtwoordgroep op via zijn ID.
        /// </summary>
        /// <param name="id">De ID van de groep.</param>
        /// <returns>Het <see cref="clsWachtWoordGroepModel"/> object met overeenkomende ID.</returns>
        public clsWachtWoordGroepModel GetById(int id)
        {
            return _repo.GetById(id);
        }

        /// <summary>
        /// Haalt de eerste wachtwoordgroep op uit de dataset.
        /// </summary>
        /// <returns>De eerste <see cref="clsWachtWoordGroepModel"/> in de lijst.</returns>
        public clsWachtWoordGroepModel GetFirst()
        {
            return _repo.GetFirst();
        }

        /// <summary>
        /// Voegt een nieuwe wachtwoordgroep toe.
        /// </summary>
        /// <param name="entity">Het toe te voegen <see cref="clsWachtWoordGroepModel"/> object.</param>
        /// <returns><c>true</c> indien succesvol toegevoegd; anders <c>false</c>.</returns>
        public bool Insert(clsWachtWoordGroepModel entity)
        {
            return _repo.Insert(entity);
        }

        /// <summary>
        /// Werkt een bestaande wachtwoordgroep bij.
        /// </summary>
        /// <param name="entity">De aangepaste <see cref="clsWachtWoordGroepModel"/>.</param>
        /// <returns><c>true</c> indien succesvol bijgewerkt; anders <c>false</c>.</returns>
        public bool Update(clsWachtWoordGroepModel entity)
        {
            return _repo.Update(entity);
        }
    }
}
