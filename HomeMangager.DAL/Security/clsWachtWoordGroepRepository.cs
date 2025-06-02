using HomeManager.Model.Security;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;


namespace HomeManager.DAL.Security
{
    /// <summary>
    /// Repository voor het beheren van wachtwoordgroepen in de beveiligingsmodule.
    /// </summary>
    public class clsWachtWoordGroepRepository : IWachtWoordGroepRepository
    {
        #region Velden

        private ObservableCollection<clsWachtWoordGroepModel> _mijnCollectie;
        private int nr = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// Lege constructor.
        /// </summary>
        public clsWachtWoordGroepRepository() { }

        #endregion

        #region Data-opbouw

        /// <summary>
        /// Laadt alle wachtwoordgroepen uit de database.
        /// </summary>
        private void GenerateCollection()
        {
            SqlDataReader reader = clsDAL.GetData(Properties.Resources.S_WachtWoordenGroep);
            _mijnCollectie = new ObservableCollection<clsWachtWoordGroepModel>();

            while (reader.Read())
            {
                var model = new clsWachtWoordGroepModel
                {
                    WachtwoordGroepID = (int)reader["WachtwoordGroepID"],
                    WachtwoordGroep = (string)reader["WachtwoordGroep"],
                    ControlField = reader["ControlField"]
                };

                _mijnCollectie.Add(model);
            }

            reader.Close();
        }

        #endregion

        #region CRUD-methodes

        /// <inheritdoc/>
        public ObservableCollection<clsWachtWoordGroepModel> GetAll()
        {
            GenerateCollection();
            return _mijnCollectie;
        }

        /// <inheritdoc/>
        public clsWachtWoordGroepModel GetById(int id)
        {
            if (_mijnCollectie == null)
                GenerateCollection();

            return _mijnCollectie.FirstOrDefault(x => x.WachtwoordGroepID == id);
        }

        /// <inheritdoc/>
        public clsWachtWoordGroepModel GetFirst()
        {
            if (_mijnCollectie == null)
                GenerateCollection();

            return _mijnCollectie.FirstOrDefault();
        }

        /// <inheritdoc/>
        public bool Insert(clsWachtWoordGroepModel entity)
        {
            (DataTable dt, bool ok, string boodschap) = clsDAL.ExecuteDataTable(
                Properties.Resources.I_WachtWoordenGroep,
                clsDAL.Parameter("WachtwoordGroep", entity.WachtwoordGroep),
                clsDAL.Parameter("@Returnvalue", 0));

            if (!ok)
                entity.ErrorBoodschap = boodschap;

            return ok;
        }

        /// <inheritdoc/>
        public bool Update(clsWachtWoordGroepModel entity)
        {
            (DataTable dt, bool ok, string boodschap) = clsDAL.ExecuteDataTable(
                Properties.Resources.U_WachtWoordenGroep,
                clsDAL.Parameter("WachtwoordGroepID", entity.WachtwoordGroepID),
                clsDAL.Parameter("WachtwoordGroep", entity.WachtwoordGroep),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@Returnvalue", 0));

            if (!ok)
                entity.ErrorBoodschap = boodschap;

            return ok;
        }

        /// <inheritdoc/>
        public bool Delete(clsWachtWoordGroepModel entity)
        {
            (DataTable dt, bool ok, string boodschap) = clsDAL.ExecuteDataTable(
                Properties.Resources.D_WachtWoordenGroep,
                clsDAL.Parameter("WachtwoordGroepID", entity.WachtwoordGroepID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@Returnvalue", 0));

            if (!ok)
                entity.ErrorBoodschap = boodschap;

            return ok;
        }

        #endregion

        #region Niet geïmplementeerde methodes

        /// <inheritdoc/>
        public clsWachtWoordGroepModel Find()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
