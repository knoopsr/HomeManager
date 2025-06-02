using HomeManager.Model.Security;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;


namespace HomeManager.DAL.Security
{
    /// <summary>
    /// Repository voor het beheren van rechten (permissions).
    /// </summary>
    public class clsRechtenRepository : IRechtenRepository
    {
        #region Velden

        private ObservableCollection<clsRechtenModel> _mijnCollectie;
        private int nr = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialiseert een nieuwe instantie van <see cref="clsRechtenRepository"/>.
        /// </summary>
        public clsRechtenRepository() { }

        #endregion

        #region Data-opbouw

        /// <summary>
        /// Laadt alle rechten uit de database en vult de collectie.
        /// </summary>
        private void GenerateCollection()
        {
            SqlDataReader reader = clsDAL.GetData(Properties.Resources.S_Rechten);
            _mijnCollectie = new ObservableCollection<clsRechtenModel>();

            while (reader.Read())
            {
                var model = new clsRechtenModel()
                {
                    RechtenID = (int)reader["RechtenID"],
                    RechtenName = (string)reader["RechtenName"],
                    RechtenCode = (int)reader["RechtenCode"],
                    RechtenCatogorieID = (int)reader["RechtenCatogorieID"]
                };

                _mijnCollectie.Add(model);
            }

            reader.Close();
        }

        #endregion

        #region Repository-methodes

        /// <inheritdoc/>
        public ObservableCollection<clsRechtenModel> GetAll()
        {
            GenerateCollection();
            return _mijnCollectie;
        }

        /// <inheritdoc/>
        public clsRechtenModel GetById(int id)
        {
            if (_mijnCollectie == null)
                GenerateCollection();

            return _mijnCollectie.FirstOrDefault(x => x.RechtenID == id);
        }

        /// <summary>
        /// Haalt het eerste recht op dat bij een specifieke categorie hoort.
        /// </summary>
        /// <param name="id">De categorie-ID.</param>
        /// <returns>Het eerste bijhorende recht.</returns>
        public clsRechtenModel GetByCatogorieID(int id)
        {
            if (_mijnCollectie == null)
                GenerateCollection();

            return _mijnCollectie.FirstOrDefault(x => x.RechtenCatogorieID == id);
        }

        /// <inheritdoc/>
        public clsRechtenModel GetFirst()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public clsRechtenModel Find()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool Insert(clsRechtenModel entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool Update(clsRechtenModel entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool Delete(clsRechtenModel entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
