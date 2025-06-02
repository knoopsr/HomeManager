using System.Collections.ObjectModel;

namespace HomeManager.Common
{
    /// <summary>
    /// Generieke repository-interface voor standaard CRUD-operaties.
    /// </summary>
    /// <typeparam name="T">Het type entiteit waarop de repository werkt.</typeparam>
    public interface IRepository<T>
    {
        #region Create

        /// <summary>
        /// Voegt een nieuwe entiteit toe aan de datastore.
        /// </summary>
        /// <param name="entity">De toe te voegen entiteit.</param>
        /// <returns>True als de invoeging is gelukt, anders false.</returns>
        bool Insert(T entity);

        #endregion

        #region Read

        /// <summary>
        /// Haalt alle entiteiten op.
        /// </summary>
        /// <returns>Een collectie van alle entiteiten.</returns>
        ObservableCollection<T> GetAll();

        /// <summary>
        /// Haalt de entiteit op met de opgegeven ID.
        /// </summary>
        /// <param name="id">De ID van de entiteit.</param>
        /// <returns>De gevraagde entiteit, of null indien niet gevonden.</returns>
        T GetById(int id);

        /// <summary>
        /// Haalt de eerste beschikbare entiteit op.
        /// </summary>
        /// <returns>De eerste entiteit, of null als de collectie leeg is.</returns>
        T GetFirst();

        /// <summary>
        /// Voert een specifieke zoekopdracht uit.
        /// </summary>
        /// <returns>De gevonden entiteit, of null.</returns>
        T Find();

        #endregion

        #region Update

        /// <summary>
        /// Wijzigt een bestaande entiteit in de datastore.
        /// </summary>
        /// <param name="entity">De bij te werken entiteit.</param>
        /// <returns>True als de update is gelukt, anders false.</returns>
        bool Update(T entity);

        #endregion

        #region Delete

        /// <summary>
        /// Verwijdert een entiteit uit de datastore.
        /// </summary>
        /// <param name="entity">De te verwijderen entiteit.</param>
        /// <returns>True als de verwijdering is gelukt, anders false.</returns>
        bool Delete(T entity);

        #endregion
    }
}
