using System.Collections.ObjectModel;

namespace HomeManager.Common
{
    /// <summary>
    /// Interface voor CRUD-operaties op generieke entiteiten.
    /// </summary>
    /// <typeparam name="T">Het type model waarop de service werkt.</typeparam>
    public interface IDataService<T>
    {
        #region Create

        /// <summary>
        /// Voegt een nieuwe entiteit toe aan de datastore.
        /// </summary>
        /// <param name="entity">De in te voegen entiteit.</param>
        /// <returns>True als succesvol, anders false.</returns>
        bool Insert(T entity);

        #endregion

        #region Read

        /// <summary>
        /// Haalt alle entiteiten op.
        /// </summary>
        /// <returns>Een collectie van entiteiten.</returns>
        ObservableCollection<T> GetAll();

        /// <summary>
        /// Haalt de entiteit op met de opgegeven ID.
        /// </summary>
        /// <param name="id">De ID van de entiteit.</param>
        /// <returns>De gevonden entiteit of null.</returns>
        T GetById(int id);

        /// <summary>
        /// Haalt de eerste beschikbare entiteit op.
        /// </summary>
        /// <returns>De eerste entiteit of null.</returns>
        T GetFirst();

        /// <summary>
        /// Voert een zoekopdracht uit (concreet te implementeren).
        /// </summary>
        /// <returns>Een bijpassende entiteit of null.</returns>
        T Find();

        #endregion

        #region Update

        /// <summary>
        /// Wijzigt een bestaande entiteit.
        /// </summary>
        /// <param name="entity">De te updaten entiteit.</param>
        /// <returns>True als succesvol, anders false.</returns>
        bool Update(T entity);

        #endregion

        #region Delete

        /// <summary>
        /// Verwijdert een entiteit uit de datastore.
        /// </summary>
        /// <param name="entity">De te verwijderen entiteit.</param>
        /// <returns>True als succesvol, anders false.</returns>
        bool Delete(T entity);

        #endregion
    }
}
