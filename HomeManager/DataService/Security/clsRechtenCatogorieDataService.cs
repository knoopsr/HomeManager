using HomeManager.DAL.Security;
using HomeManager.Model.Security;
using System.Collections.ObjectModel;


namespace HomeManager.DataService.Security
{
    /// <summary>
    /// Dataservice voor het beheren van rechten categorieën.
    /// Deze service spreekt de repositorylaag aan om CRUD-operaties uit te voeren.
    /// </summary>
    public class clsRechtenCatogorieDataService : IRechtenCatogorieDataService
    {
        private readonly IRechtenCatogorieRepository _repo = new clsRechtenCatogorieRepository();

        /// <summary>
        /// Verwijdert een rechten categorie.
        /// </summary>
        /// <param name="entity">Het model van de te verwijderen rechten categorie.</param>
        /// <returns><c>true</c> indien succesvol verwijderd; anders <c>false</c>.</returns>
        public bool Delete(clsRechtenCatogorieModel entity)
        {
            return _repo.Delete(entity);
        }

        /// <summary>
        /// Haalt de eerste gevonden rechten categorie op via de repository.
        /// </summary>
        /// <returns>Een <see cref="clsRechtenCatogorieModel"/> instantie.</returns>
        public clsRechtenCatogorieModel Find()
        {
            return _repo.Find();
        }

        /// <summary>
        /// Haalt alle rechten categorieën op.
        /// </summary>
        /// <returns>Een <see cref="ObservableCollection{clsRechtenCatogorieModel}"/> met alle categorieën.</returns>
        public ObservableCollection<clsRechtenCatogorieModel> GetAll()
        {
            return _repo.GetAll();
        }

        /// <summary>
        /// Haalt een specifieke rechten categorie op aan de hand van zijn ID.
        /// </summary>
        /// <param name="id">De unieke ID van de rechten categorie.</param>
        /// <returns>Een <see cref="clsRechtenCatogorieModel"/> object of <c>null</c> indien niet gevonden.</returns>
        public clsRechtenCatogorieModel GetById(int id)
        {
            return _repo.GetById(id);
        }

        /// <summary>
        /// Haalt de eerste rechten categorie op (meestal gebruikt als fallback).
        /// </summary>
        /// <returns>De eerste <see cref="clsRechtenCatogorieModel"/> in de dataset.</returns>
        public clsRechtenCatogorieModel GetFirst()
        {
            return _repo.GetFirst();
        }

        /// <summary>
        /// Voegt een nieuwe rechten categorie toe.
        /// </summary>
        /// <param name="entity">Het model van de toe te voegen categorie.</param>
        /// <returns><c>true</c> als het succesvol toegevoegd werd; anders <c>false</c>.</returns>
        public bool Insert(clsRechtenCatogorieModel entity)
        {
            return _repo.Insert(entity);
        }

        /// <summary>
        /// Werkt een bestaande rechten categorie bij.
        /// </summary>
        /// <param name="entity">Het aangepaste model van de rechten categorie.</param>
        /// <returns><c>true</c> als de update succesvol was; anders <c>false</c>.</returns>
        public bool Update(clsRechtenCatogorieModel entity)
        {
            return _repo.Update(entity);
        }
    }
}
