using HomeManager.Model.Security;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;


namespace HomeManager.DAL.Security
{
    /// <summary>
    /// Repository voor het ophalen van rechten categorieën uit de database.
    /// </summary>
    public class clsRechtenCatogorieRepository : IRechtenCatogorieRepository
    {
        #region Velden

        private ObservableCollection<clsRechtenCatogorieModel> _mijnCollectie;
        int nr = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// Maakt een nieuwe instantie van <see cref="clsRechtenCatogorieRepository"/>.
        /// </summary>
        public clsRechtenCatogorieRepository() { }

        #endregion

        #region Data-opbouw

        /// <summary>
        /// Laadt alle rechten categorieën uit de database en vult de interne collectie.
        /// </summary>
        private void GenerateCollection()
        {
            SqlDataReader reader = clsDAL.GetData(Properties.Resources.S_RechtenCatogorie);
            _mijnCollectie = new ObservableCollection<clsRechtenCatogorieModel>();

            while (reader.Read())
            {
                var model = new clsRechtenCatogorieModel()
                {
                    RechtenCatogorieID = (int)reader["RechtenCatogorieID"],
                    CatogorieNaam = (string)reader["CatogorieNaam"],
                    Rechten = new ObservableCollection<clsRechtenModel>() // Leeg, rechten kunnen later gekoppeld worden
                };

                _mijnCollectie.Add(model);
            }

            reader.Close();
        }

        #endregion

        #region Repository-methodes

        /// <inheritdoc/>
        public ObservableCollection<clsRechtenCatogorieModel> GetAll()
        {
            GenerateCollection();
            return _mijnCollectie;
        }

        /// <inheritdoc/>
        public clsRechtenCatogorieModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public clsRechtenCatogorieModel GetFirst()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public clsRechtenCatogorieModel Find()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool Insert(clsRechtenCatogorieModel entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool Update(clsRechtenCatogorieModel entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool Delete(clsRechtenCatogorieModel entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
